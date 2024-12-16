using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Appoinment.PatientAppoinemnt.Query
{
    public class GetPatientAppoinmentByidQueryList  : IRequest<JSonModel>
    {
        public int id { get; set; }
    }
    public class GetPatientAppoinmentByidQueryHandller : IRequestHandler<GetPatientAppoinmentByidQueryList, JSonModel>
    {

        private readonly IAppDbContext _appDbContext;

        public GetPatientAppoinmentByidQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<JSonModel> Handle(GetPatientAppoinmentByidQueryList request, CancellationToken cancellationToken)
        {

            var list = await _appDbContext.Set<Domain.Appoinment>().Where(a => a.PatientId == request.id).ToListAsync();
            return new JSonModel((int)HttpStatusCode.OK, "Patient Appoinment List ", list);
        }
    }
}
