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

namespace App.Core.Apps.User.Command
{
    public class ForgetPasswordCommand : IRequest<JSonModel>
    {
        public ForgotPasswordDto forgotEamilDto { get; set; }

    }
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, JSonModel>
    {
        private readonly IAppDbContext _context;
        private readonly IEmailService _emailService;

        public ForgetPasswordCommandHandler(IAppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<JSonModel> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            //var email = request.Email.Trim().ToLower();
            var user = await _context.Set<Domain.User>().FirstOrDefaultAsync(x => x.Email == request.forgotEamilDto.Email, cancellationToken);

            if (user == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Invalid Email", request.forgotEamilDto.Email);
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string password = new string(Enumerable.Repeat(chars, 8).Select(s => s[new Random().Next(s.Length)]).ToArray());


            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Set<Domain.User>().Update(user);

            await _context.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Your new password is {password}");
            return new JSonModel((int)HttpStatusCode.OK, "Password Sent To Your Mail ", user.Password);
        }
    }

}
