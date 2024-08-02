using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Entities;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Currencies
{
    public class Currency : DefaultNameActiveEntity<long>
    {
        [Required, MaxLength(5)]
        public string Code { get; protected set; }        
      
        [Required, MaxLength(5)]
        public string Symbol { get; protected set; }

        public static Currency Create (long? userId, string name, string displayName, string code, string symbol)
        {
            return new Currency
            {
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                Symbol = symbol,
                IsActive = true,
            };
        }
       
        public void Update(long? userId, string name, string displayName, string code, string symbol)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Code = code;
            Symbol = symbol;
        }
    }
}
