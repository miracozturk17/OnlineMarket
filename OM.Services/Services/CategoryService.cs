using System;
using System.Collections.Generic;
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
            using (var context = new OMContext())
            {
                context.Categories.Add(category);

                context.SaveChanges();
            }
        }

        public List<Category> CategoryList()
        {
            using (var context = new OMContext())
            {
                return context.Categories.ToList();
            }
        }

        public Category GetCategory(int id)
        {
            using (var context = new OMContext())
            {
                return context.Categories.Find(id);
            }
        }

        public List<Category> GetFeaturedCategory()
        {
            using (var context = new OMContext())
            {
                // x=> x.IsFeatured Default true 
                // x=> !x.IsFeatured false
                return context.Categories.Where(x => x.IsFeatured && x.ImageUrl != null)
                    .OrderByDescending(x=>x.Id).ToList();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new OMContext())
            {
                //context.Categories.AddOrUpdate(category);

                context.Entry(category).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            using (var context = new OMContext())
            {
                var category = context.Categories.Find(id);

                context.Categories.Remove(category ?? throw new InvalidOperationException());

                context.SaveChanges();
            }
        }
    }
}
