﻿using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Screens
{
    public interface IScreenManager : IDefaultActiveValidateServiceBase<Screen, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
