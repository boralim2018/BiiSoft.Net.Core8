using Abp.Timing;
using BiiSoft.ContactInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiiSoft.Partners
{
    public class PartnerContactPerson : ContactPersonBase
    {
        public Guid PartnerId { get; protected set; }
        public Partner Partner { get; protected set; }

        public static PartnerContactPerson Create
            (
            int tenantId,
            long? userId,
            Guid partnerId,
            string title,
            string name,
            string displayName,
            string surname,
            string displaySurname,
            bool displayNameFirst,
            string phoneNumber,
            string email
            )
        {
            return new PartnerContactPerson
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                PartnerId = partnerId,
                Title = title,
                Name = name,
                DisplayName = displayName,
                Surname = surname,
                DisplaySurname = displaySurname,
                DisplayNameFirst = displayNameFirst,
                PhoneNumber = phoneNumber,
                Email = email

            };
        }


        public void Update
            (
            int tenantId,
            long? userId,
            Guid partnerId,
            string title,
            string name,
            string displayName,
            string surname,
            string displaySurname,
            bool displayNameFirst,
            string phoneNumber,
            string email
            )
        {            
               
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            PartnerId = partnerId;
            Title = title;
            Name = name;
            DisplayName = displayName;
            Surname = surname;
            DisplaySurname = displaySurname;
            DisplayNameFirst = displayNameFirst;
            PhoneNumber = phoneNumber;
            Email = email;
        }

    }
}
