using Abp.Dependency;
using BiiSoft.Folders;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Net.Mail;
using Hangfire;
using BiiSoft.Tests;
using Hangfire.Common;
using Hangfire.States;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Abp.Auditing;
using Microsoft.EntityFrameworkCore;
using BiiSoft.Items;
using Abp.Timing;
using Abp.Authorization;
using System.Linq.Dynamic.Core;
using Abp.Timing.Timezone;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiiSoft.Test
{
   
    public class TestOutput
    {
        public long ExecutionDuration { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public long CumulativeSUM { get; set; }
    }

    public class TestModelOutput
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateIndex { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long RowNumber { get; set; }
        public int TotalRecords { get; set; }
    }

    [AbpAuthorize]
    public class TestAppService: BiiSoftAppServiceBase
    {
        private readonly IEmailSender _emailSender;
        private readonly ITestHangfireManager _testHangfireManager;
        public TestAppService(
            IEmailSender emailSender,
            ITestHangfireManager testHangfireManager
            )
        {
            _emailSender = emailSender;
            _testHangfireManager = testHangfireManager;
            
        }

        public async Task TestSendMail(string to, string subject, string message, bool bodyHtml)
        {
            //insert into "AbpSettings"("CreationTime", "CreatorUserId", "Name", "Value")
            //values
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.Host', 'sg2plcpnl0195.prod.sin2.secureserver.net'),
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.UseDefaultCredentials', 'false'),
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.UserName', 'mail@aptsec168.com'),
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.Password', '6B/rgthHloxIZoMnVsvHCA=='), --mail123@aptsec
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.Port', '465'),
            //(current_timestamp, '1', 'Abp.Net.Mail.Smtp.EnableSsl', 'true')
            //update "AbpSettings" set "Value" = 'mail@aptsec168.com' where "Name" = 'Abp.Net.Mail.DefaultFromAddress'
            //update "AbpSettings" set "Value" = 'Sale Dpt' where "Name" = 'Abp.Net.Mail.DefaultFromDisplayName'


            //await _emailSender.SendAsync("boralim.corarl@gmail.com", "Test Mail", "Hellow from email sender", false);
            await _emailSender.SendAsync(to, subject, message, bodyHtml);
        }

        public async Task PdfBacord()
        {
            var reportHtml = @"<!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='utf-8'>
                        <title></title>
                        <link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Khmer'>
                        <style type='text/css'>
                            @media print {
                                @page {
                                    size: 39mm 19mm;
                                }
                            }

                                body {
                                   margin: 0;
	                           padding:0;
                                }
                        </style>
                    </head>
                    <body>
                       <div style='text-align:center; padding-top: 5px;'>
	                    <img alt='Barcode Generator TEC-IT' 
	                         style=max-width:36mm; ""
                                 src='https://barcode.tec-it.com/barcode.ashx?data=123-456-78-00'/>
                       </div>

                      <div style='text-align:center; padding-top: 5px;'>
	                    <img alt='Barcode Generator TEC-IT' 
	                          style=max-width:36mm; ""
                                 src='https://barcode.tec-it.com/barcode.ashx?data=123-456-78-02'/>
                       </div>

                      <div style='text-align:center; padding-top: 5px;'>
	                    <img alt='Barcode Generator TEC-IT' 
	                          style=max-width:36mm; ""
                                 src='https://barcode.tec-it.com/barcode.ashx?data=123-456-78-03'/>
                       </div>

                    </body>
                    </html>"
            ;

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(reportHtml);
            await page.WaitForSelectorAsync("body");
            //var result = await page.GetContentAsync();

            //valid class for header and footer template
            //date formatted print date
            //title document title
            //url document location
            //pageNumber current page number
            //totalPages total pages in the document

            //page.PdfStreamAsync();
            //page.PdfDataAsync();

            await page.PdfAsync($"example_{DateTime.Now.ToString("yyMMddHHmmss")}.pdf", new PdfOptions
            {
                Format = PaperFormat.A4,
                Landscape = false,
                DisplayHeaderFooter = false,
                //HeaderTemplate = $"<div id='header-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; width:100%; z-index:1000;'>{logo}</div>",
                //FooterTemplate = $"<div id='footer-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; width:100%; z-index:1000;'><div style='float: right;'><span class='pageNumber'></span>/<span class='totalPages'></span></div></div>",
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "0cm",
                    Bottom = "0cm",
                    Left = "0cm",
                    Right = "0cm"
                },
                PrintBackground = true,
                OmitBackground = true,
            });

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

            var fontPath = System.IO.Path.Combine(appFolder.FontsFolder, "KhmerOS.ttf");
            byte[] fontBytes = System.IO.File.ReadAllBytes(fontPath);
            var base64FontString = Convert.ToBase64String(fontBytes);
            var khmerFont = "@font-face {font-family: 'KhmerOS'; src: url(data:application/font-ttf;charset=utf-8;base64," + base64FontString + ") format('truetype'); }";

            //var bgPath = System.IO.Path.Combine(appFolder.ImagesFolder, "cv-background.jpg");
            //byte[] bgImageBytes = System.IO.File.ReadAllBytes(bgPath);
            //var base64BackgroundImageString = Convert.ToBase64String(bgImageBytes);

            var marginTop = "2cm";
            var marginBottom = "1.5cm";
            var backgroundColor = "#fce0d4";

            var reportHtml = "<!DOCTTYPE html>" +
                             "<html>" +
                             "<head>" +
                             "<meta charset=\"UTF-8\">" +
                             "<title>ក្រុមហ៊ុន មេត្តា ខូអ៊ិលធីឌី Meta Co., Ltd.</title>" +
                             "<style type='text/css'>" +
                             khmerFont +
                             "body{padding:0; margin:0;}" +
                             "table {border-collapse: collapse; width: 100%}" +
                             "thead {display: table-header-group; break-inside: avoid;}" +
                             "tfoot {display: table-footer-group; break-inside: avoid;}" +
                             "th, td {border: 0px solid #333;}" +
                             //".page-background{position: fixed; top: 0; left: 0; width: 21cm; height: 29.7cm;  z-index:-1; background-repeat: repeat-y; background-image:url('data:image/jpg;base64,"+ base64BackgroundImageString + "')}" +
                             ".page-background{position: fixed; top: 0; left: 0; width: 360px; height: 29.7cm;  z-index:-1; background-color: " + backgroundColor + ";}" +
                             ".page>thead>tr>th {height: " + marginTop + "; }" +
                             ".page>tfoot>tr>th {height: " + marginBottom + "; background-color: transparent;}" +
                             ".page>thead>tr>th, .page>tfoot>tr>th, .page>tbody>tr>td {padding:0 1.5cm;}" +
                             "</style>" +
                             "</head>" +
                             "<body>" +
                             "<div class='page-background'>&nbsp;</div>" +
                             "<table class='page'>" +
                             $"<thead><tr><th style='text-align:left;'>{logo}</th></tr></thead>" +
                             "<tbody><tr><td>@Body</td></tr></tbody> " +
                             "<tfoot><tr><th></th></tr></tfoot>" +
                             "</table>" +
                             "</body>" +
                             "</html>";


            var bodyContent = "<div style='text-align:center;font-size:48px;'> <span style='font-family:KhmerOS; font-size: 18px; color:navy'> វិក្កយបត្រ </span><br> Invoice</div>" +
                             "<table>" +
                             "<thead>" +
                             "<tr>" +
                             "<th style='text-align:left;'>No</th>" +
                             "<th style='text-align:left;'>Name</th>" +
                             "<th style='text-align:right;'>Qty</th>" +
                             "</tr>" +
                             "</thead>" +
                             "<tbody>@Rows</tbody>" +
                             "</table>";

            var books = new List<string>()
            {
                "មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ)",
                "បង្រៀន​ដោយ៖ លោកគ្រូ ណន ដារី",
                "សាលា​បឋមសិក្សាគួច ម៉េងលីទួលបេង ខេត្ត កំពង់ចាម",
                "​តើក្នុងរាជ្យ ព្រះចន្ទរាជា ប្រទេសខ្មែរ ស្ថិតនៅចំណុះប្រទេសណា? (ពិន្ទុ ០.២៥)",
                "មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ) មេរៀនទី៩៖ ពេលវេលាជាមាសប្រាក់ (សំណេរ៖ ការតែងកំណាព្យបទពាក្យប្រាំពីរ)"
            };

            var rowsContent = "";

            //var dataRandom = Enumerable.Range(0, 10000);

            //dataRandom.AsParallel().ForAll(i =>
            //{
            //    var bookName = books[new Random().Next(books.Count)] + $" {i}";

            //    rowsContent += "<tr>" +
            //                 $"<td style='text-align:left;'>{i}</td>" +
            //                 $"<td style='text-align:left;'>{bookName}</td>" +
            //                 "<td style='text-align:right;'>10</td>" +
            //                 "</tr>";
            //});

            for (var i = 1; i <= 50000; i++)
            {
                var bookName = books[new Random().Next(books.Count)] + $" {i}";

                rowsContent += "<tr>" +
                             $"<td style='text-align:left;'>{i}</td>" +
                             $"<td style='text-align:left;'>{bookName}</td>" +
                             "<td style='text-align:right;'>10</td>" +
                             "</tr>";
            }

            reportHtml = reportHtml.Replace("@Body", bodyContent.Replace("@Rows", rowsContent));

            //using var browserFetcher = new BrowserFetcher();
            //await browserFetcher.DownloadAsync();
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
            //var result = await page.GetContentAsync();

            //valid class for header and footer template
            //date formatted print date
            //title document title
            //url document location
            //pageNumber current page number
            //totalPages total pages in the document

            //page.PdfStreamAsync();
            //page.PdfDataAsync();

            await page.PdfAsync($"example_{DateTime.Now.ToString("yyMMddHHmmss")}.pdf", new PdfOptions
            {
                Format = PaperFormat.A4,
                Landscape = false,
                DisplayHeaderFooter = true,
                //HeaderTemplate = $"<div id='header-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; width:100%; z-index:1000;'>{logo}</div>",
                FooterTemplate = $"<div id='footer-template' style='font-size:10px!important; color:#333; padding-left:1cm; padding-right: 1cm; width:100%; z-index:1000;'><div style='float: right;'><span class='pageNumber'></span>/<span class='totalPages'></span></div></div>",
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "0cm",
                    Bottom = "0cm",
                    Left = "0cm",
                    Right = "0cm"
                },
                PrintBackground = true,
                OmitBackground = true,
            });
        }
               
        public async Task TestHangfire()
        {
            await Task.Run(() =>
            {
                string jobId = string.Empty;
                

                for (var i = 0; i < 10; i++)
                {
                    if(jobId == string.Empty)
                    {
                        jobId = BackgroundJob.Schedule<ITestHangfireManager>(r => r.TestMethod(), TimeSpan.FromSeconds(30));
                    }
                    else
                    {
                        jobId = BackgroundJob.ContinueJobWith<ITestHangfireManager>(jobId, r => r.TestMethod());
                    } 
                }

                //var jobId = BackgroundJob.Schedule(() => _testHangfireManager.TestMethod(), TimeSpan.FromSeconds(5));
            });
        }

       
        public async Task<int> TestBulkInsert(int records)
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

            IEnumerable<TestModel> result;

            var sources = Enumerable.Range(0, records);
            int? tenantId = AbpSession.TenantId;
            long userId = AbpSession.UserId.Value;

            result = from i in sources.AsParallel()
                     let name = unitNameList[new Random().Next(unitNameList.Count)] + $" {i + 1}"
                     select
                     TestModel.Create(
                         tenantId,
                         userId,
                         name,
                         name,
                         Clock.Now,
                         Clock.Now);
            var _repository = IocManager.Instance.Resolve<IBiiSoftRepository<TestModel, Guid>>();

            await _repository.BulkInsertAsync(result.ToList());

            return records;
        }

        public async Task<List<TestModelOutput>> GetLinqSkipTake(string orderBy, int skip, int take)
        {
            var _repository = IocManager.Instance.Resolve<IBiiSoftRepository<TestModel, Guid>>();

            var query = _repository.GetAll().AsNoTracking()
                .Select(s => new TestModelOutput
                {
                    Id = s.Id,
                    Date = s.Date,
                    DateIndex = s.DateIndex,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    CreationTime = s.CreationTime,
                    CreatorUserId = s.CreatorUserId,
                    IsActive = s.IsActive,
                })
                .OrderBy(s => orderBy == "Date" ? s.Date : s.DateIndex)
                .Skip(skip).Take(take);

            var sql = query.ToQueryString();

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<string>> TimeZones()
        {
            return await Task.Run(() => { return TimezoneHelper.GetWindowsTimeZoneIds(); });
        }

    }
}
