using DAL.EF;
using DAL.EF.Table;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class SupplierRepo
    {
        private readonly MedicineShopDbContext db;

        public SupplierRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(Supplier s)
        {
            db.Suppliers.Add(s);
            return db.SaveChanges() > 0;
        }

        public Supplier Get(int id)
        {
            return db.Suppliers.Find(id);
        }

        public List<Supplier> GetAll()
        {
            return db.Suppliers.ToList();
        }

        public bool Update(Supplier s)
        {
            var existingObject = Get(s.SupplierId);
            if (existingObject != null)
            {
                db.Entry(existingObject).CurrentValues.SetValues(s);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var s = Get(id);
            if (s != null)
            {
                db.Suppliers.Remove(s);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
