using AutoMapper;
using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;
using MediatR;
using Assignment.Shared;

namespace Assignment.Application.Features.Players.Commands.UpdatePlayer;

public record UpdateProductCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Size { get; set; }
    public int? Value { get; set; }
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
}

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
        if (product != null)
        {
            product.Name = command.Name;
            product.Size = command.Size;
            product.Value = command.Value;
            product.PhotoUrl = command.PhotoUrl;
            product.Description = command.Description;

            await _unitOfWork.Repository<Product>().UpdateAsync(product);
            product.AddDomainEvent(new ProductUpdatedEvent(product));

            await _unitOfWork.Save(cancellationToken);

            return await Result<int>.SuccessAsync(product.Id, "Product Updated.");
        }
        else
        {
            return await Result<int>.FailureAsync("Product Not Found.");
        }
    }
}
