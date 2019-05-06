using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public abstract class ProductService
    {
        private readonly OMContext _context;

        protected ProductService(OMContext context)
        {
            _context = context;
        }

        /*
         *contex yapısını ister var using{} mantıgı ile 
         *ister constructer yöntemi ile kullan.
         */

        public void ProductSave(Product product)
        {
            using (var context = new OMContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;

                context.Products.Add(product);

                context.SaveChanges();
            }
        }

        public List<Product> ProductList()
        {
            return _context.Products.Include(c => c.Category).ToList();
        }

        public Product GetProduct(int id)
        {
            using (var context = new OMContext())
            {
                return context.Products.Find(id);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new OMContext())
            {
                //context.Categories.AddOrUpdate(category);

                context.Entry(product).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new OMContext())
            {
                var product = context.Products.Find(id);

                context.Products.Remove(product ?? throw new InvalidOperationException());

                context.SaveChanges();
            }
        }
    }
}
