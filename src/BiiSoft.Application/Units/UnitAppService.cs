using Abp.Authorization;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using BiiSoft.Configuration;
using BiiSoft.Units.Dto;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services.Dto;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.UI;
using System.Collections.Concurrent;
using BiiSoft.PLinqs;
using BiiSoft.Folders;
using BiiSoft.Extensions;
using BiiSoft.Items;
using BiiSoft.Dtos;
using Amazon.Runtime.Internal.Auth;

namespace BiiSoft.Units
{

    [AbpAuthorize]
    public class UnitAppService : BiiSoftAppServiceBase, IUnitAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IBiiSoftRepository<Unit, Guid> _unitRepository;

        public UnitAppService(): base()
        {
            _unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
            _unitRepository = IocManager.Instance.Resolve<IBiiSoftRepository<Unit, Guid>>();
        }

        private static bool IsPrimeNumber(int number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return number == 2;
            if (number % 3 == 0) return number == 3;
            if (number % 5 == 0) return number == 5;
            if (number % 7 == 0) return number == 7;
            if (number % 11 == 0) return number == 11;
            if (number % 13 == 0) return number == 13;
            if (number % 17 == 0) return number == 17;
            if (number % 19 == 0) return number == 19;
            if (number % 23 == 0) return number == 23;
            if (number % 29 == 0) return number == 29;
            if (number % 31 == 0) return number == 31;
            if (number % 37 == 0) return number == 37;
            if (number % 41 == 0) return number == 41;
            if (number % 43 == 0) return number == 43;
            if (number % 47 == 0) return number == 47;
            
            for (int i = 53; i <= (int)Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }


        private IEnumerable<Unit> BuildUnitListPLinq(int tenantId, long userId, int entries, List<string> nameList)
        {
            IEnumerable<Unit> result;
                        
            var sources = Enumerable.Range(0, entries);

            result = from i in sources.AsParallel()
                        select
                        Unit.Create(
                            AbpSession.TenantId.Value,
                            AbpSession.UserId.Value,
                            nameList[new Random().Next(nameList.Count)] + $" {i + 1}",
                            null);

            return result;
        }

        private IEnumerable<Unit> BuildUnitListPLinqPartition(int tenantId, long userId, int entries, List<string> nameList)
        {           
            var sources = Enumerable.Range(0, entries).ToList();
            int procCount = System.Environment.ProcessorCount;

            //Partitioner<int> partitioner = new OrderableListPartitioner<int>(sources);
            Partitioner<int> partitioner = new ListPartitioner<int>(sources, procCount);

            // Use with PLINQ
            IEnumerable<Unit> result = from i in partitioner.AsParallel()
                                       let name = nameList[new Random().Next(nameList.Count)] + $" {i + 1}"
                                       select
                                        Unit.Create(
                                            AbpSession.TenantId.Value,
                                            AbpSession.UserId.Value,
                                            name,
                                            name);

            return result;
        }

        private IEnumerable<Unit> BuildUnitListLinq(int tenantId, long userId, int entries, List<string> nameList)
        {
            IEnumerable<Unit> result;

            var sources = Enumerable.Range(0, entries);

            result = from i in sources
                     select
                     Unit.Create(
                         AbpSession.TenantId.Value,
                         AbpSession.UserId.Value,
                         nameList[new Random().Next(nameList.Count)] + $" {i + 1}",
                         null);

            return result;
        }


        private IEnumerable<Unit> BuildUnitListForLoop(int tenantId, long userId, int entries, List<string> nameList)
        {
            IEnumerable<Unit> result = new List<Unit>();

            for (var i = 1; i <= entries; i++)
            {
                var name = nameList[new Random().Next(nameList.Count)] + $" {i}";
                result.Append(Unit.Create(AbpSession.TenantId.Value, AbpSession.UserId.Value, name, null));
            }

            return result;
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<long> BulkInsert()
        {
            var unitNameList = new List<string>()
            {
                "ស្រាបៀរ",
                "ទឹកបរិសុទ្ទ",
                "នុំកញ្ចប់",
                "ទឹកក្រូច",
                "ទឹកផ្លែឈើ",
                "សាប៊ូ",
                "អង្ករ",
                "មី",
                "គ្រឿងសមុទ្រ",
                "ថ្នាំពេទ្យ",
            };
            var records = 1000000;

            //var list = BuildUnitListForLoop(AbpSession.TenantId.Value, AbpSession.UserId.Value, records, unitNameList);
            //var list = BuildUnitListLinq(AbpSession.TenantId.Value, AbpSession.UserId.Value, records, unitNameList);
            //var list = BuildUnitListPLinq(AbpSession.TenantId.Value, AbpSession.UserId.Value, records, unitNameList);
            var list = BuildUnitListPLinqPartition(AbpSession.TenantId.Value, AbpSession.UserId.Value, records, unitNameList);

            var listInput = list.ToList();
            var result = listInput.Count;

            var doubplicate = list.GroupBy(s => s.Name).Where(s => s.Count() > 1).FirstOrDefault();
            if (doubplicate != null) throw new UserFriendlyException(L("DupplicateName"));

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    var count = await _unitRepository.CountAsync();
                    if (count > 0) return 0;

                    //await _unitRepository.BulkInsertAsync(listInput);

                    var subList = listInput.ListPartition(10);
                    try
                    {
                        foreach (var l in subList)
                        {
                            await _unitRepository.BulkInsertAsync(l);
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }
                }

                await uow.CompleteAsync();
            }

            return result;
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task BulkDelete()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    var units = await _unitRepository.GetAll().AsNoTracking().ToListAsync();
                    
                    await _unitRepository.BulkDeleteAsync(units);
                }

                await uow.CompleteAsync();
            }

        }

        [UnitOfWork(IsDisabled = true)]
        public async Task BulkSoftDelete()
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    var units = await _unitRepository.GetAll().AsNoTracking().ToListAsync();
                    //units.AsParallel().ForAll(u => u.IsDeleted = true);

                    //await _unitRepository.BulkUpdateAsync(units);
                    var delete = units.FirstOrDefault();
                    if (delete != null) await _unitRepository.DeleteAsync(delete);
                }

                await uow.CompleteAsync();
            }

        }


        public async Task TestDate()
        {
            //var zones = TimeZoneInfo.GetSystemTimeZones();
            //var zoneId = "SE Asia Standard Time";
            //var zoneId = "Singapore Standard Time";
            var zoneId = "Tokyo Standard Time";
            var utcNow = Abp.Timing.Clock.Now;
            var fromUtcDate = utcNow.StartDateZone(zoneId);
            var toUtcDate = utcNow.EndDateZone(zoneId);

            var fromDate = fromUtcDate.ToZone(zoneId);
            var toDate = toUtcDate.ToZone(zoneId);
        }

        public async Task TestSetting()
        {
            var settings = await SettingManager.GetAllSettingValuesAsync();
            var appSettings = await SettingManager.GetAllSettingValuesForApplicationAsync();

            IReadOnlyList<ISettingValue> tenantSettings;
            IReadOnlyList<ISettingValue> userSettings;

            if (AbpSession.TenantId.HasValue)
            {
                tenantSettings = await SettingManager.GetAllSettingValuesForTenantAsync(AbpSession.TenantId.Value);
            }

            if (AbpSession.UserId.HasValue)
            {
                var userIdentifier = new Abp.UserIdentifier(AbpSession.TenantId, AbpSession.UserId.Value);
                userSettings = await SettingManager.GetAllSettingValuesForUserAsync(userIdentifier);


                var darkTheme = await SettingManager.GetSettingValueForUserAsync<bool>(AppSettingNames.UI.Theme.ColorScheme, userIdentifier);
                if (!darkTheme)
                {
                    await SettingManager.ChangeSettingForUserAsync(userIdentifier, AppSettingNames.UI.Theme.ColorScheme, "true");
                }

            }

            var track = 0;

        }

       
        public async Task<PagedResultDto<UnitDto>> Find(PagedActiveSortFilterInputDto input)
        {
            var query = _unitRepository.GetAll()
                                .Where(s => string.IsNullOrWhiteSpace(input.Keyword) || s.Name.ToLower().Contains(input.Keyword.ToLower()))
                                .AsNoTracking()
                                .Select(s => new UnitDto
                                {
                                    Id = s.Id,
                                    Name = s.Name
                                })
                                .OrderBy(input.GetOrdering());

            var count = await query.CountAsync();
            if (count == 0) return new PagedResultDto<UnitDto> { Items = new List<UnitDto>(), TotalCount = 0 };

            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<UnitDto> { Items = items, TotalCount = count };
        }

        public async Task TestPdf()
        {
            //string html = string.Empty;
            //var parser = new HtmlParser();
            //var doc = parser.ParseDocument(html);
            //var element = doc.QuerySelector("div");
            //var newEl = doc.CreateElement("div");


            string logo;

            //using (var webClient = new WebClient())
            //{
            //    var logoUrl = "https://1000logos.net/wp-content/uploads/2021/10/logo-Meta.png";
            //    byte[] imageBytes = webClient.DownloadData(logoUrl);
            //    var base64ImageString = Convert.ToBase64String(imageBytes);
            //    logo = $"<img class='logo' src='data:image/png;base64,{base64ImageString}' style='width:48px; vertical-align: middle;' />";
            //}

            var appFolder = IocManager.Instance.Resolve<IAppFolders>();

            var logoPath = System.IO.Path.Combine(appFolder.ImagesFolder, "logo-Meta.png");
            byte[] imageBytes = System.IO.File.ReadAllBytes(logoPath);
            var base64ImageString = Convert.ToBase64String(imageBytes);
            logo = $"<img class='logo' src='data:image/png;base64,{base64ImageString}' style='width:48px; vertical-align: middle;' />";

            var fontPath = System.IO.Path.Combine(appFolder.FontsFolder,"KhmerOS.ttf");
            byte[] fontBytes = System.IO.File.ReadAllBytes(fontPath);
            var base64FontString = Convert.ToBase64String(fontBytes);
            var khmerFont = "@font-face {font-family: 'KhmerOS'; src: url(data:application/font-ttf;charset=utf-8;base64," + base64FontString + ") format('truetype'); }";


            var reportHtml = "<!DOCTTYPE html>" +
                             "<html>" +
                             "<head>" +
                             "<meta charset=\"UTF-8\">" +
                             "<title>ក្រុមហ៊ុន មេត្តា ខូអ៊ិលធីឌី Meta Co., Ltd.</title>" +
                             "<style type='text/css'>" +
                             khmerFont +
                             "table {border-collapse: collapse; width: 100%}" +
                             "thead {display: table-header-group; break-inside: avoid;}" +
                             "tfoot {display: table-footer-group; break-inside: avoid;}" +
                             "th, td {border: 0px solid #333;}" +
                             "</style>" +
                             "</head>" +
                             "<body>" +
                             "<div style='text-align:center;font-size:48px;'> <span style='font-family:KhmerOS; font-size: 18px; color:navy'> វិក្កយបត្រ </span><br> Invoice</div>" +
                             "<table>" +
                             "<thead>" +
                             "<tr>" +
                             "<th style='text-align:left;'>No</th>" +
                             "<th style='text-align:left;'>Name</th>" +
                             "<th style='text-align:right;'>Qty</th>" +
                             "</tr>" +
                             "</thead>" +
                             "<tbody>";

            var books = new List<string>()
            {
                "មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ)",
                "បង្រៀន​ដោយ៖ លោកគ្រូ ណន ដារី",
                "សាលា​បឋមសិក្សាគួច ម៉េងលីទួលបេង ខេត្ត កំពង់ចាម",
                "​តើក្នុងរាជ្យ ព្រះចន្ទរាជា ប្រទេសខ្មែរ ស្ថិតនៅចំណុះប្រទេសណា? (ពិន្ទុ ០.២៥)",
                "មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ)"
            };

            for (var i = 1; i <= 500; i++)
            {
                var bookName = books[new Random().Next(books.Count)] + $" {i}";

                reportHtml += "<tr>" +
                             $"<td style='text-align:left;'>{i}</td>" +
                             $"<td style='text-align:left;'>{bookName}</td>" +
                             "<td style='text-align:right;'>10</td>" +
                             "</tr>";
            }

            reportHtml += "</tbody>" +
                             //"<tfoot>" +
                             //"<tr><th colspan='3'>Footer</th></tr>"+
                             //"</tfoot>" +
                             "</table>" +
                             "</body>" +
                             "</html>";


            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();            
                
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();

            //var headerOption = new Dictionary<string, string>();
            //headerOption.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");
            //headerOption.Add("upgrade-insecure-requests", "1");
            //headerOption.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            //headerOption.Add("accept-encoding", "gzip, deflate, br");
            //headerOption.Add("accept-language", "en-US,en;q=0.9,en;q=0.8");

            //await page.SetExtraHttpHeadersAsync(headerOption);

            //await page.SetViewportAsync(new ViewPortOptions { 
            //    Width = 1024, 
            //    Height = 768, 
            //    IsLandscape = true, 
            //    DeviceScaleFactor = 1
            //});

           
            await page.SetContentAsync(reportHtml);
            await page.WaitForSelectorAsync("body");
            var result = await page.GetContentAsync();

            await page.PdfAsync($"example_{DateTime.Now.ToString("yyMMddHHmmss")}.pdf", new PdfOptions
            {
                Format = PaperFormat.A4,
                Landscape = false,
                DisplayHeaderFooter = true,
                HeaderTemplate = $"<div id='header-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; border-bottom: 1px solid #333; width: 100%;'>{logo} <span class='title'></span><div style='float: right;'><span class='pageNumber'></span>/<span class='totalPages'></span></div></div>",
                FooterTemplate = "<div id='footer-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; border-top: 1px solid #333; width: 100%;'>Printed Date : <span class='date'></span><div style='float: right;'><span class='pageNumber'></span>/<span class='totalPages'></span></div></div>",
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "2cm",
                    Bottom = "1.5cm",
                    Left = "1.5cm",
                    Right = "1.5cm"
                }                
            });
        }


        public async Task<string> GenerateLicenseKey(string productKey)
        {
            return await Task.Run(() =>
            {
                return Helper.LicenseRegister.GetLicenseKey(productKey);
            });
        }

       
    }
}
