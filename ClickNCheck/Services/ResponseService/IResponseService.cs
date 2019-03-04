using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services.ResponseService
{
    public interface IResponseService
    {
         void Process(Object responseObject);
    }
}
