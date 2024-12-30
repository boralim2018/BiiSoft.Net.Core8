using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Timing;
using Abp.UI;
using BiiSoft.FileStorages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BiiSoft.Extensions;
using BiiSoft.Entities;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Items;
using BiiSoft.ColorPatterns;

namespace BiiSoft.CPUs
{
    public class CPUManager : ItemFieldManagerBase<CPU>, ICPUManager
    {
        public CPUManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<CPU, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "CPU";

        protected override CPU CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return CPU.Create(tenantId, userId, name, displayName, code);
        }
    }
}