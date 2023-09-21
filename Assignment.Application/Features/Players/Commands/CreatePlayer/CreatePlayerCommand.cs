using AutoMapper;
using Assignment.Application.Common.Mappings;
using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;
using MediatR;
using Assignment.Shared;

namespace Assignment.Application.Features.Players.Commands.CreatePlayer;

public record CreateProductCommand : IRequest<Result<int>>, IMapFrom<Product>
{
    public string Name { get; set; }
    public int? Size { get; set; }
    public int? Value { get; set; }
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
}

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = command.Name,
            Size = command.Size,
            Value = command.Value,
            PhotoUrl = command.PhotoUrl,
            Description = command.Description
        };

        await _unitOfWork.Repository<Product>().AddAsync(product);
        product.AddDomainEvent(new ProductCreatedEvent(product));
        await _unitOfWork.Save(cancellationToken);
        return await Result<int>.SuccessAsync(product.Id, "Product Created.");
    }
}
