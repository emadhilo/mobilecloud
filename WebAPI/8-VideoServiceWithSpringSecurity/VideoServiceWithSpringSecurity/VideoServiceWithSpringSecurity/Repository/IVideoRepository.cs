using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceWithSpringSecurity.Models;

namespace VideoServiceWithSpringSecurity.Repository
{
    public interface IVideoRepository
    {
        // Add a video
        bool addVideo(Video v);

        // Get the videos that have been added so far
        List<Video> getVideos();

        // Find all videos with a matching title (e.g., Video.name)
        List<Video> findByName(String title);

        // Find all videos with a duration less than
        List<Video> findByDurationLessThan(long duration);

        Users Authenticate(string username, string password);
        bool HasRole(string username, string role);
        List<String> UserRoles(string username);
 }
}
