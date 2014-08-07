using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using VideoControllerWithJpa.Models;

namespace VideoControllerWithJpa.Data
{
    public class MyVideoDatabase : DataContext
    {
        public Table<Video> Videos;

        public MyVideoDatabase() : base(ConfigurationManager.ConnectionStrings["test"].ConnectionString)
        { 
        }
    }
}