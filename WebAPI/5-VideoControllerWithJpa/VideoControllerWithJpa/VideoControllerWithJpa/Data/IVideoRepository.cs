using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoControllerWithJpa.Models;

namespace VideoControllerWithJpa.Repository
{
    public interface IVideoRepository
    {
        // Add a video
        bool addVideo(Video v);

        // Get the videos that have been added so far
        List<Video> getVideos();

        // Find all videos with a matching title (e.g., Video.name)
        List<Video> findByName(String title);
    }
}
