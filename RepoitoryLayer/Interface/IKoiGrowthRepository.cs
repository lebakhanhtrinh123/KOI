using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Interface
{
    public interface IKoiGrowthRepository
    {
        Task<bool> CreateKoiGrowth(KoiGrowth koiGrowth);
        Task<bool> DeleteKoiGrowth(int koiGrowthId);
        Task<List<KoiGrowthResponse>> GetKoiGrowths(int koiId);
    }
}
