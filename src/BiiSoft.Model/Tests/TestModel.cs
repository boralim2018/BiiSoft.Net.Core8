using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Tests
{
    public class TestModel: NameActiveEntity<Guid>, IMayHaveTenant
    {
        public DateTime Date { get; private set; }
        public DateTime DateIndex { get; private set; }
        public int? TenantId { get; set; }

        public static TestModel Create(int? tenantId, long userId, string name, string displayName, DateTime date, DateTime dateIndex)
        {
            return new TestModel
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Date = date,
                DateIndex = dateIndex,
                Name = name,
                DisplayName = displayName,
                IsActive = true,                
            };
        }
    }
}
