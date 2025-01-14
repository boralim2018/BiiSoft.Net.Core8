using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiLocations")]
    public class Location : CanModifyNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }


        [Column(TypeName = "decimal(19,8)")]
        public decimal? Latitude { get; protected set; }
        [Column(TypeName = "decimal(19,8)")]
        public decimal? Longitude { get; protected set; }

        public static Location Create(
            int tenantId,
            long userId,
            string name,
            string displayName,
            decimal? latitude,
            decimal? longitude
            )
        {
            return new Location
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Latitude = latitude,
                Longitude = longitude,
                IsActive = true
            };
        }

        public void Update(
            long userId,
            string name,
            string displayName,
            decimal? latitude,
            decimal? longitude
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

}
