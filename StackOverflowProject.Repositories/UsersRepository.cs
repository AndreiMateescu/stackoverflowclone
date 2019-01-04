using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int userID);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string email, string password);
        List<User> GetUsersByEmail(string email);
        List<User> GetUsersbyUserID(int userID);
        int GetLatestUserID();
    }

    public class UsersRepository : IUsersRepository
    {
        StackOverflowDatabaseDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void DeleteUser(int userID)
        {
            var user = db.Users.Where(u => u.UserID == userID).SingleOrDefault();

            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public int GetLatestUserID()
        {
            return db.Users.Select(temp => temp.UserID).Max();
        }

        public List<User> GetUsers()
        {
            return db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
        }

        public List<User> GetUsersByEmail(string email)
        {
            return db.Users.Where(temp => temp.Email.Equals(email)).ToList();
        }

        public List<User> GetUsersByEmailAndPassword(string email, string password)
        {
            return db.Users.Where(temp => temp.Email.Equals(email) && temp.PasswordHash.Equals(password)).ToList();
        }

        public List<User> GetUsersbyUserID(int userID)
        {
            return db.Users.Where(temp => temp.UserID == userID).ToList();
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User us = db.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if(us != null)
            {
                us.Name = user.Name;
                us.Mobile = user.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User user)
        {
            User us = db.Users.Where(temp => temp.UserID == user.UserID).FirstOrDefault();
            if (us != null)
            {
                us.PasswordHash = user.PasswordHash;
                db.SaveChanges();
            }
        }
    }
}
