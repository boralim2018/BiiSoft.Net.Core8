using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Entities;
using System.ComponentModel;

namespace BiiSoft.Items
{

    [Table("BiiCPUs")]
    public class CPU : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
      
        public static CPU Create(int? tenantId, long userId, string name, string displayName)
        {
            return new CPU
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiRAMs")]
    public class RAM : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static RAM Create(int? tenantId, long userId, string name, string displayName)
        {
            return new RAM
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiVGAs")]
    public class VGA : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        
        public static VGA Create(int? tenantId, long userId, string name, string displayname)
        {
            return new VGA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayname,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiScreens")]
    public class Screen : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static Screen Create(int tenantId, long userId, string name, string displayName)
        {
            return new Screen
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiHDDs")]
    public class HDD : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       

        public static HDD Create(int? tenantId, long userId, string name, string displayName)
        {
            return new HDD
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiCameras")]
    public class Camera : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static Camera Create(int? tenantId, long userId, string name, string displayName)
        {
            return new Camera
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiBatteries")]
    public class Battery : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       

        public static Battery Create(int? tenantId, long userId, string name, string displayName)
        {
            return new Battery
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }
    }

    [Table("BiiItemFieldAs")]
    public class FieldA : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static FieldA Create(int tenantId, long userId, string name, string dispalyName)
        {
            return new FieldA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = dispalyName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }

    }

    [Table("BiiItemFieldBs")]
    public class FieldB : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static FieldB Create(int tenantId, long userId, string name, string displayName)
        {
            return new FieldB
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }

    }

    [Table("BiiItemFieldCs")]
    public class FieldC : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
       
        public static FieldC Create(int tenantId, long userId, string name, string displayName)
        {
            return new FieldC
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId= userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
        }
    }
}
