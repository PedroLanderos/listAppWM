using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApi.Application.Responses
{
    public record TaskResponse(bool Flag = false, string Message = "");
    
}
