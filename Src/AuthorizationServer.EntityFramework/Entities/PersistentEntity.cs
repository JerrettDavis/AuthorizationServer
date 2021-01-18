using System;

namespace AuthorizationServer.EntityFramework.Entities
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