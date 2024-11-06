using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Interface
{
    public interface ISaltCalculationsRepository
    {
        Task<SaltCalculation> GetSaltCalculationsByPondID(int pondID);
    }
}
