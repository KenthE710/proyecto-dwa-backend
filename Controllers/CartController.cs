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

        [HttpPost("info")]
        public async Task<ActionResult<CartInfo>> GetCartInfo([FromBody] GetCartDto getCartDto)
        {
            CartInfo? cart = await _cartService.getCartInfo(getCartDto);
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
        public async Task<ActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                var res = await _cartService.addToCart(addToCartDto);
                return new JsonResult(new { Respuesta = res });
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
                var res = await _cartService.deleteFromCart(deleteFromCartDto);
                return new JsonResult(new { Respuesta = res });
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("clean")]
        public async Task<ActionResult<string>> DeleteFromCart([FromBody] CleanCartDto cleanCartDto)
        {
            try
            {
                var res = await _cartService.cleanCart(cleanCartDto);
                return new JsonResult(new { Respuesta = res });
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
