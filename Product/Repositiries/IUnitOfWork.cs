using Product.Repositiries.Interfaces;
using System.Threading.Tasks;

namespace Product.Repositiries
{
    public interface IUnitOfWork
    {
        public IProductRepository productRepository { get; }
        Task SaveAsync();
    }
}
