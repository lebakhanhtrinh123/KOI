using BusinessLayer.Request;
using BusinessLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public  interface IKoiService
    {
        public Task<List<KoiFishResponse>> GetKoiFishByPondId(int Pondid);
        public Task<KoiFishResponse> GetKoiFishById(int id);    
        public Task<bool> DeleteKoiFish(int id);
        public Task<string> CreateKoiFish(int pondId , KoiFishRequest koiFishRequest);

        public Task<string> UpdateKoiFish(int id);

    }
}
