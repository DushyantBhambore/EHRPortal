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
    public class GetAllProviderQuery : IRequest<JSonModel>
    {
    }
    public class GetAllProviderQueryHandller : IRequestHandler<GetAllProviderQuery, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllProviderQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<JSonModel> Handle(GetAllProviderQuery request, CancellationToken cancellationToken)
        {
            var list = await _appDbContext.Set<Domain.User>().Where(a => a.UserTypeId == 1 && a.IsActive == true).ToListAsync();
            return new JSonModel((int)HttpStatusCode.OK, "All Provider", list);
        }
    }
}
