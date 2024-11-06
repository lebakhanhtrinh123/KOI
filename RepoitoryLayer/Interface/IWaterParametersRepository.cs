using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Interface
{
    public interface IWaterParametersRepository
    {
        Task<WaterParameter> GetWaterParametersByPondID(int pondID);

    }
}
