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
         Task<List<KoiFishResponse>> GetKoiFishByPondId(int Pondid);
         Task<KoiFishResponse> GetKoiFishById(int id);    
         Task<bool> DeleteKoiFish(int id);
         Task<string> CreateKoiFish(int pondId , KoiFishRequest koiFishRequest);
         Task<string> UpdateKoiFish(int id,KoiFishRequest koiFishRequest);

    }
}
