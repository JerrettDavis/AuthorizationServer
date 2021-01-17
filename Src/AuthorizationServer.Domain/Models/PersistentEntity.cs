using System;

namespace AuthorizationServer.Domain.Models
{
    public abstract class PersistentEntity
    {
        public DateTime? DeletedDate { get; private set; }

        public virtual void Delete(DateTime deletedDate)
        {
            DeletedDate = deletedDate;
        }
    }
}