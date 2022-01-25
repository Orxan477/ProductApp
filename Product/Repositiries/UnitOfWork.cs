using Product.Data.DAL;
using Product.Repositiries.Implementations;
using Product.Repositiries.Interfaces;
using System.Threading.Tasks;

namespace Product.Repositiries
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private ProductRepository _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IProductRepository productRepository => _productRepository ?? new ProductRepository(_context);

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
