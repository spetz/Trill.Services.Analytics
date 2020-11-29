using System;

namespace Trill.Services.Analytics.Core.Models
{
    public class Visibility
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Highlighted { get; set; }
    }
}