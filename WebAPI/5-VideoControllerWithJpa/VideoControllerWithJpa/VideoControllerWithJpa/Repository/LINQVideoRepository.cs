using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using VideoControllerWithJpa.Models;
using System.Configuration;

namespace VideoControllerWithJpa.Repository
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
    }
}