using AutoMapper;
using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;
using MediatR;
using Assignment.Shared;

namespace Assignment.Application.Features.Players.Queries.GetProductById
{
    public record GetProductByIdQuery : IRequest<Result<GetProductByIdDto>>
    {
        public int Id { get; set; }

        public GetProductByIdQuery()
        {

        }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetProductByIdDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Product>().GetByIdAsync(query.Id);
            var product = _mapper.Map<GetProductByIdDto>(entity);
            return await Result<GetProductByIdDto>.SuccessAsync(product);
        }
    }
}
