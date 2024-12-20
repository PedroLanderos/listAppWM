using ListApi.Application.DTOs;
using ListApi.Application.Interfaces;
using ListApi.Application.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListApiController(IListApi listInterface) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ListApiDTO>> CreateList(ListApiDTO list)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var getEntity = ListApiMapper.ToEntity(list);
            var response = await listInterface.CreateListAsync(getEntity);

            if(response.Flag) return Ok(response);
            else return BadRequest(ModelState);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ListApiDTO>> GetListById(int listId)
        {
            if(listId < 0) return BadRequest(ModelState);

            var list = await listInterface.GetListByIdAsync(listId);
            if (list is null) return NotFound("List not found");

            var (_list, _) = ListApiMapper.FromEntity(list, null);

            return list is not null ? Ok(_list) : NotFound("list not found");
        }

        [HttpGet]
        public async Task<ActionResult<ListApiDTO>> GetALlLists()
        {
            var lists = await listInterface.GetAllListsAsync();

            if (!lists.Any()) return NotFound("no lists found");

            var(_, _lists) = ListApiMapper.FromEntity(null, lists);

            return lists is not null ? Ok(_lists) : NotFound("not list found");
        }

        [HttpDelete]
        public async Task<ActionResult<ListApiDTO>> DeleteList(int listId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var getList = await listInterface.GetListByIdAsync(list.ListId);
            if (getList is null) return NotFound("no list found");

            var response = await listInterface.DeleteListAsync(getList.ListId);

        }
    }
}
 