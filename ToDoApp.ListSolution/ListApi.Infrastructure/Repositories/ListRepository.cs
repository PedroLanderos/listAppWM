using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListApi.Application.Interfaces;
using ListApi.Application.Responses;
using ListApi.Domain.Entities;
using ListApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ListApi.Infrastructure.Repositories
{
    public class ListRepository : IListApi
    {
        private readonly ListDbConext context;
        public ListRepository(ListDbConext _context)
        {
            if (_context is null)
                throw new ArgumentNullException(nameof(_context));

            context = _context;
        }
        public async Task<ListResponse> CreateListAsync(ListEntity list)
        {
            try
            {
                var listCreated = await context.Lists.AddAsync(list);
                await context.SaveChangesAsync();

                if (listCreated is null)
                    return new ListResponse(false, "error while adding the list");

                return new ListResponse(true, "list created");
                
            }
            catch (Exception)
            {

                throw new Exception("Error while creating the list");
            }
        }

        public async Task<ListResponse> DeleteListAsync(int listId)
        {
            try
            {
                var list = await context.Lists.FindAsync(listId);

                if (list is null) throw new NullReferenceException("no list fonud");

                context.Lists.Remove(list);

                await context.SaveChangesAsync();

                return new ListResponse(true, "list deleted");
            }
            catch (Exception)
            {

                throw new Exception("Error while deleting the list");
            }
        }

        public async Task<IEnumerable<ListEntity>> GetAllListsAsync()
        {
            try
            {
                var lists = await context.Lists.ToListAsync();
                if (lists is null) throw new Exception("error");

                return lists;
            }
            catch (Exception)
            {
                throw new Exception("Error while getting all the lists");
            }
        }

        public async Task<ListEntity> GetListByIdAsync(int listId)
        {
            try
            {
                var list = await context.Lists.FindAsync(listId);
                if (list is null) throw new Exception("list not found");

                return list;
            }
            catch (Exception)
            {

                throw new Exception("list not found");
            }
        }

        public async Task<ListResponse> UpdateListAsync(ListEntity list)
        {
            try
            {
                var getList = await GetListByIdAsync(list.ListId);
                if(getList is null) throw new Exception("error");

                var propierties = typeof(ListEntity).GetProperties();
                foreach (var item in propierties)
                {
                    if(item.CanWrite)
                    {
                        var newVale = item.GetValue(list);
                        item.SetValue(getList, newVale);
                    }
                }

                await context.SaveChangesAsync();

                return new ListResponse(true, "list updated");
            }
            catch (Exception)
            {

                throw new Exception("list not found");
            }
        }
    }
}
