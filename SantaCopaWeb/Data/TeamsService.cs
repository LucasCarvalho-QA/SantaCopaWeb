using Dapper;
using SantaCopaWeb.Entities;
using SantaCopaWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SantaCopaWeb.Data
{
    public class TeamsService : ITeamsService
    {
        private readonly IDapperService _dapperService;
        public TeamsService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }
        public Task<int> Create(Teams team)
        {
            var dbParameters = new DynamicParameters();

            dbParameters.Add("Name", team.Name, DbType.String);
            dbParameters.Add("Category", team.Category, DbType.String);

            var teamId = Task.FromResult(_dapperService.Insert<int>("[dbo].[Teams]", dbParameters, commandType: CommandType.StoredProcedure));
            return teamId;
        }

        public Task<Teams> GetById(int id)
        {
            var teams = Task.FromResult(_dapperService.Get<Teams>($"select * from [Teams] where TeamID = {id}", null, commandType: CommandType.Text));
            return teams;
        }

        public Task<int> Delete(int id)
        {
            var deleteTeam = Task.FromResult(_dapperService.Execute($"Delete [Teams] where TeamID = {id}", null, commandType: CommandType.Text));
            return deleteTeam;
        }

        public Task<int> Count(string search)
        {
            var totTeams = Task.FromResult(_dapperService.Get<int>($"select COUNT(*) from [Teams] WHERE Name like '%{search}%'", null, commandType: CommandType.Text));
            return totTeams;
        }
        public Task<List<Teams>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var teams = Task.FromResult(_dapperService.GetAll<Teams>(
                $@"SELECT * 
                FROM [Teams] 
                WHERE Name like '%{search}%' 
                ORDER BY {orderBy}
                {direction}
                OFFSET {skip}
                ROWS FETCH NEXT {take}
                ROWS ONLY; ", null, commandType: CommandType.Text));
                return teams;
        }
        public Task<int> Update(Teams team)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("TeamId", team.TeamId, DbType.Int32);
            dbPara.Add("Name", team.Name, DbType.String);
            dbPara.Add("Category", team.Category, DbType.String);
            var updateTeam = Task.FromResult (_dapperService.Update<int>("[dbo].[spUpdateTeams]", dbPara, commandType: CommandType.StoredProcedure));
            return updateTeam;
        }
    }
}
