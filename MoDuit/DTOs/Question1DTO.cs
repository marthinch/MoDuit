using System;

namespace MoDuit.DTOs
{
    public class Question1DTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public DateTime createdAt { get; set; }
    }
}
