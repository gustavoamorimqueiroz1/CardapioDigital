using CompreAqui.Domain.Contracts;
using System;

namespace CompreAqui.Domain.Models.Entities
{
    public abstract class EntityModels :IEntity
    {
        public int Id { get; private set; }
        public Guid Guid
        {
            get;  set;
        //    get { return this.Guid; }
        //    set { if (value == null) value = Guid.NewGuid(); }
        }
    }
}
