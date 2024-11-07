using BusinessLayer.Request;
using BusinessLayer.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IKoiGrowthService
    {
        Task<string> CreateKoiGrowth(int koiId ,KoiGrowthRequest koiGrowthRequest);
        Task<bool> DeleteKoiGrowth(int koiGrowthId);
        Task<List<KoiGrowthResponse>> GetKoiGrowths(int koiId);
    }
}
