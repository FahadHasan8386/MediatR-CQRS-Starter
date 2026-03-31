using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniCQRS.API.Data;
using MiniCQRS.API.Models;

namespace MiniCQRS.API.CQRS.Queries;
///Command
public class GetProductQuery : IRequest<Product?>
{
    public Guid Id { get; set; }
}
///Handler
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly AppDbContext _context;

    public GetProductQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id , cancellationToken);
    }
}