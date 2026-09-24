using System;

namespace CompreAqui.Domain.Contracts
{
    public interface IEntity
    {
        int Id { get; }
        Guid Guid { get; }
    }
}