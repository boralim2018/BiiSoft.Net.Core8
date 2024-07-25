namespace BiiSoft.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetDefaultTemplate(int? tenantId);
        string GetActivationTemplate();
    }
}
