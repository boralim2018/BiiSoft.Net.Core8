﻿using Abp.AutoMapper;
using BiiSoft.Authentication.External;

namespace BiiSoft.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
