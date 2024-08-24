using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Module.Models;
using MediatR;

namespace Kitchen.Service.Domain.Module.UseCases.Queries
{
    public class ModuleInfoQuery : IRequest<ModuleResponse>
    {
        public string Id { get; set; }
    }

    public class ModuleInfoQueryHandler : IRequestHandler<ModuleInfoQuery, ModuleResponse>
    {
        private IModuleRepository Repository { get; }

        public ModuleInfoQueryHandler(IModuleRepository repository)
        {
            Repository = repository;
        }

        public async Task<ModuleResponse> Handle(ModuleInfoQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await Repository.GetByIdOrDefaultAsync(request.Id, cancellationToken);
        }
    }
}
