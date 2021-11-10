using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Common;
using STCAPI.Core.Entities.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Infrastructure.Implementation.GenericImplementation
{
    public class DetailImplementation<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class
    {
        private STCContext context;
        private DbSet<TEntity> TEntities;

        public DetailImplementation(IConfiguration configuration)
        {
            context = new STCContext(configuration);
            TEntities = context.Set<TEntity>();
        }
        public async Task<ResponseModel<TEntity, T>> CheckIsExists(Func<TEntity, bool> where)
        {
            TEntity item = null;
            IQueryable<TEntity> dbQuery = context.Set<TEntity>();
            item = dbQuery.AsNoTracking().FirstOrDefault(where);
            return  await Task.Run(() => new ResponseModel<TEntity, T>(item, null, "success", ResponseStatus.Success)); ;
        }

        public async Task<ResponseModel<TEntity, T>> CreateEntity(TEntity[] model)
        {
            try
            {
                await TEntities.AddRangeAsync(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity, T>(null, null, "Created", ResponseStatus.Created);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The duplicate key "))
                {
                    return new ResponseModel<TEntity, T>(null, null, "Already Exists", ResponseStatus.AlreadyExists);
                }
                return new ResponseModel<TEntity, T>(null, null, ex.Message, ResponseStatus.Error);
            }
        }

        public async Task<ResponseModel<TEntity, T>> DeleteEntity(params TEntity[] items)
        {
            try
            {
                context.UpdateRange(items);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity, T>(null, null, "Deleted Successfully..", ResponseStatus.Deleted);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity, T>(null, null, ex.Message, ResponseStatus.Error);
            }
        }

        public async Task<ResponseModel<TEntity, T>> GetAllEntities(Func<TEntity, bool> where)
        {
            IQueryable<TEntity> dbQuery = context.Set<TEntity>();
            var tList = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
            return await Task.Run(() => new ResponseModel<TEntity, T>(null, tList, "success", ResponseStatus.Success));
        }

        public async  Task<ResponseModel<TEntity, T>> UpdateEntity(TEntity model)
        {
            try
            {
                context.UpdateRange(model);
                await context.SaveChangesAsync();
                return new ResponseModel<TEntity, T>(null, null, "Updated", ResponseStatus.Updated); 
            }
            catch (Exception ex)
            {
                return new ResponseModel<TEntity, T>(null, null, ex.Message, ResponseStatus.Error);
            }
        }
    }
}
