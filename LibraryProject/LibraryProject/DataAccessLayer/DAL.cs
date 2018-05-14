using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryProject.DataBase;

namespace LibraryProject.DataAccessLayer
{
  
    public class DAL
    {
        private LiabraryEntities _db = new LiabraryEntities();
        public Admin getAdminDetailByEmail(string email)
        {
            Admin filteredObject = (from admins in _db.Admins
                                    where admins.adminEmail.Equals(email)
                                    select admins).FirstOrDefault();

            return filteredObject;
        }

    }
}