using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;

namespace BiiSoft.Items
{
    [Table("BiiItemGroups")]
    public class ItemGroup : ItemFieldBase
    {
        public static ItemGroup Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new ItemGroup
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }
    }

    [Table("BiiItemBrands")]
    public class ItemBrand : ItemFieldBase
    {
        public static ItemBrand Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new ItemBrand
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }
    }

    [Table("BiiItemGrades")]
    public class ItemGrade : ItemFieldBase
    {
        public static ItemGrade Create(int tenantId, long userId, string name, string displayNmae, string code)
        {
            return new ItemGrade
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayNmae,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemModels")]
    public class ItemModel : ItemFieldBase
    {
        public static ItemModel Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new ItemModel
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemSizes")]
    public class ItemSize : ItemFieldBase
    {
        public static ItemSize Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new ItemSize
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemSeries")]
    public class ItemSeries : ItemFieldBase
    {
        public static ItemSeries Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new ItemSeries
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiColorPatterns")]
    public class ColorPattern : ItemFieldBase
    {
        public static ColorPattern Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new ColorPattern
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiCPUs")]
    public class CPU : ItemFieldBase
    { 
        public static CPU Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new CPU
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiRAMs")]
    public class RAM : ItemFieldBase
    {
        public static RAM Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new RAM
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiVGAs")]
    public class VGA : ItemFieldBase
    {
        public static VGA Create(int? tenantId, long userId, string name, string displayname, string code)
        {
            return new VGA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayname,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiScreens")]
    public class Screen : ItemFieldBase
    {
        public static Screen Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new Screen
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiHDDs")]
    public class HDD : ItemFieldBase
    {
        public static HDD Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new HDD
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiCameras")]
    public class Camera : ItemFieldBase
    {
        public static Camera Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new Camera
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiBatteries")]
    public class Battery : ItemFieldBase
    {
        public static Battery Create(int? tenantId, long userId, string name, string displayName, string code)
        {
            return new Battery
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemFieldAs")]
    public class FieldA : ItemFieldBase
    {
        public static FieldA Create(int tenantId, long userId, string name, string dispalyName, string code)
        {
            return new FieldA
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = dispalyName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemFieldBs")]
    public class FieldB : ItemFieldBase
    {
        public static FieldB Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new FieldB
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }

    [Table("BiiItemFieldCs")]
    public class FieldC : ItemFieldBase
    {
        public static FieldC Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new FieldC
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId= userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }
}
