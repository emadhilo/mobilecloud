using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoServiceWithSpringSecurity.Models;

namespace VideoServiceWithSpringSecurity.Repository
{
    public class InMemoryVideoRepository : IVideoRepository
    {
        private List<Video> videos = new List<Video>();

        public bool addVideo(Video v)
        {
            videos.Add(v);

            return true;
        }

        public List<Video> getVideos()
        {
            return videos;
        }

        public List<Video> findByName(string title)
        {
            return videos.FindAll(v => v.name.Equals(title));
        }

        public List<Video> findByDurationLessThan(long duration)
        {
            return videos.FindAll(v => v.duration < duration);
        }


        public Users Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool HasRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public List<string> UserRoles(string username)
        {
            throw new NotImplementedException();
        }

    }
}