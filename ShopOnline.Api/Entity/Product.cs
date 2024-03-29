﻿namespace ShopOnline.Api.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public string ImageURL { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }

    }
}
