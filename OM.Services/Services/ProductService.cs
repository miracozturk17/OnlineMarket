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
        /*
      *contex yapısını ister var using{} mantıgı ile 
      *ister constructer yöntemi ile kullan.
      */
        private readonly OMContext _context;

        protected ProductService(OMContext context)
        {
            _context = context;
        }

        public void ProductSave(Product product)
        {
            using (var context = new OMContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;

                context.Products.Add(product);

                context.SaveChanges();
            }
        }

        public List<Product> ProductList(int pageNo)
        {
            int pageSize = 10;

            return _context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).
                Take(pageSize).Include(c => c.Category).ToList();
        }

        public Product GetProduct(int id)
        {
            using (var context = new OMContext())
            {
                return context.Products.Where(x => x.Id == id).Include(x => x.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProducts(List<int> ids)
        {
            using (var context = new OMContext())
            {
                return context.Products.Where(product => ids.Contains(product.Id)).ToList();
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

        public int GetMaximumPrice()
        {
            return (int)(_context.Products.Max(x => x.UnitePrice));
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNo, int pageSize)
        {
            var products = _context.Products.ToList();

            if (categoryID.HasValue)
            {
                products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.UnitePrice >= minimumPrice.Value).ToList();
            }

            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.UnitePrice <= maximumPrice.Value).ToList();
            }

            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.UnitePrice).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.UnitePrice).ToList();
                        break;
                }
            }

            return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            var products = _context.Products.ToList();

            if (categoryID.HasValue)
            {
                products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.UnitePrice >= minimumPrice.Value).ToList();
            }

            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.UnitePrice <= maximumPrice.Value).ToList();
            }

            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.UnitePrice).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.UnitePrice).ToList();
                        break;
                }
            }
            return products.Count;
        }

        public int GetProductsCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _context.Products
                    .Count(product => product.Name != null &&
                                      product.Name.ToLower().Contains(search.ToLower()));
            }
            else
            {
                return _context.Products.Count();
            }
        }

        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _context.Products.Where(product => product.Name != null &&
                                                         product.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
            }
            else
            {
                return _context.Products
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
            }
        }
    }
}
