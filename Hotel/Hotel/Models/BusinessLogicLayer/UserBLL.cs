using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.BusinessLogicLayer
{
    public class UserBLL
    {
        HotelEntities context = new HotelEntities();

        public User LoginMethod(User user)
        {
            var newContext = context.Users.Find(user.username);
            return newContext;

        }
        public User RegisterMethod(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}
