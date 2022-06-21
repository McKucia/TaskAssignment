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

        [HttpGet]
        public ActionResult<IEnumerable<TaskGroup>> GetAll()
        {
            var taskGroups = _service.GetAll();

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