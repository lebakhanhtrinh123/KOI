using BusinessLayer.Entity;
using BusinessLayer.Request;
using BusinessLayer.Response;
using Microsoft.EntityFrameworkCore;
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

        Task<bool> CreatePonds(int userId, PondsRequest pondsRequest);  
        Task<bool> DeletePond(int id);

        Task<bool> UpdatePond(int pondId, PondsRequest pondsRequest);

    }
}
