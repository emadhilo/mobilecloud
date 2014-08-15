using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using VideoServiceWithSpringSecurity.Models;

namespace VideoServiceWithSpringSecurity.Repository
{
    public class MyVideoDatabase : DataContext
    {
        public Table<Video> Videos;
        public Table<Users> Users;

        public MyVideoDatabase(string connString)
            : base(connString)
        {
        }
    
    }

}