using App.Core.Dto;
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

namespace App.Core.Apps.Appoinment.ProviderApooinment.Command
{
    public class CreateProviderAppoinmentCommand: IRequest<JSonModel>
    {
        public ProviderAppoinmentDto providerAppoinmentDto { get; set; }
    }
    public class CreateProviderAppoinmentCommandHandller : IRequestHandler<CreateProviderAppoinmentCommand,JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateProviderAppoinmentCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async  Task<JSonModel> Handle(CreateProviderAppoinmentCommand request, CancellationToken cancellationToken)
        {

            var checkappoinement = await _appDbContext.Set<Domain.Appoinment>()
              .FirstOrDefaultAsync(a => a.AppoinemntId == request.providerAppoinmentDto.AppoinemntId);

            if (checkappoinement != null)
            {


            }
            var newAppoinment = new Domain.Appoinment
            {
                UserId = request.providerAppoinmentDto.UserId,
                AppointmentDate = request.providerAppoinmentDto.AppointmentDate,
                AppinementTime = request.providerAppoinmentDto.AppinementTime,
                AppointmentStatus = request.providerAppoinmentDto.AppointmentStatus,
                Chiefcomplaint = request.providerAppoinmentDto.Chiefcomplaint,
            };

            return new JSonModel((int)HttpStatusCode.OK, "New Appoinment Created", newAppoinment);
        }
    }
}
