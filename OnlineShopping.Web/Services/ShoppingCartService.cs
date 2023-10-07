using OnlineShopping.Models.Dtos;
using OnlineShopping.Web.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace OnlineShopping.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CartItemDto> AddItem(CartItemToAddDto itemToAddDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<CartItemToAddDto>(("api/cart"), itemToAddDto);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/GetItems/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
}
    }
}
