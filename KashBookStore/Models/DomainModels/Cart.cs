using KashBookStore.Models.DataLayer;
using KashBookStore.Models.DataLayer.Respositories;
using KashBookStore.Models.DTOs;
using KashBookStore.Models.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DomainModels
{
    //Cart class stores CartItem objects in session and persistant cookies.
    public class Cart
    {
        private const string CartKey = "mycart";
        private const string CountKey = "mycount";

        private List<CartItem> items { get; set; }
        private List<CartItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public Cart(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }

        public void Load(Repository<Book> data)
        {
            items = session.GetObject<List<CartItem>>(CartKey);
            if (items == null)
            {
                items = new List<CartItem>();
                storedItems = requestCookies.GetObject<List<CartItemDTO>>(CartKey);
            }

            if(storedItems?.Count > items?.Count)
            {
                foreach(CartItemDTO storedItem in storedItems)
                {
                    var book = data.Get(new QueryOptions<Book>
                    {
                        Includes = "BookAuthors.Author, Genre",
                        Where = b => b.BookID == storedItem.BookID
                    });

                    if (book != null)
                    {
                        var dto = new BookDTO();
                        dto.Load(book);

                        CartItem item = new CartItem
                        {
                            Book = dto,
                            Quantity = storedItem.Quantity
                        };

                        items.Add(item);
                    }
                }

                Save();
            }
        }

        public double Subtotal => items.Sum(i => i.Subtotal);
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);

        public IEnumerable<CartItem> List => items;

        public CartItem GetByID(int id) => items.FirstOrDefault(ci => ci.Book.BookID == id);

        //if the user clicks "Add to Cart" and the item
        //is already in the cart, it's updated rather than duplicated.
        public void Add(CartItem item)
        {
            var itemInCart = GetByID(item.Book.BookID);

            //if new then add it
            if (itemInCart == null)
                items.Add(item);
            else //otherwise, increase quantity amount by 1
                itemInCart.Quantity += 1;
        }

        //when editing, replace quantity value with new one. Used when user edits a cart item
        //and changeds its quantity
        public void Edit(CartItem item)
        {
            var itemInCart = GetByID(item.Book.BookID);

            if (itemInCart != null)
                itemInCart.Quantity = item.Quantity;
        }

        public void Remove(CartItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        //stores updated cart items and new count in session and persistant cookie(stores smaller DTO in cookie).
        //If count is zero, removes cart and count from session and cookie(this is so cart  badge in navbar disappers
        //when cart is emptied, rather than showing with a value of zero).
        public void Save()
        {
            if(items.Count == 0)
            {
                session.Remove(CartKey);
                session.Remove(CountKey);
                responseCookies.Delete(CartKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<CartItem>>(CartKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<CartItemDTO>>(CartKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}
