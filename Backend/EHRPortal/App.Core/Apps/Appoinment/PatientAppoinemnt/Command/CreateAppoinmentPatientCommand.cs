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

        private readonly IEmailService _emailService;

        public CreateAppoinmentPatientCommandHandller(IAppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<JSonModel> Handle(CreateAppoinmentPatientCommand request, CancellationToken cancellationToken)
        {

            

            var checkappoinement = await _appDbContext.Set<Domain.Appoinment>()
                .FirstOrDefaultAsync(a => a.AppoinemntId == request.patientAppoinmentDto.AppoinemntId);

            if (checkappoinement != null)
            {


            }
<<<<<<< HEAD
            var charges = await _appDbContext.Set<Domain.User>().FirstOrDefaultAsync(a => a.UserId == request.patientAppoinmentDto.ProviderId);
=======
            
            var charges = await _appDbContext.Set<Domain.User>()
                .FirstOrDefaultAsync(a => a.UserId == request.patientAppoinmentDto.ProviderId);




>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
            var newAppoinment = new Domain.Appoinment
            {

                PatientId = request.patientAppoinmentDto.PatientId,
                ProviderId = request.patientAppoinmentDto.ProviderId,
                AppointmentDate = request.patientAppoinmentDto.AppointmentDate,
                AppinementTime = request.patientAppoinmentDto.AppinementTime,
                AppointmentStatus = "Scheduled",
                Chiefcomplaint = request.patientAppoinmentDto.Chiefcomplaint,
                Fee = (decimal)charges.VisitingCharge,
                SpecialisationId = request.patientAppoinmentDto.SpecialisationId
            };
            await _appDbContext.Set<Domain.Appoinment>().AddAsync(newAppoinment);
            await _appDbContext.SaveChangesAsync();
<<<<<<< HEAD
=======

            var patientemail = await _appDbContext.Set<Domain.User>().
                FirstOrDefaultAsync(a => a.UserId == request.patientAppoinmentDto.PatientId);

            _emailService.SendEmailAsync(charges.Email, "Appoinment is Schedule", "Appoiment is Schedule Check");
            _emailService.SendEmailAsync(patientemail.Email, "Appoinment is Schedule", "Appoiment is Schedule Check");

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
            return new JSonModel((int)HttpStatusCode.OK, "New Appoinment Created", newAppoinment);


        }
    }

}
