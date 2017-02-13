using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Web;

namespace Repositories
{
    public interface ISuperHeroRepository
    {
        IEnumerable<Superhero> GetSuperheroesForUser(string userId);

        Superhero GetSuperhero(int? superheroId);
        void Create(Superhero superhero);
        void Update(Superhero hero, HttpPostedFileBase uploadImage);
        void Delete(int id);
    }
}
