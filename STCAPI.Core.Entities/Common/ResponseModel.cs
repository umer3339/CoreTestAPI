using System.Collections.Generic;

namespace STCAPI.Core.Entities.Common
{
    public class ResponseModel<TEntity, T> where TEntity : class
    {
        public IEnumerable<TEntity> TEntities { get; set; }
        public TEntity Entity { get; set; }
        public string Message { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

        public ResponseModel(TEntity entity, List<TEntity> entities, string message, ResponseStatus responseStatus)
        {
            TEntities = entities;
            Entity = entity;
            Message = message;
            ResponseStatus = responseStatus;

        }
        public ResponseModel() { }
    }
}
