﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using OM.Database.Context;
using OM.Entities.EntityClass;

namespace OM.Services.Services
{
    public class CategoryService
    {
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

                context.Categories.Remove(category);

                context.SaveChanges();
            }
        }
    }
}
