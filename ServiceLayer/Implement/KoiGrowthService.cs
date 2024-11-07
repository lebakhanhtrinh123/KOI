using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using RepoitoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ServiceLayer.Implement
{
    public class KoiGrowthService : IKoiGrowthService
    {
        private readonly IKoiGrowthRepository _KoiGrowthRepository;

        public KoiGrowthService(IKoiGrowthRepository koiGrowthRepository)
        {
            _KoiGrowthRepository = koiGrowthRepository;
        }

        public async Task<string> CreateKoiGrowth(int koiId, KoiGrowthRequest koiGrowthRequest)
        {
           
            var KoiGrowth = new KoiGrowth
            {
                KoiId = koiId,
                GrowthDate = koiGrowthRequest.GrowthDate,
                Size = koiGrowthRequest.Size,
                Weight = koiGrowthRequest.Weight,
                Notes = koiGrowthRequest.Notes
            };
            bool createKoi = await _KoiGrowthRepository.CreateKoiGrowth(KoiGrowth);
            if (createKoi) {

                return "create KoiGrowth successfully";
            }
            return "create KoiGrowth fail";
        }

        public async Task<bool> DeleteKoiGrowth(int koiGrowthId)
        {
            bool delete = await _KoiGrowthRepository.DeleteKoiGrowth(koiGrowthId);
            if (delete) {
                return true;
            }
            return false;
        }

        public async Task<List<KoiGrowthResponse>> GetKoiGrowths(int koiId)
        {
            return await _KoiGrowthRepository.GetKoiGrowths(koiId);
        }
    }
}
