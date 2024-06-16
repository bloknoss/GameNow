using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static List<CartItem> Cart = new List<CartItem>();

        [HttpGet]
        public IActionResult GetCartItems()
        {
            return Ok(Cart);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            var existingItem = Cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                item.Id = Cart.Count + 1;
                Cart.Add(item);
            }
            return Ok(Cart);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            var item = Cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                Cart.Remove(item);
            }
            return Ok(Cart);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCartItem(int id, [FromBody] CartItem updatedItem)
        {
            var item = Cart.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                item.Quantity = updatedItem.Quantity;
            }
            return Ok(Cart);
        }
    }
}
