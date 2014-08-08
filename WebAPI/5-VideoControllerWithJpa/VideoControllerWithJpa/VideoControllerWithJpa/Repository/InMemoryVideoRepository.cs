using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoControllerWithJpa.Models;

namespace VideoControllerWithJpa.Repository
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
    }
}