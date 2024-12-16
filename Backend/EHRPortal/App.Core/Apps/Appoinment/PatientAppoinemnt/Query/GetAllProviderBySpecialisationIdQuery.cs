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
    public class GetAllProviderBySpecialisationIdQuery : IRequest<JSonModel>
    {
        public int id { get; set; }
    }
    public class GetAllProviderBySpecialisationIdQueryHandller : IRequestHandler<GetAllProviderBySpecialisationIdQuery, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllProviderBySpecialisationIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(GetAllProviderBySpecialisationIdQuery request, CancellationToken cancellationToken)
        {

            var final = await _appDbContext.Set<Domain.Specialisation>().Where(a => a.SpecialisationId == request.id).FirstOrDefaultAsync();


            var list = await _appDbContext
           .Set<Domain.User>().Where(a => a.Specialisation == request.id
            && a.UserTypeId == 1
           && a.IsActive == true).ToListAsync();

            return new JSonModel((int)HttpStatusCode.OK, "All Provider", list);
        }



    }
}
