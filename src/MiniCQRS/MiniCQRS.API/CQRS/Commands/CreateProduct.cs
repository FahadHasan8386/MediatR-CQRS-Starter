using MediatR;
using Microsoft.AspNetCore.Http.Features;
using MiniCQRS.API.Data;
using MiniCQRS.API.Models;

namespace MiniCQRS.API.CQRS.Commands;

///command
public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty ;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

///Handler
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly AppDbContext _context;

    public CreateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request , CancellationToken cancellationToken)
    {
        var Product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Products.AddAsync(Product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Product.Id;
    }
}