using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestClientLib
{
    public class Settings
    {
        public Category Category { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Amount { get; set; }
        public string SessionToken { get; set; }

        public Settings()
        {
            SessionToken ??= "";
        }
    }
}
