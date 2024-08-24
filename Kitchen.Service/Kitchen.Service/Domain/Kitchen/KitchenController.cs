using Kitchen.Service.DataAccess.Models;
using Kitchen.Service.Domain.Kitchen.Models;
using Kitchen.Service.Domain.Kitchen.UseCases.Commands;
using Kitchen.Service.Domain.Kitchen.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Service.Domain.Kitchen
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitchenController : ControllerBase
    {
        private IMediator Mediator { get; }

        public KitchenController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("Get/{id}")]
        public async Task<KitchenResponse> GetKitchen(string id, CancellationToken cancellationToken = default)
        {
            var command = new KitchenInfoQuery { Id = id };
            
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<KitchenResponse>> GetAllKitchensAsync(CancellationToken cancellationToken = default)
        {
            var command = new AllKitchenQuery();
            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPost("AddModule")]
        public async Task<KitchenResponse> AddModuleToKitchen(AddModuleToKitchenCommand command, CancellationToken cancellationToken = default)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPost("Create")]
        public async Task<CreateKitchenResponse> Post(CreateKitchenCommand command, CancellationToken cancellationToken = default)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return await Mediator.Send(command, cancellationToken);
        }

        [HttpPut("Update/{id}")]
        public async Task Put(string id, AddOrUpdateKitchenDto request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var command = new UpdateKitchenCommand { Id = id, Dto = request };

            await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("Delete/{id}")]
        public async Task Delete(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            var command = new DeleteKitchenCommand { Id = id };
            await Mediator.Send(command, cancellationToken);
        }
    }
}