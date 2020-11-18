using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DTOs
{
    //used when storing CartItem data to a persistent cookie. Use a DTO to only store the
    //minimal amount of data needed to restore data from database.
    public class CartItemDTO
    {
        public int BookID { get; set; }
        public int Quantity { get; set; }
    }
}
