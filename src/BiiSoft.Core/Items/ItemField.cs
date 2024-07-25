using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Extensions;
using BiiSoft.Entities;

namespace BiiSoft.Items
{

    [Table("BiiCPUs")]
    public class CPU : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static CPU Create(int? tenantId, long userId, string name)
        {
            return new CPU
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiRAMs")]
    public class RAM : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static RAM Create(int? tenantId, long userId, string name)
        {
            return new RAM
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiVGAs")]
    public class VGA : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static VGA Create(int? tenantId, long userId, string name)
        {
            return new VGA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiScreens")]
    public class Screen : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public static Screen Create(int tenantId, long userId, string name)
        {
            return new Screen
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiStorages")]
    public class Storage : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static Storage Create(int? tenantId, long userId, string name)
        {
            return new Storage
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiCameras")]
    public class Camera : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static Camera Create(int? tenantId, long userId, string name)
        {
            return new Camera
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiBatteries")]
    public class Battery : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static Battery Create(int? tenantId, long userId, string name)
        {
            return new Battery
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiOSs")]
    public class OS : DefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static OS Create(int? tenantId, long userId, string name)
        {
            return new OS
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }
    }

    [Table("BiiItemFieldAs")]
    public class FieldA : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public static FieldA Create(int tenantId, long userId, string name)
        {
            return new FieldA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }

    }

    [Table("BiiItemFieldBs")]
    public class FieldB : AuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        [StringLength(BiiSoftConsts.MaxLengthLongCode, ErrorMessage = BiiSoftConsts.MaxLengthLongCodeErrorMessage)]
        public string Name { get; private set; }

        public bool IsActive { get; private set; }
        public void Enable(bool isActive) => IsActive = isActive;

        public static FieldB Create(int tenantId, long userId, string name)
        {
            return new FieldB
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
        }

    }

    [Table("BiiItemFieldCs")]
    public class FieldC : AuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        [StringLength(BiiSoftConsts.MaxLengthLongCode, ErrorMessage = BiiSoftConsts.MaxLengthLongCodeErrorMessage)]
        public string Name { get; private set; }

        public bool IsActive { get; private set; }
        public void Enable(bool isActive) => IsActive = isActive;

        public static FieldC Create(int tenantId, long userId, string name)
        {
            return new FieldC
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId= userId,
                CreationTime = Clock.Now,
                Name = name,
                IsActive = true
            };
        }

        public void Update(long userId, string name)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
        }
    }
}
