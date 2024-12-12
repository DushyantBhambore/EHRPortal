using App.Core.Dto;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace App.Core.Apps.Country.Command
{
    public class AddCountryCommand : IRequest<JSonModel>
    {
        public CountryDto CountryDto { get; set; }
    }
    public class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        public AddCountryCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {

            var checkcountryname = await _appDbContext.Set<Domain.Country>().Where(x => x.CountryName == request.CountryDto.CountryName).FirstOrDefaultAsync();


            if (checkcountryname != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Country already exists", null);
            }
            var country = new Domain.Country()
            {
                CountryName = request.CountryDto.CountryName,
                sortname = request.CountryDto.sortname,
                phonecode = request.CountryDto.phonecode,
            };
            await _appDbContext.Set<Domain.Country>().AddAsync(country);
            await _appDbContext.SaveChangesAsync();
            return new JSonModel((int)HttpStatusCode.OK, "Country added successfully", country);

        }
    }
}
