using BusinessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using RepoitoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Implement
{
    public class SaltCalculationsRepository : ISaltCalculationsRepository
    {
        private readonly KoiContext _context;

        public SaltCalculationsRepository(KoiContext context)
        {
            _context = context;
        }
        public Task<SaltCalculation> GetSaltCalculationsByPondID(int pondID)
        {
            var salt = _context.SaltCalculations.FirstOrDefaultAsync(n => n.PondId == pondID);
            if (salt == null)
            {
                return null;
            }
            return salt;
        }

    }
}
