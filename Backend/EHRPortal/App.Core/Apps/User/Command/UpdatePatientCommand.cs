using App.Core.Dto;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class UpdatePatientCommand : IRequest<JSonModel>
    {
        public PatientDto patientDto { get; set; }
    }
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, JSonModel>
    {

            private readonly IAppDbContext _appDbContext;

            private readonly IEmailService _emailService;

            private readonly IFileService _fileservice;
            public UpdatePatientCommandHandler(IAppDbContext appDbContext,
                IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
            {
                _appDbContext = appDbContext;
                _emailService = emailService;
                _fileservice = fileservice;
            }

        public async Task<JSonModel> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var finduserid = await _appDbContext.Set<Domain.User>().
               FirstOrDefaultAsync(a => a.UserId == request.patientDto.UserId);

            if (finduserid == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Invalid PAtient Id ", null);
            }


            var imageFile = request.patientDto.ImageFile;

            if (imageFile != null)
            {
                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
                var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
                var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";
                finduserid.ImageFile = fileUrl;
            }

            finduserid.FirstName = request.patientDto.FirstName;
            finduserid.LastName = request.patientDto.LastName;
            finduserid.PinCode = request.patientDto.PinCode;
            finduserid.Address = request.patientDto.Address;
            finduserid.Email = request.patientDto.Email;
            finduserid.Gender = request.patientDto.Gender;
            finduserid.City = request.patientDto.City;
            finduserid.BloogGroup = request.patientDto.BloogGroup;
            finduserid.CountryId = request.patientDto.CountryId;
            finduserid.Mobile = request.patientDto.Mobile;
            finduserid.DOB = request.patientDto.DOB;
            finduserid.StateId = request.patientDto.StateId;

            await _appDbContext.SaveChangesAsync();
            await _emailService.SendEmailAsync(finduserid.Email, "Updated Request", "Your Profile is Updated");
            return new JSonModel((int)(HttpStatusCode.OK), "Patient Profile Updated", finduserid);

        }
    }
}
