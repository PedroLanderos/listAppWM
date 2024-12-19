using ListApi.Application.DTOs;
using ListApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListApi.Application.Mappers
{
    public static class ListApiMapper
    {
        public static ListEntity ToEntity(ListApiDTO list) => new()
        {
            ListId = list.ListId,
            UserId = list.UserId,
            ListName = list.ListName,
            CreatedDate = list.CreatedDate,
            UpdatedDate = list.UpdatedTime
        };

        public static (ListApiDTO?, IEnumerable<ListApiDTO>?) FromEntity(ListEntity? list, IEnumerable<ListEntity>? lists)
        {
            if (lists is not null)//multiple users
            {
                var multipleLists = lists!.Select(
                    x => new ListApiDTO(list.ListId, list.UserId, list.ListName, list.CreatedDate, list.UpdatedDate)
                    );

                return (null, multipleLists);
            }
            else if (list is not null) //one user
            {
                var singleList = new ListApiDTO(list.ListId, list.UserId, list.ListName, list.CreatedDate, list.UpdatedDate);
                return (singleList, null);
            }

            return (null, null);
        }

    }
}
