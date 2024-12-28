using ListApi.Application.DTOs;
using ListApi.Application.Interfaces;
using ListApi.Application.Mappers;
using ListApi.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace ListApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListApiController(IListApi listInterface) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<ActionResult<ListResponse>> CreateList(ListApiDTO list)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var listToEntity = ListApiMapper.ToEntity(list);

            var response = await listInterface.CreateListAsync(listToEntity);

            if(response is not null) return Ok(response);
            else return BadRequest(response);
        }

        [HttpGet("GetList/{listId:int}")]
        public async Task<ActionResult<ListApiDTO>> GetListById(int listId)
        {
            if(listId < 0) return BadRequest(ModelState);
            
            var list = await listInterface.GetListByIdAsync(listId);

            if (list is null) return NotFound("The list was not found");

            var (_list, _) = ListApiMapper.FromEntity(list, null);

            if (_list is not null) return Ok(_list);
            else return NotFound("The list was not found");
        }

        [HttpGet("GetLists")]
        public async Task<ActionResult<IEnumerable<ListApiDTO>>> GetALlLists()
        {
            var lists = await listInterface.GetAllListsAsync();

            if (!lists.Any()) return NotFound("No list found");

            var (_, _lists) = ListApiMapper.FromEntity(null, lists);
            if (_lists is not null) return Ok(_lists);
            else return NotFound("No list found");
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ListResponse>> DeleteList(int listId)
        {
            if (listId < 0) return BadRequest("La id no existe");

            var list = await GetListById(listId);

            var response = await listInterface.DeleteListAsync(listId);
            if (response is not null) return Ok(response);
            else return StatusCode(500, "An error occurred while deleting the list");
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ListResponse>> EditList(ListApiDTO list)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var listExist = await listInterface.GetListByIdAsync(list.ListId);
            if (listExist is null) return NotFound("The list does not exists");

            var listToEntity = ListApiMapper.ToEntity(list);

            var response = await listInterface.UpdateListAsync(listToEntity);

            if (response is not null) return Ok(response);
            else return BadRequest(response);    
        }
    }
}