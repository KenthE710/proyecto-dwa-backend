using System.Data;
using App.Dto.Cart;
using System.Xml.Linq;
using App.DBMagnament;

namespace App.Services.CartService
{
    public class CartService : ICartService
    {
        public async Task<CartDto?> getCartByUserId(GetCartDto getCartDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(getCartDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "GetCart",
                "GET_CART",
                xmlParam?.ToString()
            );

            List<CartItemDto> ListData = new List<CartItemDto>();

            if (dsResultado.Tables.Count > 0)
            {
                try
                {
                    var rows = dsResultado.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        if (!row.IsNull("game_id"))
                        {
                            ListData.Add(
                                new CartItemDto
                                {
                                    quantity = Convert.ToInt32(row["quantity"]),
                                    addedOn = DateTime.Parse(row["added_on"].ToString()!),
                                    game = new CartItemGameDto
                                    {
                                        id = Convert.ToInt32(row["game_id"]),
                                        title = row["title"].ToString(),
                                        cover = row["cover"].ToString(),
                                        price = Convert.ToDecimal(row["price"]),
                                        isActive = Convert.ToBoolean(row["is_active"]),
                                        stock = Convert.ToInt32(row["stock"])
                                    }
                                }
                            );
                        }
                    }
                    return new CartDto
                    {
                        id = Convert.ToInt32(rows[0]["id"]),
                        createdOn = DateTime.Parse(rows[0]["created_on"].ToString()!),
                        items = ListData
                    };
                }
                catch (Exception)
                {
                    Console.Write("Error");
                }
            }

            return null;
        }

        public async Task<CartInfo?> getCartInfo(GetCartDto getCartDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(getCartDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "GetCart",
                "GET_CART_INFO",
                xmlParam?.ToString()
            );

            if (dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                var data = dsResultado.Tables[0].Rows[0];
                return new CartInfo
                {
                    id = Convert.ToInt32(data["id"]),
                    items = Convert.ToInt32(data["items"])
                };
            }

            return null;
        }

        public async Task<string> addToCart(AddToCartDto addToCartDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(addToCartDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "SetCart",
                "ADD_TO_CART",
                xmlParam?.ToString()
            );

            return "ok";
        }

        public async Task<string> deleteFromCart(DeleteFromCartDto deleteFromCartDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(deleteFromCartDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "SetCart",
                "DELETE_FROM_CART",
                xmlParam?.ToString()
            );

            return "ok";
        }

        public async Task<string> cleanCart(CleanCartDto cleanCartDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(cleanCartDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "SetCart",
                "CLEAN_CART",
                xmlParam?.ToString()
            );

            return "ok";
        }
    }
}
