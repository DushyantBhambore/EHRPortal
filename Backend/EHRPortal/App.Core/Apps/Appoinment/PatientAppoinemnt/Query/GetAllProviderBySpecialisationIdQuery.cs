<<<<<<< HEAD
﻿using App.Core.Dto;
using App.Core.Interface;
using Dapper;
=======
﻿using App.Core.Interface;
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
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
<<<<<<< HEAD
        //public GetAllProviderBySpecialisationIdDto providerBySpecialisationId { get; set; }
=======
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
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
<<<<<<< HEAD
            
            var final = await _appDbContext.Set<Domain.Specialisation>().Where(a => a.SpecialisationId == request.id).FirstOrDefaultAsync();

           
                var list = await _appDbContext
               .Set<Domain.User>().Where(a => a.Specialisation == request.id
                && a.UserTypeId == 1
               && a.IsActive == true).ToListAsync();

                return new JSonModel((int)HttpStatusCode.OK, "All Provider", list);


=======

            var final = await _appDbContext.Set<Domain.Specialisation>().Where(a => a.SpecialisationId == request.id).FirstOrDefaultAsync();


            var list = await _appDbContext
           .Set<Domain.User>().Where(a => a.Specialisation == request.id
            && a.UserTypeId == 1
           && a.IsActive == true).ToListAsync();

            return new JSonModel((int)HttpStatusCode.OK, "All Provider", list);
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
        }



    }
}
