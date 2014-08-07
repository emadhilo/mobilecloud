using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoControllerDependencyInjection.Models;

namespace VideoControllerDependencyInjection.Repository
{
    public class AllowsDuplicatesVideoRepository : IVideoRepository
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

        public List<Video> findByTitle(string title)
        {
            return videos.FindAll(v => v.name.Equals(title));
        }
    }
}