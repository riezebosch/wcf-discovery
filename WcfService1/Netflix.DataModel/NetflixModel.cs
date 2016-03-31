using System;
using System.Data.Entity;

namespace Netflix.DataModel
{
    public class NetflixModel : DbContext
    {
        public IDbSet<Person> People { get; set; }
    }
}