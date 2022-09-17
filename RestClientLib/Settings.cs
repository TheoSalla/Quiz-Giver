using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class Settings
    {
        public Category category { get; set; }
        public Difficulty difficulty { get; set; }
        public int amount { get; set; }
        public string sessionToken { get; set; }
    }
}