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

namespace App.Core.Apps.User.Command
{
    public class LoginUserCommand : IRequest<JSonModel>
    {
        public LoginDto LoginDto { get; set; }
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IEmailService _emailService;
        public LoginUserCommandHandler(IAppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<JSonModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var checkuser = await _appDbContext.Set<Domain.User>().Where(x => x.Username == request.LoginDto.Username).FirstOrDefaultAsync();
            if (checkuser == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Username  and Password is  Invalid", null);
            }
            var checkpassword = BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, checkuser.Password);
            if (!checkpassword)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Username  and Password is  Invalid", null);
            }
            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            var existingOtps = await _appDbContext.Set<Otp>()
            .Where(o => o.Username.ToLower() == checkuser.Username.ToLower())
            .ToListAsync(cancellationToken);
            if (existingOtps.Any())
            {
                _appDbContext.Set<Otp>().RemoveRange(existingOtps);
            }
            await _appDbContext.Set<Domain.Otp>().AddAsync(new Domain.Otp { Email = checkuser.Email, Username = checkuser.Username, Code = otp, Expiration = DateTime.Now.AddMinutes(5) });
            await _appDbContext.SaveChangesAsync();
            await _emailService.SendEmailAsync(checkuser.Email, "Your OTP Code", $"Your OTP code is {otp}");
            return new JSonModel((int)HttpStatusCode.OK, "Otp is Send Successfully", checkuser);
        }

    }
}
