using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Application.DTOs
{
    public record ListApiDTO(int ListId, int UserId, string ListName, DateTime CreatedDate, DateTime UpdatedTime);
}
