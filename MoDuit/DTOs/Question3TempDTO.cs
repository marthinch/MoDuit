using System;
using System.Collections.Generic;

namespace MoDuit.DTOs
{
    public class Question3TempDTO
    {
        public Question3TempDTO()
        {
            items = new List<Item>();
        }

        public int id { get; set; }
        public int category { get; set; }
        public List<Item> items { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
    }
}
