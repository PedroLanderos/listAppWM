using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApi.Application.DTOs
{
    public record TaskDTO(int TaskId, int ListId, bool Finished, string Taskname, string TaskDescription, DateTime CreatedDate, DateTime UpdatedDate);    
}
