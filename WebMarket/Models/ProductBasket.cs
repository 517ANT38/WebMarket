﻿namespace WebMarket.Models
{
    public class ProductBasket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public string Email { get; set; }
    }
}