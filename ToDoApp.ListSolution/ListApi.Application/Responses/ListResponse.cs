using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Application.Responses
{
    public record ListResponse(bool Flag = false, string Message = "");
}
