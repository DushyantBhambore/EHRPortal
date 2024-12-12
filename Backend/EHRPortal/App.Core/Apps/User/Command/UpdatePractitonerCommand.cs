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
    public class UpdatePractitonerCommand : IRequest<JSonModel>
    {
        public PractitionerDto practitionerDto { get; set; }
    }
    public class UpdatePractitonerCommandHandller : IRequestHandler<UpdatePractitonerCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IEmailService _emailService;

        private readonly IFileService _fileservice;
        public UpdatePractitonerCommandHandller(IAppDbContext appDbContext,
            IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileservice = fileservice;
        }
        public async Task<JSonModel> Handle(UpdatePractitonerCommand request, CancellationToken cancellationToken)
        {
            var finduserid = await _appDbContext.Set<Domain.User>().
               FirstOrDefaultAsync(a => a.UserId == request.practitionerDto.UserId);

            if (finduserid == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Invalid Practitioner Id ", null);
            }


            var imageFile = request.practitionerDto.ImageFile;

            if (imageFile != null)
            {
                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
                var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
                var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";
                finduserid.ImageFile = fileUrl;
            }

            finduserid.FirstName = request.practitionerDto.FirstName;
            finduserid.LastName = request.practitionerDto.LastName;
            finduserid.PinCode = request.practitionerDto.PinCode;
            finduserid.Address = request.practitionerDto.Address;
            finduserid.Email = request.practitionerDto.Email;
            finduserid.Gender = request.practitionerDto.Gender;
            finduserid.City = request.practitionerDto.City;
            finduserid.BloogGroup = request.practitionerDto.BloogGroup;
            finduserid.CountryId = request.practitionerDto.CountryId;
            finduserid.Mobile = request.practitionerDto.Mobile;
            finduserid.DOB = request.practitionerDto.DOB;
            finduserid.StateId = request.practitionerDto.StateId;
            finduserid.Specialisation = request.practitionerDto.Specialisation;
            finduserid.Qualification = request.practitionerDto.Qualification;
            finduserid.VisitingCharge = request.practitionerDto.VisitingCharge;

            await _appDbContext.SaveChangesAsync();
            await _emailService.SendEmailAsync(finduserid.Email, "Updated Request", "Your Profile is Updated");
            return new JSonModel((int)(HttpStatusCode.OK), "Practitioner Profile Updated", finduserid);

        
        }
    }
}
