using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using VideoControllerWithJpa.Models;

namespace VideoControllerWithJpa.Repository
{
    public class MyVideoDatabase : DataContext
    {
        public Table<Video> Videos;

        //public MyVideoDatabase() : base(ConfigurationManager.ConnectionStrings["test"].ConnectionString)
        //{ 
        //}
    //ConfigurationManager.ConnectionStrings["test"].ConnectionString
        public MyVideoDatabase(string connString)
            : base(connString)
        {
        }
    
    }

}