using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_Core
{
    public class Team
    {
        public static int CurrentIdNum = 1;
        public int CurrentId { get; set; }
        public string Name { get; set; }
        public int CurrentScore { get; set; } = 0;
        public int TotalScore { get; set; } = 0;
        public int Victories { get; set; } = 0;
        public int Games { get; set; } = 0;

    }
}
