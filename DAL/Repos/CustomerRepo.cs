using DAL.EF;
using DAL.EF.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class CustomerRepo
    {
        private readonly MedicineShopDbContext db;

        public CustomerRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }


        public bool Create(Customer c)
        {
            db.Customers.Add(c);
            return db.SaveChanges() > 0;
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }


        public bool Update(Customer c)
        {
            var existingObject = Get(c.CustomerId);
            db.Entry(existingObject).CurrentValues.SetValues(c);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var c = Get(id);
            db.Customers.Remove(c);
            return db.SaveChanges() > 0;
        }
    }
}
