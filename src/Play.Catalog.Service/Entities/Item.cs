using System;

namespace Play.Catalog.Service.Entities
{
    public class Item
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public decimal Price { get; set; }

    }
}