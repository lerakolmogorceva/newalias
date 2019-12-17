using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_Core
{
    public class Team
    {
        public int CurrentId { get; set; }
        public static int CurrentIdNum = 1;
        //ID in current game
        public int TotalId { get; set; }
        public static int TotalIdNum = 1;
        //ID amongst ALL teams

        public int CurrentScore { get; set; } = 0;
        public int TotalScore { get; set; } = 0;
        public string Name { get; set; }
        public int Victories { get; set; } = 0;
        public int Games { get; set; } = 0;

    }
}
