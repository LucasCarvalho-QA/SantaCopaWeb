using SantaCopaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantaCopaWeb.Interfaces
{
    public interface ITeamsService
    {
        Task<int> Create(Teams teams);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(Teams teams);
        Task<Teams> GetById(int Id);
        Task<List<Teams>> ListAll(int skip, int take, string orderBy, string direction, string search);
    }
}
