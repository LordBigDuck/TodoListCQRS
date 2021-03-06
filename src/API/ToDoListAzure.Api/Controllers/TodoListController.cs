using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TodoListAzure.Application.Features.Commons.Results;
using TodoListAzure.Application.Features.Todos.Commands.CreateTodoList;
using TodoListAzure.Application.Features.Todos.Commands.UpdateTodoState;
using TodoListAzure.Application.Features.Todos.Queries.GetTodoList;

namespace TodoListAzure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(PageResult<TodoResult>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TodoResult>>> GetByCategory(
            [FromRoute] Guid categoryId,
            [FromQuery] PaginationOptions paginationOptions)
        {
            var todoListResult = await _mediator.Send(new GetTodoListQuery
            {
                CategoryId = categoryId,
                PaginationOptions = paginationOptions
            });

            return Ok(todoListResult.Value);
        }

        [HttpPost("category/{categoryId}/todo")]
        [ProducesResponseType(typeof(TodoResult), StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoResult>> AddTodo(
            [FromRoute] Guid categoryId,
            [FromBody] CreateTodoCommand createCommand)
        {
            var commandResult = createCommand.AddCategoryId(categoryId);
            if (commandResult.IsFailed)
            {
                return BadRequest(commandResult.Errors);
            }

            var todoResult = await _mediator.Send(createCommand);

            return Created("", todoResult.Value);
        }

        [HttpPut("todo/{todoId}/do")]
        [ProducesResponseType(typeof(TodoResult), StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoResult>> DoTodo([FromRoute] Guid todoId)
        {
            var updateCommand = new UpdateTodoStateCommand
            {
                TodoId = todoId,
                SetToDone = true
            };
            var todoResult = await _mediator.Send(updateCommand);

            return Ok(todoResult.Value);
        }

        [HttpPut("todo/{todoId}/undo")]
        public async Task<ActionResult<TodoResult>> UndoTodo([FromRoute] Guid todoId)
        {
            var updateCommand = new UpdateTodoStateCommand
            {
                TodoId = todoId,
                SetToDone = false
            };
            var todoResult = await _mediator.Send(updateCommand);

            return Ok(todoResult.Value);
        }
    }
}
