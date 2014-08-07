using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoControllerDependencyInjection.Models;

namespace VideoControllerDependencyInjection.Repository
{
    public class NoDuplicatesVideoRepository : IVideoRepository
    {
        private HashSet<Video> videoSet = new HashSet<Video>();

        public bool addVideo(Video v)
        {
            return videoSet.Add(v);
        }

        public List<Video> getVideos()
        {
            return videoSet.ToList();
        }

        public List<Video> findByTitle(string title)
        {
            return getVideos().FindAll(v => v.name.Equals(title));
        }
    }
}