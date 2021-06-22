using NET_Core_API.Data.Entities;
using NET_Core_API.Data.Repositories.Repos_Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET_Core_API.Data.Repositories.Repositories
{
    public class ProductRepo : IProductRepo
    {

        private readonly ProductContext _context;

        public ProductRepo(ProductContext context)
        {
            _context = context;
        }

        public Product Get(int id)
        {
            var product = _context.Products.FirstOrDefault
                        (i => i.ID == id);

            return product;
        }

        public List<Product> GetAll()
        {
            var products = _context.Products.ToList();

            return products;
        }

        public int Save(Product item)
        {
            if(item.ID == 0)
            {
                _context.Products.Add(item);
            }
            else
            {
                _context.Products.Update(item);
            }

            return _context.SaveChanges();
        }


        public int Delete(Product item)
        {
            _context.Remove(item);

            return _context.SaveChanges();
        }
    }
}
