using ListApi.Application.Responses;
using ListApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Application.Interfaces
{
    public interface IListApi
    {
        Task<ListResponse> CreateListAsync(ListEntity list);
        Task<ListResponse> UpdateListAsync(ListEntity list);
        Task<ListResponse> DeleteListAsync(int listId);
        Task<IEnumerable<ListEntity>> GetAllListsAsync();
        Task<ListEntity> GetListByIdAsync(int listId);
    }
}
