using Abp.Domain.Services;
using BiiSoft.BFiles.Dto;
using System.Threading.Tasks;

namespace BiiSoft.Excels
{
    public interface IExcelManager : IDomainService
    {
        Task<ExportFileOutput> ExportExcelTemplateAsync(ExportFileInput input);
        Task<ExportFileOutput> ExportExcelAsync(ExportDataFileInput input);
    }
   
}
