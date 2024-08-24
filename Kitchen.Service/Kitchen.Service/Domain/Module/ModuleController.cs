using Kitchen.Service.Domain.Module.Models;
using Kitchen.Service.Domain.Module.UseCases.Commands;
using Kitchen.Service.Domain.Module.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Service.Domain.Module
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private IMediator Mediator { get; }

        public ModuleController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("Get/{id}")]
        public async Task<ModuleResponse> GetModule(string id, CancellationToken cancellationToken = default)
        {
            var command = new ModuleInfoQuery { Id = id };

            return await Mediator.Send(command, cancellationToken);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<ModuleResponse>> GetAllModulesAsync(CancellationToken cancellationToken = default)
        {
            var command = new AllModuleQuery();
            return await Mediator.Send(command, cancellationToken);
        }


        [HttpPost("Create")]
        public async Task Post(CreateModuleCommand command, CancellationToken cancellationToken = default)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await Mediator.Send(command, cancellationToken);
        }

        [HttpPut("Update/{id}")]
        public async Task Put(string id, AddOrUpdateModuleDto request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var command = new UpdateModuleCommand { Id = id, Dto = request };

            await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete("Delete/{id}")]
        public async Task Delete(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            var command = new DeleteModuleCommand { Id = id };
            await Mediator.Send(command, cancellationToken);
        }
    }
}
