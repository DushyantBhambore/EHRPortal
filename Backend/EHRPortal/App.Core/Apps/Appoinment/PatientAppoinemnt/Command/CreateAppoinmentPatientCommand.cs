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

namespace App.Core.Apps.Appoinment.PatientAppoinemnt.Command
{
    public class CreateAppoinmentPatientCommand : IRequest<JSonModel>
    {
        public PatientAppoinmentDto patientAppoinmentDto { get; set; }
    }
    public class CreateAppoinmentPatientCommandHandller : IRequestHandler<CreateAppoinmentPatientCommand, JSonModel>
    {

        private readonly IAppDbContext _appDbContext;

        public CreateAppoinmentPatientCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(CreateAppoinmentPatientCommand request, CancellationToken cancellationToken)
        {

            var checkappoinement = await _appDbContext.Set<Domain.Appoinment>()
                .FirstOrDefaultAsync(a => a.AppoinemntId == request.patientAppoinmentDto.AppoinemntId);

            if (checkappoinement != null)
            {


            }
            var charges = await _appDbContext.Set<Domain.User>().FirstOrDefaultAsync(a => a.UserId == request.patientAppoinmentDto.UserId);
            var newAppoinment = new Domain.Appoinment
            {
                UserId = request.patientAppoinmentDto.UserId,
                AppointmentDate = request.patientAppoinmentDto.AppointmentDate,
                AppinementTime = request.patientAppoinmentDto.AppinementTime,
                AppointmentStatus = request.patientAppoinmentDto.AppointmentStatus,
                Chiefcomplaint = request.patientAppoinmentDto.Chiefcomplaint,
                Fee = (decimal)charges.VisitingCharge,
                SpecialisationId = request.patientAppoinmentDto.SpecialisationId,
            };

            return new JSonModel((int)HttpStatusCode.OK, "New Appoinment Created", newAppoinment);

        }
    }

}
