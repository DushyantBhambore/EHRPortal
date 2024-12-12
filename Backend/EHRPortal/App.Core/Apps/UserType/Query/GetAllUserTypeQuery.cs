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
    public class GetAllUserTypeQuery : IRequest<List<UserTypeDto>>
    {

    }
    public class GetAllUserTypeQueryHandller : IRequestHandler<GetAllUserTypeQuery, List<UserTypeDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllUserTypeQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<UserTypeDto>> Handle(GetAllUserTypeQuery request, CancellationToken cancellationToken)
        {
            //var list = await _appDbContext.Set<Domain.Role>().AsTracking().ToListAsync();
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Role ";
            var data = await connection.QueryAsync<UserTypeDto>(query);
            return data.AsList();

        }
    }
}
