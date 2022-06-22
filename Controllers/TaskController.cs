using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAssignment.Entities;
using TaskAssignment.Services;

namespace TaskAssignment.Controllers
{
    [Route("api/taskgroup")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskGroupService _service;

        public TaskController(ITaskGroupService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _service.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            
            return NotFound();
        }
        
        [HttpDelete("task-{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            var isDeleted = _service.DeleteTask(id);

            if (isDeleted)
            {
                return NoContent();
            }
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateTaskGroup([FromBody] TaskGroup taskGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var id = _service.Create(taskGroup);

            return Created($"/api/taskgroup/{id}", null);
        }
        
        [HttpPost("{id}/{name}")]
        public ActionResult ChangeName([FromRoute] int id, [FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var isChanged = _service.ChangeName(id, name);

            if (!isChanged)
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpPost("{id}")]
        public ActionResult CreateTask([FromBody] UserTask task, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var isCreated = _service.CreateTask(task, id);

            if (isCreated)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskGroup>> GetAll([FromQuery] string orderBy = "id")
        {
            var taskGroups = _service.GetAll(orderBy);

            return Ok(taskGroups);
        }
        
        [HttpGet("{id}")]
        public ActionResult<TaskGroup> Get([FromRoute] int id)
        {
            var taskGroup = _service.GetById(id);

            if (taskGroup is null)
            {
                return NotFound();
            }
            
            return Ok(taskGroup);
        }
    }
}