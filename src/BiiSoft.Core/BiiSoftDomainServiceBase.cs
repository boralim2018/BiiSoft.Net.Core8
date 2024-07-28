using Abp.Domain.Services;
using Abp.Extensions;
using BiiSoft.Extensions;
using Abp.UI;
using System;
using System.Threading.Tasks;
using BiiSoft.Entities;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Abp.Timing;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BiiSoft
{
    public abstract class BiiSoftDomainServiceBase : DomainService
    {

        protected BiiSoftDomainServiceBase()
        {
            LocalizationSourceName = BiiSoftConsts.LocalizationSourceName;
        }
    }

    public abstract class BiiSoftValidateServiceBase : BiiSoftDomainServiceBase
    {
        protected abstract string InstanceName { get; }

        #region Validate

        protected UserFriendlyException ErrorException(string message) => throw new UserFriendlyException(L("Error"), message);
        protected UserFriendlyException SelectException(string instance, string message = "") => ErrorException(L("PleaseSelect_", instance) + message);
        protected UserFriendlyException InvalidException(string instance, string message = "") => ErrorException(L("Invalid", instance) + message);
        protected UserFriendlyException IsNotValidException(string instance, string message = "") => ErrorException(L("IsNotValid", instance) + message);
        protected UserFriendlyException InputException(string instance, string message = "") => ErrorException(L("PleaseEnter_", instance) + message);
        protected UserFriendlyException NotEditableException(string instance, string message = "") => ErrorException(L("IsNotEditable", instance) + message);
        protected UserFriendlyException NotDeletableException(string instance, string message = "") => ErrorException(L("IsNotDeletable", instance) + message);
        protected UserFriendlyException NotFoundException(string instance, string message = "") => ErrorException(L("NotFound", instance) + message);
        protected UserFriendlyException DuplicateException(string instance, string message = "") => ErrorException(L("Duplicate", instance) + message);
        protected UserFriendlyException MoreThanException(string subject, string value, string message = "") => ErrorException(L("CannotBeMoreThan", subject, value) + message);
        protected UserFriendlyException MoreThanCharactersException(string subject, long characters, string message = "") => ErrorException(L("CannotBeMoreThan", subject, L("Characters_", characters)) + message);
        protected UserFriendlyException EqualException(string subject, string compare, string message = "") => ErrorException(L("MustBeEqualTo", subject, compare) + message);
        protected UserFriendlyException EqualCharactersException(string subject, long characters, string message = "") => ErrorException(L("MustBeEqualTo", subject, L("Characters_", characters)) + message);

        /// <summary>
        /// throw Duplicate Instance Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected UserFriendlyException DuplicateCodeException(string code, string message = "") => DuplicateException($"{L("Code_", InstanceName)} : {code}", message);
        protected UserFriendlyException DuplicateNameException(string name, string message = "") => DuplicateException($"{L("Name_", InstanceName)} : {name}", message);

        /// <summary>
        /// throw Invalid Instance Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected UserFriendlyException InvalidCodeException(string code, string message = "") => InvalidException($"{L("Code_", InstanceName)} : {code}", message);

        /// <summary>
        /// throw Invalid Instance Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected void ValidateCodeInput(string code, string message = "")
        {
            if (code.IsNullOrWhiteSpace()) InputException(L("Code_", InstanceName), message);
        }

        /// <summary>
        /// throw Invalid Instance Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected void ValidateCodeInput(long code, string message = "")
        {
            if (code <= 0) InputException(L("Code_", InstanceName), message);
        }

        protected void ValidateName(string name, string message = "")
        {
            if (name.IsNullOrWhiteSpace()) InputException(L("Name_", InstanceName), message);
        }

        protected void ValidateDisplayName(string displayName, string message = "")
        {
            if (displayName.IsNullOrWhiteSpace()) InputException(L("DisplayName"), message);
        }

        protected void ValidateInput(string value, string instance, string message = "")
        {
            if (value.IsNullOrWhiteSpace()) InputException(instance, message);
        }

        protected void ValidateSelect(Guid? id, string instance, string message = "")
        {
            if (id.IsNullOrEmpty()) SelectException(instance, message);
        }

        protected void ValidateSelect(long? id, string instance, string message = "")
        {
            if (id.IsNullOrZero()) SelectException(instance, message);
        }

        #endregion
    }

    public abstract class BiiSoftValidateServiceBase<TEntity, TPrimaryKey> : BiiSoftValidateServiceBase 
        where TEntity : Entity<TPrimaryKey>
    {

        protected readonly IBiiSoftRepository<TEntity, TPrimaryKey> _repository;
        public BiiSoftValidateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository)
        {
            _repository = repository;
        }

        protected abstract Task ValidateInputAsync(TEntity input);

        protected abstract TEntity CreateInstance(int? tenantId, long userId, TEntity input);

        public virtual async Task<IdentityResult> InsertAsync(int? tenantId, long userId, TEntity input)
        {
            await ValidateInputAsync(input);

            var entity = CreateInstance(tenantId, userId, input);

            await _repository.InsertAsync(entity);
            input.Id = entity.Id;
            return IdentityResult.Success;
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id, bool readOnly = true)
        {
            return readOnly ?
                   await _repository.GetAll().AsNoTracking().FirstOrDefaultAsync(u => u.Id.Equals(id)) :
                   await _repository.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        protected virtual void ValidateDeletable(TEntity input, string message = "")
        {
            if (input is ICanModifyEntity entity && entity.CannotDelete) NotDeletableException(InstanceName, message);
        }

        public virtual async Task<IdentityResult> DeleteAsync(TPrimaryKey id)
        {
            var entity = await GetAsync(id, false);
            if (entity == null) NotFoundException(InstanceName);
            ValidateDeletable(entity);

            await _repository.DeleteAsync(entity);
            return IdentityResult.Success;
        }

        protected abstract void UpdateInstance(long userId, TEntity input, TEntity entity);
        protected virtual void ValidateEditable(TEntity input, string message = "")
        {
            if (input is ICanModifyEntity entity && entity.CannotEdit) NotEditableException(InstanceName, message);
        }

        public virtual async Task<IdentityResult> UpdateAsync(long userId, TEntity input)
        {
            await ValidateInputAsync(input);

            var entity = await GetAsync(input.Id, false);
            if(entity == null) NotFoundException(InstanceName);
            ValidateEditable(entity);

            UpdateInstance(userId, input, entity);

            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }

    }

    /// <summary>
    /// This class is use for Auto Generated Code Entity only
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BiiSoftCodeGenerateServiceBase<TEntity, TPrimaryKey> : BiiSoftValidateServiceBase<TEntity, TPrimaryKey> 
        where TEntity : Entity<TPrimaryKey>, ICodeEntity
    {
        protected abstract string Prefix { get; }
        protected abstract int CodeLength { get; }

        public BiiSoftCodeGenerateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }

        #region Validate

        protected virtual async Task<string> GetLatestCodeAsync()
        {
            return await _repository.GetAll()
                            .AsNoTracking()
                            .Where(s => s.Code.StartsWith(Prefix))
                            .Where(s => s.Code.Length == CodeLength)
                            .Select(s => s.Code)
                            .OrderByDescending(s => s)
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Generate new code start with prefix and has character length equal Code Length
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GenerateCode(long index)
        {
            return index.GenerateCode(CodeLength, Prefix);
        }

        protected bool IsCode(string code)
        {
            return code.IsCode(CodeLength, Prefix);
        }

        /// <summary>
        /// throw Invalid Instance Code
        /// This method use to validate Auto Generate Code only.
        /// If code not valid throw invalid code exception
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected void ValidateAutoCodeInvalid(string code, string message = "")
        {
            if (!IsCode(code)) InvalidCodeException(code, message);
        }

        protected void ValidateAutoCodeIfHasValue(string code, string message = "")
        {
            if (!code.IsNullOrWhiteSpace() && !IsCode(code)) InvalidCodeException(code, message);
        }

        /// <summary>
        /// This method use to validate Auto Generate Code only.
        /// If user not input code throw warning input exception, 
        /// If code input not valid throw invalid exception
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected void ValidateAutoCode(string code, string message = "")
        {
            ValidateCodeInput(code, message);
            ValidateAutoCodeInvalid(code, message);
        }


        /// <summary>
        /// This method use to validate Auto Generate Code only.
        /// If code is longer then CodeLength throw Code Out of Range Exception
        /// </summary>
        /// <param name="code"></param>
        protected void ValidateCodeOutOfRange(string code, string message = "")
        {
            if (code.Length > CodeLength) ErrorException($"{L("OutOfRange", L("Code_", InstanceName))} : {code}" + message);
        }

        /// <summary>
        /// Generate next code and set for that code for Input Entity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task SetCodeAsync(TEntity input)
        {
            if (!input.Code.IsNullOrWhiteSpace()) return;

            var latestCode = await GetLatestCodeAsync();

            if (latestCode.IsNullOrWhiteSpace())
            {
                input.SetCode(GenerateCode(1));
            }
            else
            {
                ValidateAutoCodeInvalid(latestCode);

                input.SetCode(latestCode.NextCode(Prefix));
            }

            ValidateCodeOutOfRange(input.Code);
        }

        protected virtual void ValidteInput(TEntity input)
        {
            ValidateAutoCode(input.Code);
        }

        protected override async Task ValidateInputAsync(TEntity input)
        {
            ValidteInput(input);           

            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => !s.Id.Equals(input.Id) && s.Code == input.Code);
            if (find) DuplicateCodeException(input.Code);
        }

        public override async Task<IdentityResult> InsertAsync(int? tenantId, long userId, TEntity input)
        {
            await SetCodeAsync(input);
            return await base.InsertAsync(tenantId, userId, input);
        }
        #endregion
    }


    public abstract class BiiSoftNameValidateServiceBase<TEntity, TPrimaryKey> : BiiSoftValidateServiceBase<TEntity, TPrimaryKey> 
        where TEntity : NameEntity<TPrimaryKey>
    {
        protected virtual bool IsUniqueName => false;

        public BiiSoftNameValidateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }

        /// <summary>
        /// Validate Name and DisplayName
        /// </summary>
        /// <param name="input"></param>
        protected virtual void ValidateInput(TEntity input)
        {
            ValidateName(input.Name);
            ValidateDisplayName(input.DisplayName);
        }

        protected override async Task ValidateInputAsync(TEntity input)
        {
            ValidateInput(input);

            if (!IsUniqueName) return;

            var find = await _repository.GetAll().AsNoTracking().AnyAsync(s => !s.Id.Equals(input.Id) && s.Name == input.Name);
            if (find) DuplicateNameException(input.Name);
        }
    }

    public abstract class BiiSoftNameActiveValidateServiceBase<TEntity, TPrimaryKey> : BiiSoftNameValidateServiceBase<TEntity, TPrimaryKey> where TEntity : NameActiveEntity<TPrimaryKey>
    {
        public BiiSoftNameActiveValidateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }

        public async Task<IdentityResult> EnableAsync(long userId, TPrimaryKey id)
        {
            var entity = await GetAsync(id, false);
            if(entity == null) NotFoundException(InstanceName);

            entity.Enable(true);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DisableAsync(long userId, TPrimaryKey id)
        {
            var entity = await GetAsync(id, false);
            if (entity == null) NotFoundException(InstanceName);

            entity.Enable(false);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);
            return IdentityResult.Success;
        }
    }

    public abstract class BiiSoftDefaultNameActiveValidateServiceBase<TEntity, TPrimaryKey> : BiiSoftNameActiveValidateServiceBase<TEntity, TPrimaryKey> where TEntity : DefaultNameActiveEntity<TPrimaryKey>, IDefaultNameEntity
    {

        public BiiSoftDefaultNameActiveValidateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }

        public async Task<IdentityResult> SetAsDefaultAsync(long userId, TPrimaryKey id)
        {
            var entity = await GetAsync(id);
            if (entity == null) NotFoundException(InstanceName);

            var modificationTime = Clock.Now;

            var otherDefault = await _repository.GetAll().Where(s => !s.Id.Equals(id) && s.IsDefault).AsNoTracking().ToListAsync();
            foreach (var d in otherDefault)
            {
                d.SetDefault(false);
                d.LastModifierUserId = userId;
                d.LastModificationTime = modificationTime;
            }

            entity.SetDefault(true);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = modificationTime;

            otherDefault.Add(entity);

            await _repository.BulkUpdateAsync(otherDefault);

            return IdentityResult.Success;
        }

    }


    public abstract class BiiSoftCodeGenerateNameActiveValidateServiceBase<TEntity, TPrimaryKey> : BiiSoftCodeGenerateServiceBase<TEntity, TPrimaryKey> 
        where TEntity : NameActiveEntity<TPrimaryKey>, ICodeEntity
    {
        public BiiSoftCodeGenerateNameActiveValidateServiceBase(IBiiSoftRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }

        protected override void ValidteInput(TEntity input)
        {
            base.ValidteInput(input);
            ValidateName((input as INameEntity).Name);
            ValidateDisplayName((input as INameEntity).DisplayName);
        }

        public async Task<IdentityResult> EnableAsync(long userId, TPrimaryKey id)
        {
            var entity = await GetAsync(id, false);
            if (entity == null) NotFoundException(InstanceName);

            entity.Enable(true);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DisableAsync(long userId, TPrimaryKey id)
        {
            var entity = await GetAsync(id, false);
            if (entity == null) NotFoundException(InstanceName);

            entity.Enable(false);
            entity.LastModifierUserId = userId;
            entity.LastModificationTime = Clock.Now;

            await _repository.UpdateAsync(entity);
            return IdentityResult.Success;
        }
    }

}
