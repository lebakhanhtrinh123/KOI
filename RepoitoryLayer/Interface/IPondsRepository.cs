using BusinessLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoitoryLayer.Interface
{
    public interface IPondsRepository
    {
        Task<List<PondsResponse>> GetAllPonds();

        Task<PondsResponse> GetPondsById(int id);

        Task<List<PondsResponse>> GetAllPondsByUserId(int userId);

    }
}
