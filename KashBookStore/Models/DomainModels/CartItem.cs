﻿using KashBookStore.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KashBookStore.Models.DomainModels
{
    public class CartItem
    {
        //Instances of this class are stored in session after being converted to a
        //JSON string. Since the readonly Subtotal property doesn't need to be
        //stored, it's decorated with the [JsonIgnore] attribute so it will
        //be skipped when the JSON  string is created.
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => Book.Price * Quantity;
    }
}
