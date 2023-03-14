using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.Services.CartService;
using App.Dto.Cart;

namespace App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> GetCart([FromBody] GetCartDto getCartDto)
        {
            CartDto? cart = await _cartService.getCartByUserId(getCartDto);
            if (cart == null)
            {
                return NotFound(new { error = "No se encontro el carrito" });
            }
            else
            {
                return cart;
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<string>> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                return await _cartService.addToCart(addToCartDto);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<string>> DeleteFromCart(
            [FromBody] DeleteFromCartDto deleteFromCartDto
        )
        {
            try
            {
                return await _cartService.deleteFromCart(deleteFromCartDto);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
