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
    public class WaterParametersRepository : IWaterParametersRepository
    {
        private readonly KoiContext _context;

        public WaterParametersRepository(KoiContext context)
        {
            _context = context;
        }
        public Task<WaterParameter> GetWaterParametersByPondID(int pondID)
        {
            var WaterParameter =  _context.WaterParameters.FirstOrDefaultAsync(n => n.PondId == pondID);
            return WaterParameter;
        }
    }
}
