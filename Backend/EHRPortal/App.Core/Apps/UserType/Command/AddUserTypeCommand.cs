using App.Core.Dto;
using App.Core.Interface;
using Domain.ResponseModel;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.UserType.Command
{
    public class AddUserTypeCommand : IRequest<JSonModel>
    {
        public UserTypeDto UserTypeDto { get; set; }
    }
    public class AddUserTypeCommandhandller : IRequestHandler<AddUserTypeCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public AddUserTypeCommandhandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(AddUserTypeCommand request, CancellationToken cancellationToken)
        {

            var checkrole = await _appDbContext.Set<Domain.UserType>().Where(x => x.UserTypeId == request.UserTypeDto.UserTypeId).FirstOrDefaultAsync();

            if (checkrole != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Role already exists", null);
            }

            var role = new Domain.UserType
            {
                UserTypName = request.UserTypeDto.UserTypName,
              
            };
            await _appDbContext.Set<Domain.UserType>().AddAsync(role);
            await _appDbContext.SaveChangesAsync();
            return new JSonModel((int)HttpStatusCode.OK, "Role added successfully", role.Adapt<UserTypeDto>()); ;
        }
    }

}
