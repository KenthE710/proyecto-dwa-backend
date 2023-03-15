using App.Dto.Cart;

namespace App.Services.CartService
{
    public interface ICartService
    {
        ///<summary>
        /// Obtener el carrito de compras del usuario.
        ///</summary>
        ///<param name="getCartDto">Dto con el id del usuario</param>
        ///<returns>Retorna el carrito del usuario</returns>
        public Task<CartDto?> getCartByUserId(GetCartDto getCartDto);

        ///<summary>
        /// Añadir Juego al carrito de compras.
        ///</summary>
        ///<param name="addToCartDto">Dto con los datos para añadir al carrito</param>
        public Task<string> addToCart(AddToCartDto addToCartDto);

        ///<summary>
        /// Elimina un Juego del carrito de compras.
        ///</summary>
        ///<param name="deleteFromCartDto">Dto con los datos para eliminar el juego del carrito</param>
        public Task<string> deleteFromCart(DeleteFromCartDto deleteFromCartDto);

        ///<summary>
        /// Obtiene la informacion del carrito de compras.
        ///</summary>
        ///<param name="getCartDto">Dto con el id del usuario</param>
        public Task<CartInfo?> getCartInfo(GetCartDto getCartDto);

        ///<summary>
        /// Elimina todos los items del carrito.
        ///</summary>
        ///<param name="cleanCartDto">Dto con el id del carrito</param>
        public Task<string> cleanCart(CleanCartDto cleanCartDto);
    }
}
