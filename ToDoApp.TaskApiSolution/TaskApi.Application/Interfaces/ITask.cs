using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskApi.Application.Responses;
using TaskApi.Domain.Entities;

namespace TaskApi.Application.Interfaces
{
    public interface ITask
    {
        //create
        Task<TaskResponse> CreateTask(TaskEntity task);
        //update
        Task<TaskResponse> UpdateTask(TaskEntity task);
        //delete
        Task<TaskResponse> DeleteTask(int taskId);
        //get by id
        Task<TaskEntity> GetTaskById(int taskId);
        //get all
        Task<IEnumerable<TaskEntity>> GetAllTasks();
        //get by using a different condition
        Task<TaskEntity> GetBy(Expression<Func<TaskEntity, bool>> predicate);
    }
}