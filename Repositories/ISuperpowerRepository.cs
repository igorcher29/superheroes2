using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Web;

namespace Repositories
{
    public interface ISuperpowerRepository
    {
        IEnumerable<Superpower> GetSuperpowersForSuperhero(int id);
        Superpower GetSuperpower(int? superpowerId);
        void Create(Superpower superpower);
        void Update(Superpower superpower, HttpPostedFileBase uploadImage);
        void Delete(int id);
    }
}
