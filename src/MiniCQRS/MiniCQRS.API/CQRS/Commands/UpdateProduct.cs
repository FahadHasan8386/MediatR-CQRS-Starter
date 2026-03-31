using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniCQRS.API.Data;

namespace MiniCQRS.API.CQRS.Commands;

public class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock {  get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand , bool>
{
    private readonly AppDbContext _context;
    public UpdateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) {
            return false;
        } 

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

}