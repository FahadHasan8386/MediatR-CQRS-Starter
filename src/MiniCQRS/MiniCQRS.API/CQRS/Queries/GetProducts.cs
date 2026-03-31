using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniCQRS.API.Data;
using MiniCQRS.API.Models;

namespace MiniCQRS.API.CQRS.Queries;

public class GetProductsQuery : IRequest<Product?>
{
    public Guid Id { get; set; }
}
///Handler
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Product?>
{
    private readonly AppDbContext _context;

    public GetProductsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
    }
}
