using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Entities;
using Abp.Domain.Entities;
using Abp.Timing;

namespace BiiSoft.Locations
{
    [Table("BiiLocations")]
    public class Location : LocationBase, IMayHaveTenant, ICodeEntity
    {
        public int? TenantId { get; set; }

        public static Location Create(
            int? tenantId,
            long UserId,
            string code,
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
                CreatorUserId = UserId,
                CreationTime = Clock.Now,
                Code = code,
                Name = name,
                DisplayName = displayName,
                Latitude = latitude,
                Longitude = longitude,
                IsActive = true
            };
        }

        public void Update(
            long UserId,
            string code,
            string name,
            string displayName,
            decimal? latitude,
            decimal? longitude
            )
        {
            LastModifierUserId = UserId;
            LastModificationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public abstract class LocationBase : CanModifyNameActiveEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }

        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string Code { get; protected set; }
        public void SetCode(string code) => Code = code;

        [Column(TypeName = "decimal(19,8)")]
        public decimal? Latitude { get; protected set; }
        [Column(TypeName = "decimal(19,8)")]
        public decimal? Longitude { get; protected set; }

    }

}
