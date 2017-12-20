using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public interface IStoreManagerRepository
    {
        // used for Unit Testing with Mock Store Manager Album data
        IQueryable<Party> Parties { get; }
        IQueryable<PLocation> Plocation { get; }
        Party Save(Party album);
        void Delete(Party album);
    }
}