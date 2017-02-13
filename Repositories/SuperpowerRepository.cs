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
    public class SuperpowerRepository : ISuperpowerRepository
    {
        SuperheroDbContext _ctx;
        public SuperpowerRepository(SuperheroDbContext dataContext)
        {
            _ctx = dataContext;
        }
        public void Create(Superpower superpower)
        {
            _ctx.Superpowers.Add(superpower);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            Superpower superpower = GetSuperpower(id);
            if (superpower != null)
            {
                _ctx.Superpowers.Remove(superpower);
                _ctx.SaveChanges();
            }
        }

        public Superpower GetSuperpower(int? superpowerId)
        {
            Superpower dbEntry = null;

            if (superpowerId != null)
            {
                dbEntry = _ctx.Superpowers.Find(superpowerId);
            }

            return dbEntry;
        }

        public IEnumerable<Superpower> GetSuperpowersForSuperhero(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Superpower superpower, HttpPostedFileBase uploadImage)
        {
            Superpower dbEntry = _ctx.Superpowers.Find(superpower.Id);
            if (dbEntry != null)
            {
                dbEntry.Name = superpower.Name;
                dbEntry.Description = superpower.Description;
                dbEntry.Rating = superpower.Rating;
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
