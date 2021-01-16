using System;
using System.Collections.Generic;

namespace Trill.Services.Analytics.Core.Models
{
    public class Story
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public Visibility Visibility { get; set; }
        public int TotalRate { get; set; }
    }
}