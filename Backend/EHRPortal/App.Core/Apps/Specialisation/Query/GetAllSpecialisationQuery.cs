using App.Core.Dto;
using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Specialisation.Query
{
    public class GetAllSpecialisationQuery : IRequest<List<SpecialisationDto>>
    {

    }
    public class GetAllSpecialisationQueryHandller : IRequestHandler<GetAllSpecialisationQuery,List<SpecialisationDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllSpecialisationQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<SpecialisationDto>> Handle(GetAllSpecialisationQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Specialisation";
            var data = await connection.QueryAsync<SpecialisationDto>(query);
            return data.AsList();
        }
    }
}
