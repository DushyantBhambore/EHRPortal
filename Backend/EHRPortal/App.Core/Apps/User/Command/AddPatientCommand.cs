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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class AddPatientCommand : IRequest<JSonModel>
    {
        public PatientDto  patientDto { get; set; }
    }
    public class AddPatientCommandHandller : IRequestHandler<AddPatientCommand,JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IEmailService _emailService;

        private readonly IFileService _fileservice;
        public AddPatientCommandHandller(IAppDbContext appDbContext,
            IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileservice = fileservice;

        }
        public async Task<JSonModel> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {

            var checkemail = await _appDbContext.Set<Domain.User>().Where(x => x.Email ==
            request.patientDto.Email).FirstOrDefaultAsync();

            if (checkemail != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Email already exists", null);
            }
            var imageFile = request.patientDto.ImageFile;

            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";


            string formattedDOB = request.patientDto.DOB.ToString("ddMMyy");
            string username = $"EC_{request.patientDto.LastName.
                ToUpper()}{request.patientDto.FirstName.ToUpper()[0]}{formattedDOB}";

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string password = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
            //string password =Password()
            //string encryptedPassword = Encrypt(password);
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            // Create new user
            var user = new Domain.User
            {
                FirstName = request.patientDto.FirstName,
                LastName = request.patientDto.LastName,
                Email = request.patientDto.Email,
                UserTypeId = request.patientDto.UserTypeId,
                DOB = request.patientDto.DOB,
                Mobile = request.patientDto.Mobile,
                BloogGroup=request.patientDto.BloogGroup,
                Gender = request.patientDto.Gender,
                City=request.patientDto.City,
                Address = request.patientDto.Address,
                PinCode = request.patientDto.PinCode,
                StateId = request.patientDto.StateId,
                CountryId = request.patientDto.CountryId,
                ImageFile = fileUrl,
                Username = username,
                Password = encryptedPassword,
                IsActive=true,
                IsDeletd=false,
                VisitingCharge = 0,
                RegistrationNumber=null,
                Specialisation= 0,
                Qualification= null,

            };

            await _appDbContext.Set<Domain.User>().AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            await _emailService.SendEmailAsync(request.patientDto.Email,
                 "Welcome to Our Application", $"Hello {request.patientDto.FirstName}" +
                 $",\n\nYour account has been created successfully.\n\nUsername: " +
                 $"{username}\nPassword: {password}\n\nRegards,\nTeam");


            return new JSonModel((int)HttpStatusCode.OK, "User Added Successfully", user);
        }
    }
}
