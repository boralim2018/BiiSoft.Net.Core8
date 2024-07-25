using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace BiiSoft.Security.Recaptcha
{
    public class RecaptchaValidator : BiiSoftDomainServiceBase, IRecaptchaValidator, ITransientDependency
    {
        public const string RecaptchaResponseKey = "g-recaptcha-response";

        private readonly IRecaptchaValidationService _recaptchaValidationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecaptchaValidator(IRecaptchaValidationService recaptchaValidationService, IHttpContextAccessor httpContextAccessor)
        {
            _recaptchaValidationService = recaptchaValidationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(string captchaResponse)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("RecaptchaValidator should be used in a valid HTTP context!");
            }

            if (captchaResponse.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L("CanNotBeEmpty","Captcha"));
            }

            try
            {
                await _recaptchaValidationService.ValidateResponseAsync(
                    captchaResponse,
                    _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString()
                );
            }
            catch (RecaptchaValidationException)
            {
                throw new UserFriendlyException(L("IncorrectCaptchaAnswer"));
            }
        }
    }
}
