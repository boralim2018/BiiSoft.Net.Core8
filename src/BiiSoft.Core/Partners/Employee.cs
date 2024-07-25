using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Extensions;

namespace BiiSoft.Partners
{
    public class Employee : PartnerBase
    {
       
        public bool SameAsPlaceOfBirth { get; protected set; }

        public Guid? ProfilePictureId { get; protected set; }
        public void SetProfilePicture(Guid? profilePictureId) { ProfilePictureId = profilePictureId; }



        public static Employee Create
            (
            int tenantId, 
            long? userId,
            string code,
            string name,
            string displayName,
            string surname,
            string displaySurname,
            bool displayNameFirst,
            string phoneNumber,
            string email,
            string website            
            )
        {
            return new Employee
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Code = code,
                Name = name,
                DisplayName = displayName,
                Surname = surname,
                DisplaySurname = displaySurname,
                DisplayNameFirst = displayNameFirst,
                PhoneNumber = phoneNumber,
                Email = email,
                Website = website,
                IsActive = true,
            };
        }

        public void Update
            (
            long? userId,
            string code,
            string name,
            string displayName,
            string surname,
            string displaySurname,
            bool displayNameFirst,
            string phoneNumber,
            string email,
            string website
            )
        {
            CreatorUserId = userId;
            CreationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;
            Surname = surname;
            DisplaySurname = displaySurname;
            DisplayNameFirst = displayNameFirst;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;      
        }

    }
}
