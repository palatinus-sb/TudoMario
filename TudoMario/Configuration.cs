using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    public class Configuration
    {
        public static Developer Dev { get; } = Developer.Adam;

        public enum Developer { Adam, Dani, Patrik, Soma }
    }
}
