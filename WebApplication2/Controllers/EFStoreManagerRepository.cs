
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// added references
using System.Web;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class EFStoreManagerRepository : IStoreManagerRepository
    {
        // db connection
        Comp2007Sept28_Ass2Entities db = new Comp2007Sept28_Ass2Entities();

        public IQueryable<Party> Parties { get { return db.Parties; } }

        public IQueryable<PLocation> Plocation { get { return db.PLocations; } }

        public void Delete(Party album)
        {
            db.Parties.Remove(album);
            db.SaveChanges();
        }

        public Party Save(Party party)
        {
            if (party.PartyId == 0)
            {
                db.Parties.Add(party);
            }
            else
            {
                db.Entry(party).State = EntityState.Modified;
            }
            db.SaveChanges();

            return party;
        }
    }
}