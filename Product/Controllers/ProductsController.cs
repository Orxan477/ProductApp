using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Data.DAL;
using Product.DTOs.ProductDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public ProductsController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns>All Product</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductGetDto>>> Get()
        {
            var dbProduct = await _context.Products.Where(p => p.IsDeleted == false).ToListAsync();
            List<ProductGetDto> products = _mapper.Map<List<ProductGetDto>>(dbProduct);
            return products;
        }
        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product and product property</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductGetDto> Get(int id)
        {
            Data.Entities.Product product = _context.Products
                                                    .Where(p => p.IsDeleted == false && p.Id == id)
                                                    .FirstOrDefault();
            if (product is null) return NotFound();
            ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);
            return productGetDto;
        }
        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="productPost">Properties of the product to be created</param>
        /// <returns>No Content</returns>
        [HttpPost]
        public async Task<ActionResult> Post(ProductPostDto productPost)
        {
            Data.Entities.Product product = _mapper.Map<Data.Entities.Product>(productPost);
           await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
           return NoContent();

        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="productUpdate">Properties of the product to be renewed</param>
        /// <returns>Updated Product's object</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductUpdateDto productUpdate)
        {
            Data.Entities.Product dbProduct =await _context.Products.Where(x => !x.IsDeleted && x.Id == id)
                                                                  .FirstOrDefaultAsync();
            if (dbProduct is null) return NotFound();

            if (productUpdate.Name is null) dbProduct.Name = dbProduct.Name; 
            else dbProduct.Name = productUpdate.Name;

            if (productUpdate.Count is 0) dbProduct.Count = dbProduct.Count; 
            else dbProduct.Count = productUpdate.Count;

            if (productUpdate.Price is 0) dbProduct.Price = dbProduct.Price; 
            else dbProduct.Price = productUpdate.Price;

            await _context.SaveChangesAsync();
            return Ok(dbProduct);
        }
        /// <summary>
        /// Product Deleted
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Data.Entities.Product dbProduct = await _context.Products.Where(x => !x.IsDeleted && x.Id == id)
                                                                  .FirstOrDefaultAsync();
            if (dbProduct is null) return NotFound();
            dbProduct.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// One Column Update
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="patchDoc"></param>
        /// <returns>Updated Product</returns>
        [HttpPatch("{id}")]
        //public async Task<ActionResult> PatchUpdate(int id,[FromBody] JsonPatchDocument<ProductUpdateDto> patchDoc)
        public async Task<ActionResult> PatchUpdate(int id,[FromBody] JsonPatchDocument<Data.Entities.Product> patchDoc)
        {
            var dbProduct =await _context.Products.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (dbProduct is null) return NotFound();

            patchDoc.ApplyTo(dbProduct, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
  
           await _context.SaveChangesAsync();
           return Ok(dbProduct);
        }
    }
}
