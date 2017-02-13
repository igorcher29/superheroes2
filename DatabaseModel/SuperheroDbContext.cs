using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain;

namespace DatabaseModel
{
    public class SuperheroDbContext : DbContext
    {
        public SuperheroDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Superhero> Superheroes { get; set; }
        public DbSet<Superpower> Superpowers { get; set; }
    }
}
