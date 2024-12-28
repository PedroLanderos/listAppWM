using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskApi.Application.DTOs;
using TaskApi.Application.Interfaces;
using TaskApi.Application.Mappers;
using TaskApi.Application.Responses;
using TaskApi.Domain.Entities;

namespace TaskApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITask taskInterface) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<TaskResponse>> CreateTask(TaskDTO task)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var taskToEntity = TaskMapper.ToEntity(task);

            var response = await taskInterface.CreateTask(taskToEntity);

            if(response.Flag) return Ok(response);
            else return StatusCode(500, "Error while creating the task");
        }

        //update
        [HttpPut]
        public async Task<ActionResult<TaskResponse>> UpdateTask(TaskDTO task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var taskToEntity = TaskMapper.ToEntity(task);

            var response = await taskInterface.UpdateTask(taskToEntity);

            if (response.Flag) return Ok(response);
            else return StatusCode(500, "Error while updating the task");
        }

        //delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TaskResponse>> DeleteTask(int taskId)
        {
            var task = await taskInterface.GetTaskById(taskId);
            if(task == null || taskId < 0) return NotFound("The task was not found");

            var response = await taskInterface.DeleteTask(taskId);

            if (response.Flag) return Ok(response);
            else return BadRequest(response);
            
        }
        //getbyid
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(int taskId)
        {
            if (taskId < 0) return BadRequest();
            var task = await taskInterface.GetTaskById(taskId);

            if (task is null) return NotFound("The task was not found");

            var (_task, _) = TaskMapper.FromEntity(task, null);

            if (_task is not null) return Ok(_task);
            else return NotFound("The task aws not found");
        }

        //getall
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
        {
            var tasks = await taskInterface.GetAllTasks();

            if (!tasks.Any()) return NotFound("No tasks found");

            var (_, _tasks) = TaskMapper.FromEntity(null, tasks);

            if (_tasks is not null) return Ok(_tasks);
            else return NotFound("No tasks found");
        }

        //getby

        

    }
}
