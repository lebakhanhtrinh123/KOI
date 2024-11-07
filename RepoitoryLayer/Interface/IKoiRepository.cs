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
    public interface IKoiRepository
    {
        Task<List<KoiFishResponse>> GetKoiFishByPondId(int pondid);

        Task<KoiFishResponse> GetKoiFishById(int id);

        Task<bool> CreateKoiFish(KoiFish koiFish);
        Task<bool> DeleteKoiFish(int id);
        Task<bool> UpdateKoi(int id, KoiFishRequest koiFishRequest);
    }
}
