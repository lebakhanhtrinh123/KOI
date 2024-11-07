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
    }
}
