using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace VideoControllerWithJpa.Models
{
    [Table]
    public class Video
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public String name { get; set; }

        [Column]
        public String url { get; set; }

        [Column]
        public long duration { get; set; }

        public Video()
        {
        }

        public Video(String name, String url, long duration)
        {
            this.name = name;
            this.url = url;
            this.duration = duration;
        }

        /**
         * Two Videos will generate the same hashcode if they have exactly
         * the same values for their name, url, and duration.
         * 
         */
        public override int GetHashCode()
        {
            return String.Format("{0}_{1}_{2}", name, url, duration).GetHashCode();
        }

        /**
         * Two Videos are considered equal if they have exactly
         * the same values for their name, url, and duration.
         * 
         */
        public override bool Equals(Object obj)
        {
            var other = obj as Video;
            if (other != null)
            {
                return other.name.Equals(name) && other.url.Equals(url) && other.duration.Equals(duration);
            }
            else
            {
                return false;
            }
        }
    }
 }