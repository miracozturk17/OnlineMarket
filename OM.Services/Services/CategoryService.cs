using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public abstract class CategoryService
    {
        private readonly OMContext _context;

        protected CategoryService(OMContext context)
        {
            //Dogrudan _context kullanılmalı (using() tekrarı gereksiz.) 
            _context = context;
        }

        public void CategorySave(Category category)
        {
            _context.Categories.Add(category);

            _context.SaveChanges();
        }

        public List<Category> CategoryList()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Find(id);
        }

        public List<Category> GetFeaturedCategory()
        {
            // x=> x.IsFeatured Default true 
            // x=> !x.IsFeatured false
            return _context.Categories.Where(x => x.IsFeatured && x.ImageUrl != null)
                .OrderByDescending(x => x.Id).ToList();
        }

        public void UpdateCategory(Category category)
        {
            //context.Categories.AddOrUpdate(category);

            _context.Entry(category).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            _context.Categories.Remove(category ?? throw new InvalidOperationException());

            _context.SaveChanges();
        }

        public int GetCategoriesCount(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return _context.Categories.Count(category => category.Name != null &&
                                                             category.Name.ToLower().Contains(search.ToLower()));
            }
            return _context.Categories.Count();
        }

        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;

            if (!string.IsNullOrEmpty(search))
            {
                return _context.Categories.Where(category => category.Name != null &&
                                                            category.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
            }
            return _context.Categories
                .OrderBy(x => x.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.Products)
                .ToList();
        }
    }
}