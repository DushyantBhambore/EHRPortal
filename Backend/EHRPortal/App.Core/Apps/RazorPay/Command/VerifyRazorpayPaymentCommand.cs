using MediatR;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.RazorPay.Command
{
    public class VerifyRazorpayPaymentCommand : IRequest<bool>
    {
        public string RazorpayPaymentId { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpaySignature { get; set; }
    }
    public class VerifyRazorpayPaymentCommandHandler : IRequestHandler<VerifyRazorpayPaymentCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public VerifyRazorpayPaymentCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Handle(VerifyRazorpayPaymentCommand request, CancellationToken cancellationToken)
        {
            string keySecret = _configuration["Razorpay:KeySecret"];
            var attributes = new Dictionary<string, string>
                    {
                        { "razorpay_order_id", request.RazorpayOrderId },
                        { "razorpay_payment_id", request.RazorpayPaymentId },
                        { "razorpay_signature", request.RazorpaySignature }
                    };

            try
            {
                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
