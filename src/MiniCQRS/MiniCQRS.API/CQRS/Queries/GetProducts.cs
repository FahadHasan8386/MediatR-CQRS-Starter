using MediatR;
using MiniCQRS.API.Data;
using MiniCQRS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MiniCQRS.API.CQRS.Queries
{
    public class GetProductsQuery : IRequest<List<Product>>
    {
        public string? SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly AppDbContext _context;

        public GetProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(request.SearchTerm) ||
                                        p.Description.Contains(request.SearchTerm));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice);
            }

            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync(cancellationToken);
        }
    }
}