using BusinessLayer.Request;
using BusinessLayer.Response;
using RepoitoryLayer.Interface;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implement
{
    public class KoiService : IKoiService
    {
        private readonly IKoiRepository koiRepository;

        public KoiService(IKoiRepository koiRepository)
        {
            this.koiRepository = koiRepository;
        }

        public async Task<string> CreateKoiFish(int pondId, KoiFishRequest koiFishRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteKoiFish(int id)
        {
            throw new NotImplementedException();
        }

        public Task<KoiFishResponse> GetKoiFishById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KoiFishResponse>> GetKoiFishByPondId(int Pondid)
        {
            return await koiRepository.GetKoiFishByPondId(Pondid);
        }

        public Task<string> UpdateKoiFish(int id)
        {
            throw new NotImplementedException();
        }
    }
}
