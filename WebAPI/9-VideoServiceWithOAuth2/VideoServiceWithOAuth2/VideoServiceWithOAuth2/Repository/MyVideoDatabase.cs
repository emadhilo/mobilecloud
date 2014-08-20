using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using VideoServiceWithOAuth2.Models;

namespace VideoServiceWithOAuth2.Repository
{
    public class MyVideoDatabase : DataContext
    {
        public Table<Video> Videos;
        public Table<User> Users;
        public Table<Client> Clients;

        public MyVideoDatabase(string connString)
            : base(connString)
        {
        }
    
    }

}