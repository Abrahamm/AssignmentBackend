using Assignment.Shared;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using Assignment.Application.Features.Players.Commands.CreatePlayer;
using Assignment.Application.Features.Players.Commands.UpdatePlayer;
using Assignment.Application.Features.Players.Queries.GetAllProducts;
using Assignment.Application.Features.Players.Queries.GetProductById;
using Assignment.Application.Features.Players.Commands.DeletePlayer;
using Assignment.Application.Features.Players.Queries.GetProductsWithPagination;

namespace Assignment.API.Controllers
{
    public class ProductController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<GetAllProductsDto>>>> Get()
        {
            return await _mediator.Send(new GetAllProductsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<GetProductByIdDto>>> GetProductsById(int id)
        {
            return await _mediator.Send(new GetProductByIdQuery(id));
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<PaginatedResult<GetProductsWithPaginationDto>>> GetProductsWithPagination([FromQuery] GetProductsWithPaginationQuery query)
        {
            var validator = new GetProductsWithPaginationValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = validator.Validate(query);

            if (result.IsValid)
            {
                return await _mediator.Send(query);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<int>>> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<int>>> Delete(int id)
        {
            return await _mediator.Send(new DeleteProductCommand(id));
        }
    }
}
