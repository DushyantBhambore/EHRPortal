using App.Core.Interface;
using Dapper;
using Domain.ResponseModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Appoinment.PatientAppoinemnt.Query
{
    public class GetAllAppoinmentForPatient : IRequest<JSonModel>
    {
    }
    public class GetAllAppoinmentForPatientHandller : IRequestHandler<GetAllAppoinmentForPatient, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllAppoinmentForPatientHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<JSonModel> Handle(GetAllAppoinmentForPatient request, CancellationToken cancellationToken)
        {
            //using var connection = _appDbContext.GetConnection();
            //var query = "SELECT * FROM Appoinment Where UserId = @Id And IsActive=1";
            //var data = await connection.QueryAsync<Domain.Appoinment>(query, new { Id = request.id });
            //return data;

            throw new NotImplementedException();
        }
    }
}
