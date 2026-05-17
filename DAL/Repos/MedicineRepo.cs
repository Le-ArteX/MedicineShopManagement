using DAL.EF;
using DAL.EF.Table;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class MedicineRepo
    {
        private readonly MedicineShopDbContext db;

        public MedicineRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(Medicine m)
        {
            db.Medicines.Add(m);
            return db.SaveChanges() > 0;
        }

        public Medicine Get(int id)
        {
            return db.Medicines.Find(id);
        }

        public List<Medicine> GetAll()
        {
            return db.Medicines.ToList();
        }

        public bool Update(Medicine m)
        {
            var existingObject = Get(m.MedicineId);
            if (existingObject != null)
            {
                db.Entry(existingObject).CurrentValues.SetValues(m);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var m = Get(id);
            if (m != null)
            {
                db.Medicines.Remove(m);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
