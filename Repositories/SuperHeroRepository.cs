using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;
using DatabaseModel;
using System.Web;

namespace Repositories
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        SuperheroDbContext _ctx;
        public SuperHeroRepository(SuperheroDbContext dataContext)
        {
            _ctx = dataContext;
        }
        public void Create(Superhero superhero)
        {
            _ctx.Superheroes.Add(superhero);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            Superhero superHero = GetSuperhero(id);
            if (superHero != null)
            {
                foreach (var sp in superHero.Superpowers)
                {
                    sp.SuperheroId = null;
                }

                _ctx.Superheroes.Remove(superHero);
                _ctx.SaveChanges();
            }
        }

        public Superhero GetSuperhero(int? superheroId)
        {
            Superhero dbEntry = null;

            if (superheroId!=null)
            {
                dbEntry = _ctx.Superheroes.Find(superheroId);
            }

            return dbEntry;
        }

        public IEnumerable<Superhero> GetSuperheroesForUser(string userId)
        {
            return _ctx.Superheroes.Where(c => c.UserId == userId).OrderBy(c => c.Name).ToList();
        }

        public void Update(Superhero hero, HttpPostedFileBase uploadImage)
        {
            Superhero dbEntry = _ctx.Superheroes.Find(hero.Id);
            if (dbEntry != null)
            {
                dbEntry.Name = hero.Name;
                dbEntry.Description = hero.Description;

                if (uploadImage != null)
                {
                    dbEntry.ImageMimeType = uploadImage.ContentType;
                    dbEntry.ImageData = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(dbEntry.ImageData, 0, uploadImage.ContentLength);
                }

                _ctx.Entry(dbEntry).State = EntityState.Modified;
            }
            _ctx.SaveChanges();
        }
    }
}
