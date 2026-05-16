using DAL.EF;
using DAL.EF.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class CategoryRepo
    {
        private readonly MedicineShopDbContext db;
        
        public CategoryRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }


        public bool Create(Category c)
        {
            db.Categories.Add(c);
            return db.SaveChanges() > 0;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }


        public bool Update(Category c)
        {
            var existingObject = Get(c.CategoryId);
            db.Entry(existingObject).CurrentValues.SetValues(c);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var c = Get(id);
            db.Categories.Remove(c);
            return db.SaveChanges() > 0;
        }

    }
}
