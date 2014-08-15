using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace VideoServiceWithSpringSecurity.Models
{
    [Table]
    public class Users
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column]
        public String username { get; set; }

        [Column]
        public String password { get; set; }

        [Column]
        public String authorities { get; set; }

        public Users()
        {
        }
    }
 }