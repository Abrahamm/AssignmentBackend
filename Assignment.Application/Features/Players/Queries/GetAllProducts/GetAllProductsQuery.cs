using AutoMapper;
using AutoMapper.QueryableExtensions;
using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Assignment.Shared;

namespace Assignment.Application.Features.Players.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<Result<List<GetAllProductsDto>>>;

    internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllProductsDto>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Repository<Product>().Entities
                   .ProjectTo<GetAllProductsDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

            return await Result<List<GetAllProductsDto>>.SuccessAsync(products);
        }
    }
}
