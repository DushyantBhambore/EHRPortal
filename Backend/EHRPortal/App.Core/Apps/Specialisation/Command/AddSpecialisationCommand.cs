using App.Core.Dto;
using App.Core.Interface;
using Domain;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Specialisation.Command
{
    public class AddSpecialisationCommand : IRequest<JSonModel>
    {
        public SpecialisationDto SpecialisationDto { get; set; }
    }
    public class AddSpecialisationCommandHandller : IRequestHandler<AddSpecialisationCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public AddSpecialisationCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(AddSpecialisationCommand request, CancellationToken cancellationToken)
        {

            var checkname = await _appDbContext.Set<Domain.Specialisation>().
                FirstOrDefaultAsync(a => a.SpecialisationName == request.SpecialisationDto.SpecialisationName);

            if (checkname == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Name already exists", null);
            }

            var name = new Domain.Specialisation
            {
                SpecialisationName = request.SpecialisationDto.SpecialisationName
            };

            await _appDbContext.Set<Domain.Specialisation>().AddAsync(name);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new JSonModel((int)HttpStatusCode.OK, "Name added successfully", name);

        }
    }
}
