using ListApi.Application.DTOs;
using ListApi.Application.Interfaces;
using ListApi.Application.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ListApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListApiController(IListApi listInterface) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<ActionResult<ListApiDTO>> CreateList(ListApiDTO list)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var getEntity = ListApiMapper.ToEntity(list);
            var response = await listInterface.CreateListAsync(getEntity);

            if(response.Flag) return Ok(response);
            else return BadRequest(ModelState);
        }

        [HttpGet("GetList/{listId:int}")]
        public async Task<ActionResult<ListApiDTO>> GetListById(int listId)
        {
            if(listId < 0) return BadRequest(ModelState);

            var list = await listInterface.GetListByIdAsync(listId);
            if (list is null) return NotFound("List not found");

            var (_list, _) = ListApiMapper.FromEntity(list, null);

            return list is not null ? Ok(_list) : NotFound("list not found");
        }

        [HttpGet("GetLists")]
        public async Task<ActionResult<ListApiDTO>> GetALlLists()
        {
            var lists = await listInterface.GetAllListsAsync();

            if (!lists.Any()) return NotFound("no lists found");

            var(_, _lists) = ListApiMapper.FromEntity(null, lists);

            return lists is not null ? Ok(_lists) : NotFound("not list found");
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ListApiDTO>> DeleteList(int listId)
        {
            var list = GetListById(listId);

            if (list is null) return NotFound("the list was not found");

            var response = await listInterface.DeleteListAsync(listId);

            //return condicional ? que hacer si se cumple : que hacer si no se cumple
            return response.Flag ? Ok(response) : BadRequest("the list was not deleted");  
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ListApiDTO>> EditList(ListApiDTO list)
        {
            var _list = GetListById(list.ListId);

            if (_list is null) return NotFound("the list was not found");

            var l = ListApiMapper.ToEntity(list);

            var response = await listInterface.UpdateListAsync(l);

            return (response.Flag) ? Ok(response) : BadRequest("the list was not updated");
        }
    }
}
 