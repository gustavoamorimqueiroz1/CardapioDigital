using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Contracts
{
    public interface ICommandResult
    {
        int Status { get; }
        string Message { get; }
        bool Success { get; }
        object Data { get; }
    }
}
