using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers.Repository
{
    public interface IUnitOfWorkResult
    {
        bool IsError { get; set; }
        Exception ErrorInfo { get; set; }
        object Result { get; set; }
        string CustomErrorMessage { get; set; }
        string ErrorMessage { get; }
    }
}
