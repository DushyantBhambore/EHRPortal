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
    public class AddPractitionerCommand : IRequest<JSonModel>
    {
        public PractitionerDto  practitionerDto { get; set; }
    }
    public class AddPractitionerCommandHandller : IRequestHandler<AddPractitionerCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IEmailService _emailService;

        private readonly IFileService _fileservice;
        public AddPractitionerCommandHandller(IAppDbContext appDbContext,
            IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileservice = fileservice;

        }
        public async Task<JSonModel> Handle(AddPractitionerCommand request, CancellationToken cancellationToken)
        {

            var checkemail = await _appDbContext.Set<Domain.User>().Where(x => x.Email ==
            request.practitionerDto.Email).FirstOrDefaultAsync();

            if (checkemail != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Email already exists", null);
            }
            var imageFile = request.practitionerDto.ImageFile;

            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";


            string formattedDOB = request.practitionerDto.DOB.ToString("ddMMyy");
            string username = $"PR_{request.practitionerDto.FirstName.
                ToUpper()}{request.practitionerDto.LastName.ToUpper()[0]}{formattedDOB}";

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string password = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
            //string password =Password()
            //string encryptedPassword = Encrypt(password);
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            // Create new user
            var user = new Domain.User
            {
                FirstName = request.practitionerDto.FirstName,
                LastName = request.practitionerDto.LastName,
                Email = request.practitionerDto.Email,
                UserTypeId = request.practitionerDto.UserTypeId,
                DOB = request.practitionerDto.DOB,
                Mobile = request.practitionerDto.Mobile,
                BloogGroup = request.practitionerDto.BloogGroup,
                Gender = request.practitionerDto.Gender,
                City = request.practitionerDto.City,
                Address = request.practitionerDto.Address,
                PinCode = request.practitionerDto.PinCode,
                StateId = request.practitionerDto.StateId,
                CountryId = request.practitionerDto.CountryId,
                ImageFile = fileUrl,
                Username = username,
                Password = encryptedPassword,
                IsActive = true,
                IsDeletd = false,
                VisitingCharge = request.practitionerDto.VisitingCharge,
                RegistrationNumber = request.practitionerDto.RegistrationNumber,
                Specialisation = request.practitionerDto.Specialisation,
                Qualification = request.practitionerDto.Qualification,
            };

            await _appDbContext.Set<Domain.User>().AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            await _emailService.SendEmailAsync(request.practitionerDto.Email,
                 "Welcome to Our Application", $"Hello {request.practitionerDto.FirstName}" +
                 $",\n\nYour account has been created successfully.\n\nUsername: " +
                 $"{username}\nPassword: {password}\n\nRegards,\nTeam");


            return new JSonModel((int)HttpStatusCode.OK, "Provider Added Successfully", user);
        }
    }
}
