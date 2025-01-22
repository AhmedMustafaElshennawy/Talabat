using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Domain.basket;

namespace Talabat.Infrastructure.Common.Interfaces.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _InMemoryDataBase;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer) => _InMemoryDataBase = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            var result = await _InMemoryDataBase.KeyDeleteAsync(BasketId);
            return result;
        }
        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await _InMemoryDataBase.StringGetAsync(BasketId);
            var result = Basket.IsNull?null:JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            return result;
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var jesonBasket = JsonSerializer.Serialize(Basket);
            var result = await _InMemoryDataBase
                .StringSetAsync(Basket.Id, jesonBasket, TimeSpan.FromDays(5));

            if (!result) return null;
            return await GetBasketAsync(Basket.Id.ToString());
        }
    }
}