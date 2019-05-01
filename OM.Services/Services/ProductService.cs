using System;
using System.Collections.Generic;
using System.Linq;
using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public abstract class ProductService
    {
        public void ProductSave(Product product)
        {
            using (var context = new OMContext())
            {
                context.Products.Add(product);

                context.SaveChanges();
            }
        }

        public List<Product> ProductList()
        {
            using (var context = new OMContext())
            {
                return context.Products.ToList();
            }
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

                context.Entry(product).State = System.Data.Entity.EntityState.Modified;

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
