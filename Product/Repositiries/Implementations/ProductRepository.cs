using Product.Data.DAL;
using Product.Repositiries.Interfaces;

namespace Product.Repositiries.Implementations
{
    public class ProductRepository:Repository<Data.Entities.Product>,IProductRepository
    {
        private AppDbContext _context { get; }
        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
