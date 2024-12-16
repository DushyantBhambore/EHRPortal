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
    public class GetByIdPatientAppoinemntQuery : IRequest<JSonModel>
    {
        public int id { get; set; }
    }
    public class GetByIdPatientAppoinemntQueryHandller : IRequestHandler<GetByIdPatientAppoinemntQuery, JSonModel>
    {

        private readonly IAppDbContext _appDbContext;

        public GetByIdPatientAppoinemntQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(GetByIdPatientAppoinemntQuery request, CancellationToken cancellationToken)
        {

            var data = await _appDbContext.Set<Domain.Appoinment>().FirstOrDefaultAsync(a => a.AppoinemntId == request.id);

            if (data == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Error in  get Appoinment ", null);
            }
            return new JSonModel((int)HttpStatusCode.OK, "Get Appoinment ", data);

        }
    }
}
