using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace BiiSoft.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public Guid? ProfilePictureId { get; protected set; }
        public void SetProfilePicture(Guid? profilePictureId) => ProfilePictureId = profilePictureId;

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public bool ShouldChangePasswordOnNextLogin { get; set; }
        public DateTime? SignInTokenExpireTimeUtc { get; set; }
        public string SignInToken { get; set; }
        public string GoogleAuthenticatorKey { get; set; }
        public bool UseEmail { get; set; }

        /// <summary>
        /// This status will exclude user from maxcount limitation
        /// </summary>
        public bool IsDeactivate { get; set; }
       
        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
