using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Users;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Branches;
using BiiSoft.ContactInfo;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Excels;
using BiiSoft.Extensions;
using BiiSoft.Items.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;

namespace BiiSoft.Items
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class ItemAppService : BiiSoftAppServiceBase, IItemAppService
    {
        private readonly IItemManager _itemManager;
        private readonly IBiiSoftRepository<Item, Guid> _itemRepository;
        private readonly IBiiSoftRepository<ItemSetting, Guid> _itemSettingRepository;
        private readonly IBiiSoftRepository<ItemFieldSetting, Guid> _itemFieldSettingRepository;
        private readonly IContactAddressManager _contactAddressManager;
        private readonly IBiiSoftRepository<ContactAddress, Guid> _contactAddressRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        private readonly IItemSettingManager _itemSettingManager;
        private readonly IItemFieldSettingManager _itemFieldSettingManager;

        public ItemAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IItemManager itemManager,
            IItemSettingManager itemSettingManager,
            IItemFieldSettingManager itemFieldSettingManager,
            IBiiSoftRepository<Item, Guid> itemRepository,
            IBiiSoftRepository<ItemSetting, Guid> itemSettingRepository,
            IBiiSoftRepository<ItemFieldSetting, Guid> itemFieldSettingRepository,
            IContactAddressManager contactAddressManager,
            IBiiSoftRepository<ContactAddress, Guid> contactAddressRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _itemManager=itemManager;
            _itemSettingManager = itemSettingManager;
            _itemFieldSettingManager = itemFieldSettingManager;
            _itemRepository =itemRepository;
            _itemSettingRepository = itemSettingRepository;
            _itemFieldSettingRepository = itemFieldSettingRepository;
            _contactAddressManager =contactAddressManager;
            _contactAddressRepository=contactAddressRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_Create)]
        public async Task<Guid> Create(CreateUpdateItemInputDto input)
        {   
            var entity = MapEntity<Item, Guid>(input); 

            CheckErrors(await _itemManager.InsertAsync(entity));

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _itemManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _itemManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _itemManager.EnableAsync(entity));
        }


        [AbpAuthorize(PermissionNames.Pages_Find_Items)]
        public async Task<PagedResultDto<FindItemDto>> Find(PageItemInputDto input)
        {
            var userId = AbpSession.UserId;

            var query = _itemRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.ItemTypeFilter != null && !input.ItemTypeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemTypeFilter.Exclude && (!input.ItemTypeFilter.Ids.Contains(s.ItemType))) ||
                            (!input.ItemTypeFilter.Exclude && input.ItemTypeFilter.Ids.Contains(s.ItemType)))
                        .WhereIf(input.ItemCategoryFilter != null && !input.ItemCategoryFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemCategoryFilter.Exclude && (!input.ItemCategoryFilter.Ids.Contains(s.ItemCategory))) ||
                            (!input.ItemCategoryFilter.Exclude && input.ItemCategoryFilter.Ids.Contains(s.ItemCategory)))
                        .WhereIf(input.UnitFilter != null && !input.UnitFilter.Ids.IsNullOrEmpty(), s =>
                            (input.UnitFilter.Exclude && (!s.UnitId.HasValue || !input.UnitFilter.Ids.Contains(s.UnitId.Value))) ||
                            (!input.UnitFilter.Exclude && input.UnitFilter.Ids.Contains(s.UnitId.Value)))
                        .WhereIf(input.ItemGroupFilter != null && !input.ItemGroupFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemGroupFilter.Exclude && (!s.ItemGroupId.HasValue || !input.ItemGroupFilter.Ids.Contains(s.ItemGroupId.Value))) ||
                            (!input.ItemGroupFilter.Exclude && input.ItemGroupFilter.Ids.Contains(s.ItemGroupId.Value)))
                        .WhereIf(input.ItemBrandFilter != null && !input.ItemBrandFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemBrandFilter.Exclude && (!s.ItemBrandId.HasValue || !input.ItemBrandFilter.Ids.Contains(s.ItemBrandId.Value))) ||
                            (!input.ItemBrandFilter.Exclude && input.ItemBrandFilter.Ids.Contains(s.ItemBrandId.Value)))
                        .WhereIf(input.ItemGradeFilter != null && !input.ItemGradeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemGradeFilter.Exclude && (!s.ItemGradeId.HasValue || !input.ItemGradeFilter.Ids.Contains(s.ItemGradeId.Value))) ||
                            (!input.ItemGradeFilter.Exclude && input.ItemGradeFilter.Ids.Contains(s.ItemGradeId.Value)))
                        .WhereIf(input.ItemModelFilter != null && !input.ItemModelFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemModelFilter.Exclude && (!s.ItemModelId.HasValue || !input.ItemModelFilter.Ids.Contains(s.ItemModelId.Value))) ||
                            (!input.ItemModelFilter.Exclude && input.ItemModelFilter.Ids.Contains(s.ItemModelId.Value)))
                        .WhereIf(input.ItemSizeFilter != null && !input.ItemSizeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemSizeFilter.Exclude && (!s.ItemSizeId.HasValue || !input.ItemSizeFilter.Ids.Contains(s.ItemSizeId.Value))) ||
                            (!input.ItemSizeFilter.Exclude && input.ItemSizeFilter.Ids.Contains(s.ItemSizeId.Value)))
                        .WhereIf(input.ItemSeriesFilter != null && !input.ItemSeriesFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemSeriesFilter.Exclude && (!s.ItemSeriesId.HasValue || !input.ItemSeriesFilter.Ids.Contains(s.ItemSeriesId.Value))) ||
                            (!input.ItemSeriesFilter.Exclude && input.ItemSeriesFilter.Ids.Contains(s.ItemSeriesId.Value)))
                        .WhereIf(input.ColorPatternFilter != null && !input.ColorPatternFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ColorPatternFilter.Exclude && (!s.ColorPatternId.HasValue || !input.ColorPatternFilter.Ids.Contains(s.ColorPatternId.Value))) ||
                            (!input.ColorPatternFilter.Exclude && input.ColorPatternFilter.Ids.Contains(s.ColorPatternId.Value)))
                        .WhereIf(input.CPUFilter != null && !input.CPUFilter.Ids.IsNullOrEmpty(), s =>
                            (input.CPUFilter.Exclude && (!s.CPUId.HasValue || !input.CPUFilter.Ids.Contains(s.CPUId.Value))) ||
                            (!input.CPUFilter.Exclude && input.CPUFilter.Ids.Contains(s.CPUId.Value)))
                        .WhereIf(input.RAMFilter != null && !input.RAMFilter.Ids.IsNullOrEmpty(), s =>
                            (input.RAMFilter.Exclude && (!s.RAMId.HasValue || !input.RAMFilter.Ids.Contains(s.RAMId.Value))) ||
                            (!input.RAMFilter.Exclude && input.RAMFilter.Ids.Contains(s.RAMId.Value)))
                        .WhereIf(input.VGAFilter != null && !input.VGAFilter.Ids.IsNullOrEmpty(), s =>
                            (input.VGAFilter.Exclude && (!s.VGAId.HasValue || !input.VGAFilter.Ids.Contains(s.VGAId.Value))) ||
                            (!input.VGAFilter.Exclude && input.VGAFilter.Ids.Contains(s.VGAId.Value)))
                        .WhereIf(input.HDDFilter != null && !input.HDDFilter.Ids.IsNullOrEmpty(), s =>
                            (input.HDDFilter.Exclude && (!s.HDDId.HasValue || !input.HDDFilter.Ids.Contains(s.HDDId.Value))) ||
                            (!input.HDDFilter.Exclude && input.HDDFilter.Ids.Contains(s.HDDId.Value)))
                        .WhereIf(input.ScreenFilter != null && !input.ScreenFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ScreenFilter.Exclude && (!s.ScreenId.HasValue || !input.ScreenFilter.Ids.Contains(s.ScreenId.Value))) ||
                            (!input.ScreenFilter.Exclude && input.ScreenFilter.Ids.Contains(s.ScreenId.Value)))
                        .WhereIf(input.CameraFilter != null && !input.CameraFilter.Ids.IsNullOrEmpty(), s =>
                            (input.CameraFilter.Exclude && (!s.CameraId.HasValue || !input.CameraFilter.Ids.Contains(s.CameraId.Value))) ||
                            (!input.CameraFilter.Exclude && input.CameraFilter.Ids.Contains(s.CameraId.Value)))
                        .WhereIf(input.BatteryFilter != null && !input.BatteryFilter.Ids.IsNullOrEmpty(), s =>
                            (input.BatteryFilter.Exclude && (!s.BatteryId.HasValue || !input.BatteryFilter.Ids.Contains(s.BatteryId.Value))) ||
                            (!input.BatteryFilter.Exclude && input.BatteryFilter.Ids.Contains(s.BatteryId.Value)))
                        .WhereIf(input.FieldAFilter != null && !input.FieldAFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldAFilter.Exclude && (!s.FieldAId.HasValue || !input.FieldAFilter.Ids.Contains(s.FieldAId.Value))) ||
                            (!input.FieldAFilter.Exclude && input.FieldAFilter.Ids.Contains(s.FieldAId.Value)))
                        .WhereIf(input.FieldBFilter != null && !input.FieldBFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldBFilter.Exclude && (!s.FieldBId.HasValue || !input.FieldBFilter.Ids.Contains(s.FieldBId.Value))) ||
                            (!input.FieldBFilter.Exclude && input.FieldBFilter.Ids.Contains(s.FieldBId.Value)))
                        .WhereIf(input.FieldCFilter != null && !input.FieldCFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldCFilter.Exclude && (!s.FieldCId.HasValue || !input.FieldCFilter.Ids.Contains(s.FieldCId.Value))) ||
                            (!input.FieldCFilter.Exclude && input.FieldCFilter.Ids.Contains(s.FieldCId.Value)))
                        .WhereIf(input.Creators != null && !input.Creators.Ids.IsNullOrEmpty(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && !input.Modifiers.Ids.IsNullOrEmpty(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                       

            var totalCount = await query.CountAsync();
            var items = new List<FindItemDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindItemDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    Code = l.Code,
                    IsActive = l.IsActive
                 });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<FindItemDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_View, PermissionNames.Pages_Setup_Items_List_Edit)]
        public async Task<ItemDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _itemRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new ItemDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Code = l.Code,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            ItemType = l.ItemType,
                            ItemTypeName = l.ItemType.GetName(),
                            ItemCategory = l.ItemCategory,
                            ItemCategoryName = l.ItemCategory.GetName(),
                            GrossWeight = l.GrossWeight,
                            NetWeight = l.NetWeight,
                            Area = l.Area,
                            Diameter = l.Diameter,
                            Height = l.Height,
                            Width = l.Width,
                            Length = l.Length,
                            Volume = l.Volume,
                            MaxStock = l.MaxStock,
                            MinStock = l.MinStock,
                            ReorderStock = l.ReorderStock,
                            TrackBatchNo = l.TrackBatchNo,
                            TrackExpired = l.TrackExpired,
                            TrackInventoryStatus = l.TrackInventoryStatus,
                            TrackSerial = l.TrackSerial,
                            AreaUnit = l.AreaUnit,
                            AreaUnitName = l.AreaUnit.GetName(),
                            LengthUnit = l.LengthUnit,
                            LengthUnitName = l.LengthUnit.GetName(),
                            VolumeUnit = l.VolumeUnit,
                            VolumeUnitName = l.VolumeUnit.GetName(),
                            WeightUnit = l.WeightUnit,
                            WeightUnitName = l.WeightUnit.GetName(),
                            Description = l.Description,
                            UnitId = l.UnitId,
                            ItemGroupId = l.ItemGroupId,
                            ItemBrandId = l.ItemBrandId,
                            ItemGradeId = l.ItemGradeId,
                            ItemModelId = l.ItemModelId,
                            ItemSeriesId = l.ItemSeriesId,
                            ItemSizeId = l.ItemSizeId,
                            ImageId = l.ImageId,
                            ColorPatternId = l.ColorPatternId,
                            CPUId = l.CPUId,
                            RAMId = l.RAMId,
                            VGAId = l.VGAId,
                            HDDId = l.HDDId,
                            CameraId = l.CameraId,
                            BatteryId = l.BatteryId,
                            ScreenId = l.ScreenId,
                            FieldAId = l.FieldAId,
                            FieldBId = l.FieldBId,
                            FieldCId = l.FieldCId,
                            InventoryAccountId = l.InventoryAccountId,
                            PurchaseAccountId = l.PurchaseAccountId,
                            SaleAccountId = l.SaleAccountId,
                            UnitName = !l.UnitId.HasValue ? "" : isDefaultLanguage ? l.Unit.Name : l.Unit.DisplayName,
                            ItemGroupName = !l.ItemGroupId.HasValue ? "" : isDefaultLanguage ? l.ItemGroup.Name : l.ItemGroup.DisplayName,
                            ItemBrandName = !l.ItemBrandId.HasValue ? "" : isDefaultLanguage ? l.ItemBrand.Name : l.ItemBrand.DisplayName,
                            ItemGradeName = !l.ItemGradeId.HasValue ? "" : isDefaultLanguage ? l.ItemGrade.Name : l.ItemGrade.DisplayName,
                            ItemModelName = !l.ItemModelId.HasValue ? "" : isDefaultLanguage ? l.ItemModel.Name : l.ItemModel.DisplayName,
                            ItemSizeName = !l.ItemSizeId.HasValue ? "" : isDefaultLanguage ? l.ItemSize.Name : l.ItemSize.DisplayName,
                            ItemSeriesName = !l.ItemSeriesId.HasValue ? "" : isDefaultLanguage ? l.ItemSeries.Name : l.ItemSeries.DisplayName,
                            ColorPatternName = !l.ColorPatternId.HasValue ? "" : isDefaultLanguage ? l.ColorPattern.Name : l.ColorPattern.DisplayName,
                            CPUName = !l.CPUId.HasValue ? "" : isDefaultLanguage ? l.CPU.Name : l.CPU.DisplayName,
                            RAMName = !l.RAMId.HasValue ? "" : isDefaultLanguage ? l.RAM.Name : l.RAM.DisplayName,
                            VGAName = !l.VGAId.HasValue ? "" : isDefaultLanguage ? l.VGA.Name : l.VGA.DisplayName,
                            HDDName = !l.HDDId.HasValue ? "" : isDefaultLanguage ? l.HDD.Name : l.HDD.DisplayName,
                            ScreenName = !l.ScreenId.HasValue ? "" : isDefaultLanguage ? l.Screen.Name : l.Screen.DisplayName,
                            CameraName = !l.CameraId.HasValue ? "" : isDefaultLanguage ? l.Camera.Name : l.Camera.DisplayName,
                            BatteryName = !l.BatteryId.HasValue ? "" : isDefaultLanguage ? l.Battery.Name : l.Battery.DisplayName,
                            FieldAName = !l.FieldAId.HasValue ? "" : isDefaultLanguage ? l.FieldA.Name : l.FieldA.DisplayName,
                            FieldBName = !l.FieldBId.HasValue ? "" : isDefaultLanguage ? l.FieldB.Name : l.FieldB.DisplayName,
                            FieldCName = !l.FieldCId.HasValue ? "" : isDefaultLanguage ? l.FieldC.Name : l.FieldC.DisplayName,
                            InventoryAccountName = !l.InventoryAccountId.HasValue ? "" : isDefaultLanguage ? l.InventoryAccount.Name : l.InventoryAccount.DisplayName,
                            PurchaseAccountName = !l.PurchaseAccountId.HasValue ? "" : isDefaultLanguage ? l.PurchaseAccount.Name : l.PurchaseAccount.DisplayName,
                            SaleAccountName = !l.SaleAccountId.HasValue ? "" : isDefaultLanguage ? l.SaleAccount.Name : l.SaleAccount.DisplayName,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModifierUserId = l.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                            ItemZones = l.ItemZones.Select(s => new ItemZoneDto
                            {
                                Id = s.Id,
                                WarehouseId = s.Zone.WarehouseId,
                                ZoneId = s.ZoneId,
                                ZoneName = isDefaultLanguage ? s.Zone.Name : s.Zone.DisplayName,
                            }).ToList()
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _itemManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List)]
        public async Task<PagedResultDto<ItemListDto>> GetList(PageItemInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<ItemListDto>> GetListHelper(PageItemInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();
            var userId = AbpSession.UserId;

            var query = _itemRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.ItemTypeFilter != null && !input.ItemTypeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemTypeFilter.Exclude && (!input.ItemTypeFilter.Ids.Contains(s.ItemType))) ||
                            (!input.ItemTypeFilter.Exclude && input.ItemTypeFilter.Ids.Contains(s.ItemType)))
                        .WhereIf(input.ItemCategoryFilter != null && !input.ItemCategoryFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemCategoryFilter.Exclude && (!input.ItemCategoryFilter.Ids.Contains(s.ItemCategory))) ||
                            (!input.ItemCategoryFilter.Exclude && input.ItemCategoryFilter.Ids.Contains(s.ItemCategory)))
                        .WhereIf(input.UnitFilter != null && !input.UnitFilter.Ids.IsNullOrEmpty(), s =>
                            (input.UnitFilter.Exclude && (!s.UnitId.HasValue || !input.UnitFilter.Ids.Contains(s.UnitId.Value))) ||
                            (!input.UnitFilter.Exclude && input.UnitFilter.Ids.Contains(s.UnitId.Value)))
                        .WhereIf(input.ItemGroupFilter != null && !input.ItemGroupFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemGroupFilter.Exclude && (!s.ItemGroupId.HasValue || !input.ItemGroupFilter.Ids.Contains(s.ItemGroupId.Value))) ||
                            (!input.ItemGroupFilter.Exclude && input.ItemGroupFilter.Ids.Contains(s.ItemGroupId.Value)))
                        .WhereIf(input.ItemBrandFilter != null && !input.ItemBrandFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemBrandFilter.Exclude && (!s.ItemBrandId.HasValue || !input.ItemBrandFilter.Ids.Contains(s.ItemBrandId.Value))) ||
                            (!input.ItemBrandFilter.Exclude && input.ItemBrandFilter.Ids.Contains(s.ItemBrandId.Value)))
                        .WhereIf(input.ItemGradeFilter != null && !input.ItemGradeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemGradeFilter.Exclude && (!s.ItemGradeId.HasValue || !input.ItemGradeFilter.Ids.Contains(s.ItemGradeId.Value))) ||
                            (!input.ItemGradeFilter.Exclude && input.ItemGradeFilter.Ids.Contains(s.ItemGradeId.Value)))
                        .WhereIf(input.ItemModelFilter != null && !input.ItemModelFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemModelFilter.Exclude && (!s.ItemModelId.HasValue || !input.ItemModelFilter.Ids.Contains(s.ItemModelId.Value))) ||
                            (!input.ItemModelFilter.Exclude && input.ItemModelFilter.Ids.Contains(s.ItemModelId.Value)))
                        .WhereIf(input.ItemSizeFilter != null && !input.ItemSizeFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemSizeFilter.Exclude && (!s.ItemSizeId.HasValue || !input.ItemSizeFilter.Ids.Contains(s.ItemSizeId.Value))) ||
                            (!input.ItemSizeFilter.Exclude && input.ItemSizeFilter.Ids.Contains(s.ItemSizeId.Value)))
                        .WhereIf(input.ItemSeriesFilter != null && !input.ItemSeriesFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ItemSeriesFilter.Exclude && (!s.ItemSeriesId.HasValue || !input.ItemSeriesFilter.Ids.Contains(s.ItemSeriesId.Value))) ||
                            (!input.ItemSeriesFilter.Exclude && input.ItemSeriesFilter.Ids.Contains(s.ItemSeriesId.Value)))
                        .WhereIf(input.ColorPatternFilter != null && !input.ColorPatternFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ColorPatternFilter.Exclude && (!s.ColorPatternId.HasValue || !input.ColorPatternFilter.Ids.Contains(s.ColorPatternId.Value))) ||
                            (!input.ColorPatternFilter.Exclude && input.ColorPatternFilter.Ids.Contains(s.ColorPatternId.Value)))
                        .WhereIf(input.CPUFilter != null && !input.CPUFilter.Ids.IsNullOrEmpty(), s =>
                            (input.CPUFilter.Exclude && (!s.CPUId.HasValue || !input.CPUFilter.Ids.Contains(s.CPUId.Value))) ||
                            (!input.CPUFilter.Exclude && input.CPUFilter.Ids.Contains(s.CPUId.Value)))
                        .WhereIf(input.RAMFilter != null && !input.RAMFilter.Ids.IsNullOrEmpty(), s =>
                            (input.RAMFilter.Exclude && (!s.RAMId.HasValue || !input.RAMFilter.Ids.Contains(s.RAMId.Value))) ||
                            (!input.RAMFilter.Exclude && input.RAMFilter.Ids.Contains(s.RAMId.Value)))
                        .WhereIf(input.VGAFilter != null && !input.VGAFilter.Ids.IsNullOrEmpty(), s =>
                            (input.VGAFilter.Exclude && (!s.VGAId.HasValue || !input.VGAFilter.Ids.Contains(s.VGAId.Value))) ||
                            (!input.VGAFilter.Exclude && input.VGAFilter.Ids.Contains(s.VGAId.Value)))
                        .WhereIf(input.HDDFilter != null && !input.HDDFilter.Ids.IsNullOrEmpty(), s =>
                            (input.HDDFilter.Exclude && (!s.HDDId.HasValue || !input.HDDFilter.Ids.Contains(s.HDDId.Value))) ||
                            (!input.HDDFilter.Exclude && input.HDDFilter.Ids.Contains(s.HDDId.Value)))
                        .WhereIf(input.ScreenFilter != null && !input.ScreenFilter.Ids.IsNullOrEmpty(), s =>
                            (input.ScreenFilter.Exclude && (!s.ScreenId.HasValue || !input.ScreenFilter.Ids.Contains(s.ScreenId.Value))) ||
                            (!input.ScreenFilter.Exclude && input.ScreenFilter.Ids.Contains(s.ScreenId.Value)))
                        .WhereIf(input.CameraFilter != null && !input.CameraFilter.Ids.IsNullOrEmpty(), s =>
                            (input.CameraFilter.Exclude && (!s.CameraId.HasValue || !input.CameraFilter.Ids.Contains(s.CameraId.Value))) ||
                            (!input.CameraFilter.Exclude && input.CameraFilter.Ids.Contains(s.CameraId.Value)))
                        .WhereIf(input.BatteryFilter != null && !input.BatteryFilter.Ids.IsNullOrEmpty(), s =>
                            (input.BatteryFilter.Exclude && (!s.BatteryId.HasValue || !input.BatteryFilter.Ids.Contains(s.BatteryId.Value))) ||
                            (!input.BatteryFilter.Exclude && input.BatteryFilter.Ids.Contains(s.BatteryId.Value)))
                        .WhereIf(input.FieldAFilter != null && !input.FieldAFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldAFilter.Exclude && (!s.FieldAId.HasValue || !input.FieldAFilter.Ids.Contains(s.FieldAId.Value))) ||
                            (!input.FieldAFilter.Exclude && input.FieldAFilter.Ids.Contains(s.FieldAId.Value)))
                        .WhereIf(input.FieldBFilter != null && !input.FieldBFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldBFilter.Exclude && (!s.FieldBId.HasValue || !input.FieldBFilter.Ids.Contains(s.FieldBId.Value))) ||
                            (!input.FieldBFilter.Exclude && input.FieldBFilter.Ids.Contains(s.FieldBId.Value)))
                        .WhereIf(input.FieldCFilter != null && !input.FieldCFilter.Ids.IsNullOrEmpty(), s =>
                            (input.FieldCFilter.Exclude && (!s.FieldCId.HasValue || !input.FieldCFilter.Ids.Contains(s.FieldCId.Value))) ||
                            (!input.FieldCFilter.Exclude && input.FieldCFilter.Ids.Contains(s.FieldCId.Value)))
                        .WhereIf(input.Creators != null && !input.Creators.Ids.IsNullOrEmpty(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && !input.Modifiers.Ids.IsNullOrEmpty(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                      

            var totalCount = await query.CountAsync();
            var items = new List<ItemListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new ItemListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    ItemType = l.ItemType,
                    ItemTypeName = l.ItemType.GetName(),
                    ItemCategory = l.ItemCategory,
                    ItemCategoryName = l.ItemCategory.GetName(),
                    GrossWeight = l.GrossWeight,
                    NetWeight = l.NetWeight,
                    Area = l.Area,
                    Diameter = l.Diameter,
                    Height = l.Height,
                    Width = l.Width,
                    Length = l.Length,
                    Volume = l.Volume,
                    MaxStock = l.MaxStock,
                    MinStock = l.MinStock,
                    ReorderStock = l.ReorderStock,
                    TrackBatchNo = l.TrackBatchNo,
                    TrackExpired = l.TrackExpired,
                    TrackInventoryStatus = l.TrackInventoryStatus,
                    TrackSerial = l.TrackSerial,
                    AreaUnit = l.AreaUnit,
                    AreaUnitName = l.AreaUnit.GetName(),
                    LengthUnit = l.LengthUnit,
                    LengthUnitName = l.LengthUnit.GetName(),
                    VolumeUnit = l.VolumeUnit,
                    VolumeUnitName = l.VolumeUnit.GetName(),
                    WeightUnit = l.WeightUnit,
                    WeightUnitName = l.WeightUnit.GetName(),
                    Description = l.Description,
                    UnitName = !l.UnitId.HasValue ? "" : isDefaultLanguage ? l.Unit.Name : l.Unit.DisplayName,
                    ItemGroupName = !l.ItemGroupId.HasValue ? "" : isDefaultLanguage ? l.ItemGroup.Name : l.ItemGroup.DisplayName,
                    ItemBrandName = !l.ItemBrandId.HasValue ? "" : isDefaultLanguage ? l.ItemBrand.Name : l.ItemBrand.DisplayName,
                    ItemGradeName = !l.ItemGradeId.HasValue ? "" : isDefaultLanguage ? l.ItemGrade.Name : l.ItemGrade.DisplayName,
                    ItemModelName = !l.ItemModelId.HasValue ? "" : isDefaultLanguage ? l.ItemModel.Name : l.ItemModel.DisplayName,
                    ItemSizeName = !l.ItemSizeId.HasValue ? "" : isDefaultLanguage ? l.ItemSize.Name : l.ItemSize.DisplayName,
                    ItemSeriesName = !l.ItemSeriesId.HasValue ? "" : isDefaultLanguage ? l.ItemSeries.Name : l.ItemSeries.DisplayName,
                    ColorPatternName = !l.ColorPatternId.HasValue ? "" : isDefaultLanguage ? l.ColorPattern.Name : l.ColorPattern.DisplayName,
                    CPUName = !l.CPUId.HasValue ? "" : isDefaultLanguage ? l.CPU.Name : l.CPU.DisplayName,
                    RAMName = !l.RAMId.HasValue ? "" : isDefaultLanguage ? l.RAM.Name : l.RAM.DisplayName,
                    VGAName = !l.VGAId.HasValue ? "" : isDefaultLanguage ? l.VGA.Name : l.VGA.DisplayName,
                    HDDName = !l.HDDId.HasValue ? "" : isDefaultLanguage ? l.HDD.Name : l.HDD.DisplayName,
                    ScreenName = !l.ScreenId.HasValue ? "" : isDefaultLanguage ? l.Screen.Name : l.Screen.DisplayName,
                    CameraName = !l.CameraId.HasValue ? "" : isDefaultLanguage ? l.Camera.Name : l.Camera.DisplayName,
                    BatteryName = !l.BatteryId.HasValue ? "" : isDefaultLanguage ? l.Battery.Name : l.Battery.DisplayName,
                    FieldAName = !l.FieldAId.HasValue ? "" : isDefaultLanguage ? l.FieldA.Name : l.FieldA.DisplayName,
                    FieldBName = !l.FieldBId.HasValue ? "" : isDefaultLanguage ? l.FieldB.Name : l.FieldB.DisplayName,
                    FieldCName = !l.FieldCId.HasValue ? "" : isDefaultLanguage ? l.FieldC.Name : l.FieldC.DisplayName,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModifierUserId = l.LastModifierUserId,
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : ""   
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<ItemListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelItemInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
          
            PagedResultDto<ItemListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "Item.xlsx",
                Items = listResult.Items,
                Columns = input.Columns,
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _itemManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _itemManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_Edit)]
        public async Task Update(CreateUpdateItemInputDto input)
        {
            var entity = MapEntity<Item, Guid>(input);

            CheckErrors(await _itemManager.UpdateAsync(entity));          
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ChangeSetting)]
        public async Task<ItemSettingDto> GetItemSetting()
        {
            var setting = await _itemSettingRepository.GetAll().AsNoTracking().FirstOrDefaultAsync();
            return ObjectMapper.Map<ItemSettingDto>(setting);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ChangeSetting)]
        public async Task<Guid> CreateOrUpdateItemSetting(ItemSettingDto input)
        {
            var entity = MapEntity<ItemSetting, Guid>(input);

            if (input.Id.IsNullOrEmpty())
            {
                CheckErrors(await _itemSettingManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _itemSettingManager.UpdateAsync(entity));
            }

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ChangeSetting)]
        public async Task<ItemFieldSettingDto> GetItemFieldSetting()
        {
            var setting = await _itemFieldSettingRepository.GetAll().AsNoTracking().FirstOrDefaultAsync();
            return ObjectMapper.Map<ItemFieldSettingDto>(setting);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Items_List_ChangeSetting)]
        public async Task<Guid> CreateOrUpdateItemFieldSetting(ItemFieldSettingDto input)
        {
            var entity = MapEntity<ItemFieldSetting, Guid>(input);

            if (input.Id.IsNullOrEmpty())
            {
                CheckErrors(await _itemFieldSettingManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _itemFieldSettingManager.UpdateAsync(entity));
            }

            return entity.Id;
        }
    }
}
