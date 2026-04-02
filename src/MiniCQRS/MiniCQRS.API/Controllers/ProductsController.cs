using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MiniCQRS.API.CQRS.Commands;
using MiniCQRS.API.CQRS.Queries;
using MiniCQRS.API.Models;

namespace MiniCQRS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet]
    //public async Task<ActionResult<List<Product>>> GetProducts([FromQuery] string? search,
    //        [FromQuery] decimal? minPrice,
    //        [FromQuery] decimal? maxPrice)
    //{
    //    var query = new GetProductQuery
    //    {
    //        SearchTerm = search,
    //        MinPrice = minPrice,
    //        MaxPrice = maxPrice
    //    };
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var query = new GetProductQuery { Id = id };
        var product = await _mediator.Send(query);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(CreateProductCommand createProductCommand)
    {
        var id = await _mediator.Send(createProductCommand);
        return CreatedAtAction(nameof(GetProduct), new { id }, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
