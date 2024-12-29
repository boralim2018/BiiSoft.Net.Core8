﻿using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Cameras
{
    public interface ICameraManager : IDefaultActiveValidateServiceBase<Camera, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
