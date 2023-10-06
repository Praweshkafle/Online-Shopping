using Microsoft.AspNetCore.Components;
using OnlineShopping.Models.Dtos;

namespace OnlineShopping.Web.Pages
{
    public class DisplayProductBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
