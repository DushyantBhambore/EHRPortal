using App.Core.Dto;
using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.UserType.Query
{
    public class GetAllUserTypeQuery : IRequest<List<Domain.UserType>>
    {

    }
    public class GetAllUserTypeQueryHandller : IRequestHandler<GetAllUserTypeQuery, List<Domain.UserType>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllUserTypeQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Domain.UserType>> Handle(GetAllUserTypeQuery request, CancellationToken cancellationToken)
        {
            //var list = await _appDbContext.Set<Domain.Role>().AsTracking().ToListAsync();
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM UserType";
            var data = await connection.QueryAsync<Domain.UserType>(query);
            return data.AsList();

        }
    }
}
