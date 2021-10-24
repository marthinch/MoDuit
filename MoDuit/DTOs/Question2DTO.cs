using System;
using System.Collections.Generic;

namespace MoDuit.DTOs
{
    public class Question2DTO
    {
        public Question2DTO()
        {
            tags = new List<string>();
        }

        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public List<string> tags { get; set; }
        public DateTime createdAt { get; set; }
    }
}
