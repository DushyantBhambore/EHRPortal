using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Query
{
    public class GetByIdQuery : IRequest<Domain.User>
    {
        public int id { get; set; }
    }
    public class GetByIdQueryHandller : IRequestHandler<GetByIdQuery, Domain.User>
    {
        private readonly IAppDbContext _appDbContext;

        public GetByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Domain.User> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM [User] Where UserId = @Id And IsActive=1";
            var data = await connection.QueryFirstOrDefaultAsync<Domain.User>(query, new { Id = request.id });
            return data;

        }
    }
}
