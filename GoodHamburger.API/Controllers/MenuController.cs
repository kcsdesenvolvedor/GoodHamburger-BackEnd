using GoodHamburger.API.Dtos;
using GoodHamburger.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        public MenuController()
        {
            
        }

        [HttpGet]
        public IActionResult GetMenu()
        {
            var menu = MenuService.Items
                .Select(item => new MenuItemResponseDTO
                {
                    Name = item.Name,
                    Type = item.Type.ToString(),
                    Price = item.Price
                })
                .ToList();

            return Ok(menu);
        }
    }
}
