using App.Core.Dto;
using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.State.Query
{
    public class GetStateByIdQuery : IRequest<List<StateDto>>
    {
        public int id { get; set; }
    }
    public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, List<StateDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetStateByIdQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<StateDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            //var list = await _appDbContext.Set<Domain.State>().Where(x => x.CountryId == request.id).AsTracking().ToListAsync();  
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM State Where CountryId = @Id ";
            var data = await connection.QueryAsync<StateDto>(query, new { Id = request.id });
            return data.AsList();
        }
    }
}
