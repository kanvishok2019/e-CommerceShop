using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Core.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.ApplicationCore.Basket.Query;
using ShoppingCart.ApplicationCore.Buyer;
using ShoppingCart.ApplicationCore.Buyer.Query;
using IMapper = AutoMapper.IMapper;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IQueryBus _queryBus;
        private readonly IMapper _mapper;

        public BuyerController(IQueryBus queryBus, IMapper mapper)
        {
            _queryBus = queryBus;
            _mapper = mapper;
        }

        public async Task<List<Models.Buyer>> Get()
        {
            var buyers = await _queryBus.SendAsync<GetAllBuyers, IReadOnlyList<Buyer>>(new GetAllBuyers());
            var buyersList = new List<Models.Buyer>();
            foreach (var buyer in buyers)
            {
                buyersList.Add(_mapper.Map<Buyer,Models.Buyer>(buyer));
            }
            return buyersList;
        }
    }
}
