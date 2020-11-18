using KashBookStore.Models.DomainModels;
using KashBookStore.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.ExtensionMethods
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                BookID = ci.Book.BookID,
                Quantity = ci.Quantity
            }).ToList();
    }
}
