using FluentValidation;
using Kitchen.Service.DataAccess.Abstract;
using Kitchen.Service.Domain.Module.Models;
using MediatR;

namespace Kitchen.Service.Domain.Module.UseCases.Queries
{
    public class AllModuleQuery : IRequest<IEnumerable<ModuleResponse>>
    {
        
    }

    public class AllModuleQueryHandler : IRequestHandler<AllModuleQuery, IEnumerable<ModuleResponse>>
    {
        private IModuleRepository Repository { get; }

        public AllModuleQueryHandler(IModuleRepository repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<ModuleResponse>> Handle(AllModuleQuery request, CancellationToken cancellationToken)
        {
            return await Repository.GetAllAsync(request, cancellationToken);
        }
    }
}
