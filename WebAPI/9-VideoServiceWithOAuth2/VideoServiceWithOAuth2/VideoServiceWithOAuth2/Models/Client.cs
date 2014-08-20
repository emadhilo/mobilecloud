using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace VideoServiceWithOAuth2.Models
{
    [Table(Name="Clients")]
    public class Client
    {
        [Column]
        public String Name { get; set; }

        [Column]
        public String Secret { get; set; }
    }
}