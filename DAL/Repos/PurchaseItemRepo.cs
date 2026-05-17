using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DAL.EF.Table;

namespace DAL.Repos
{
    public class PurchaseItemRepo
    {
        private readonly MedicineShopDbContext db;

        public PurchaseItemRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(PurchaseItem obj)
        {
            db.PurchaseItems.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            if (ex != null)
            {
                db.PurchaseItems.Remove(ex);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public List<PurchaseItem> Get()
        {
            return db.PurchaseItems.ToList();
        }

        public PurchaseItem Get(int id)
        {
            return db.PurchaseItems.Find(id);
        }

        public bool Update(PurchaseItem obj)
        {
            var ex = Get(obj.PurchaseItemId);
            if (ex != null)
            {
                db.Entry(ex).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
