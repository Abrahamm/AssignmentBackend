using AutoMapper;
using Assignment.Application.Common.Mappings;
using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;
using MediatR;
using Assignment.Shared;

namespace Assignment.Application.Features.Players.Commands.DeletePlayer
{
    public record DeleteProductCommand : IRequest<Result<int>>, IMapFrom<Product>
    {
        public int Id { get; set; }

        public DeleteProductCommand()
        {

        }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
            if (product != null)
            {
                await _unitOfWork.Repository<Product>().DeleteAsync(product);
                product.AddDomainEvent(new ProductDeletedEvent(product));

                await _unitOfWork.Save(cancellationToken);

                return await Result<int>.SuccessAsync(product.Id, "Product Deleted");
            }
            else
            {
                return await Result<int>.FailureAsync("Product Not Found.");
            }
        }
    }
}
