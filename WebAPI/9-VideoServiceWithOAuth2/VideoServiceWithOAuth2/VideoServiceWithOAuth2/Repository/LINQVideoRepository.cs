using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using VideoServiceWithOAuth2.Models;
using System.Configuration;

namespace VideoServiceWithOAuth2.Repository
{
    public class LINQVideoRepository : IVideoRepository
    {
        private MyVideoDatabase _db;

        public LINQVideoRepository()
        {
            _db = new MyVideoDatabase(ConfigurationManager.ConnectionStrings["test"].ConnectionString);
        }

        public bool addVideo(Video v)
        {
            _db.Videos.InsertOnSubmit(v);
            _db.SubmitChanges();

            return true;
        }

        public List<Video> getVideos()
        {
            return _db.Videos.ToList<Video>();
        }

        public List<Video> findByName(string title)
        {
            var findResult = from v in _db.Videos
                             where v.name.Equals(title)
                             select v;

            return findResult.ToList<Video>();
        }

        public List<Video> findByDurationLessThan(long duration)
        {
            var findResult = from v in _db.Videos
                             where v.duration < duration
                             select v;

            return findResult.ToList<Video>();
        }
        
        public bool Authenticate(string username, string password)
        {
            var findUser = (from u in _db.Users
                   where u.username.Equals(username) && u.password.Equals(password)
                   select u).FirstOrDefault<User>();

            return findUser != null;
        }

        public bool AuthenticateClient(string client, string secret)
        {
            var findClient = (from c in _db.Clients
                            where c.Name.Equals(client) && c.Secret.Equals(secret)
                            select c).FirstOrDefault<Client>();

            return findClient != null;
        }


        public bool HasRole(string username, string role)
        {
            var findUser = (from u in _db.Users
                            where u.username.Equals(username)
                            select u).FirstOrDefault<User>();

            if (findUser == null)
                return false;

            return findUser.authorities.Contains(role);
        }

        public List<String> UserRoles(string username)
        {
            var findUser = (from u in _db.Users
                            where u.username.Equals(username)
                            select u).FirstOrDefault<User>();

            if (findUser == null)
                return new List<String>();

            return findUser.authorities.Split(',').ToList();
        }

    }
}