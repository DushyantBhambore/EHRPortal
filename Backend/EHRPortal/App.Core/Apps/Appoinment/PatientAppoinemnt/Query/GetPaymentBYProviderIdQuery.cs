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
    public class GetPaymentBYProviderIdQuery : IRequest<JSonModel>
    {
        public int id { get; set; }
    }
    public class GetPaymentBYProviderIdQueryHandller : IRequestHandler<GetPaymentBYProviderIdQuery, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetPaymentBYProviderIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(GetPaymentBYProviderIdQuery request, CancellationToken cancellationToken)
        {

            var providerid = await _appDbContext.Set<Domain.User>().
                Where(a => a.UserId == request.id && a.UserTypeId == 1 && a.IsActive == true).FirstOrDefaultAsync();

            if(providerid == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, " Invalid Id Fees", null);
            }

            return new JSonModel((int)HttpStatusCode.OK, "Get Fees ", providerid);

        }
    }
}
