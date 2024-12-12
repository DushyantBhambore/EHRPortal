using App.Core.Dto;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.State.Command
{
    public class AddStateCommand : IRequest<JSonModel>
    {
        public StateDto StateDto { get; set; }
    }
    public class AddStateCommandHandler : IRequestHandler<AddStateCommand, JSonModel>
    {

        private readonly IAppDbContext _appDbContext;

        public AddStateCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(AddStateCommand request, CancellationToken cancellationToken)
        {

            var checkstate = await _appDbContext.Set<Domain.State>().Where(x => x.StateName == request.StateDto.StateName).FirstOrDefaultAsync();

            if (checkstate != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "State already exist", null);
            }

            var state = new Domain.State()
            {
                StateName = request.StateDto.StateName,
                CountryId = request.StateDto.CountryId
            };
            await _appDbContext.Set<Domain.State>().AddAsync(state);
            await _appDbContext.SaveChangesAsync();
            return new JSonModel((int)HttpStatusCode.OK, "State added successfully", state);

        }
    }
}
