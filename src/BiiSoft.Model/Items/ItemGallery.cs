using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemGalleries")]
    public class ItemGallery : AuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public Guid GalleryId { get; private set; }

        public static ItemGallery Create(int tenantId, long userId, Guid itemId, Guid galleryId)
        {
            return new ItemGallery
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemId = itemId,
                GalleryId = galleryId
            };
        }

        public void Update(long userId, Guid itemId, Guid galleryId)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.ItemId = itemId;
            this.GalleryId = galleryId;
        }
    }
}
