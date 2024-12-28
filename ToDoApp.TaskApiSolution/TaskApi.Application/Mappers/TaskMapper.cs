using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApi.Application.DTOs;
using TaskApi.Domain.Entities;

namespace TaskApi.Application.Mappers
{
    public class TaskMapper
    {
        public static TaskEntity ToEntity(TaskDTO task) => new()
        {
            TaskId = task.TaskId,
            ListId = task.ListId,
            Finished = task.Finished,
            TaskName = task.Taskname,
            TaskDescription = task.TaskDescription,
            CreatedDate = task.CreatedDate,
            UpdatedDate = task.UpdatedDate
        };

        public static(TaskDTO?, IEnumerable<TaskDTO>?) FromEntity(TaskEntity? task, IEnumerable<TaskEntity>? tasks)
        {
            if(task is not null)
            {
                var singleTask = new TaskDTO(task.TaskId, task.ListId, task.Finished, task.TaskName!, task.TaskDescription!, task.CreatedDate, task.UpdatedDate);
                return (singleTask, null);
            }
            else if (tasks is not null)
            {
                var multipleTasks = tasks!.Select(x => new TaskDTO(x.TaskId, x.ListId, x.Finished, x.TaskName!, x.TaskDescription!, x.CreatedDate, x.UpdatedDate)).ToList();

                return (null, multipleTasks);
            }

            return (null, null);
        }
    }
}
