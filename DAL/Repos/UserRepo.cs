using DAL.EF;
using DAL.EF.Table;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class UserRepo
    {
        private readonly MedicineShopDbContext db;

        public UserRepo(MedicineShopDbContext db)
        {
            this.db = db;
        }

        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public List<User> GetByRole(string role)
        {
            return db.Users.Where(u => u.Role == role).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public bool Delete(int id)
        {
            var user = Get(id);
            if (user != null)
            {
                db.Users.Remove(user);
                return db.SaveChanges() > 0;
            }

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == null) return false;
            var users = db.Users.ToList();
            foreach (var u in users)
            {
                if (u.Email != null && u.Email.Trim().Equals(email.Trim(), System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public User Authenticate(string email, string password)
        {
            if (email == null) return null;
            var users = db.Users.ToList();
            foreach (var u in users)
            {
                if (u.Email != null && u.Email.Trim().Equals(email.Trim(), System.StringComparison.OrdinalIgnoreCase) && u.Password == password)
                {
                    return u;
                }
            }
            return null;
        }

        public User GetByEmail(string email)
        {
            if (email == null) return null;
            var users = db.Users.ToList();
            foreach (var u in users)
            {
                if (u.Email != null && u.Email.Trim().Equals(email.Trim(), System.StringComparison.OrdinalIgnoreCase))
                {
                    return u;
                }
            }
            return null;
        }

        public bool Update(User user)
        {
            db.Users.Update(user);
            return db.SaveChanges() > 0;
        }
    }
}
