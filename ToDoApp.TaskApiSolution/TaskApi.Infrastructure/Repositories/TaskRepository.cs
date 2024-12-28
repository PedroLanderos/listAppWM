using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskApi.Application.Interfaces;
using TaskApi.Application.Responses;
using TaskApi.Domain.Entities;
using TaskApi.Infrastructure.Data;

namespace TaskApi.Infrastructure.Repositories
{
    public class TaskRepository : ITask
    {
        private readonly TaskDbContext context;

        public TaskRepository(TaskDbContext _context)
        {
            if (_context is null)
                throw new ArgumentException(nameof(context));

            context = _context;
        }
        public async Task<TaskResponse> CreateTask(TaskEntity task)
        {
            try
            {
                var response = await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();

                if (response is null)
                    return new TaskResponse(false, "the task was not created");

                return new TaskResponse(true, "Task created");
            }
            catch (Exception)
            {
                throw new Exception("error");
            }
        }

        public async Task<TaskResponse> DeleteTask(int taskId)
        {
            try
            {
                var task = await GetTaskById(taskId);
                if (task is null)
                    throw new Exception("The task was not found");

                context.Tasks.Remove(task);
                await context.SaveChangesAsync();

                return new TaskResponse(true, "The task was deleted");
            }
            catch (Exception)
            {
                throw new Exception("Error while deleting task");
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasks()
        {
            try
            {
                var tasks = await context.Tasks.ToListAsync();

                if (tasks is null)
                    throw new Exception("Error while getting the tasks");

                return tasks; 
            }
            catch (Exception)
            {
                throw new Exception("Error while getting the tasks");
            }
        }

        public async Task<TaskEntity> GetBy(Expression<Func<TaskEntity, bool>> predicate)
        {
            try
            {
                var task = await context.Tasks.Where(predicate).FirstOrDefaultAsync();

                if (task is null)
                    throw new Exception("Error while getting the task");

                return task;
            }
            catch (Exception)
            {

                throw new Exception("Error while getting the task");
            }
        }

        public async Task<TaskEntity> GetTaskById(int taskId)
        {
            try
            {
                var task = await context.Tasks.FindAsync(taskId);
                if (task is null)
                    throw new Exception("The task was not found");

                return task;
            }
            catch (Exception)
            {
                throw new Exception("Error while getting the task");
            }
        }

        public async Task<TaskResponse> UpdateTask(TaskEntity task)
        {
            try
            {
                var response = await GetTaskById(task.TaskId);

                if (response is null)
                    return new TaskResponse(false, "Task was not found");

                context.Entry(task).State = EntityState.Detached;
                context.Tasks.Update(task);

                await context.SaveChangesAsync();
                return new TaskResponse(true, "Task updated");


            }
            catch (Exception)
            {

                throw new Exception("Error while updating the task");
            }
        }
    }
}