using System;
using System.Collections.Generic;
using System.Text;

namespace Alias_Core
{
    public class Round
    {
        public string TeamName { get; set; }
        public int WordsGuessed { get; set; }
        public int WordsSkipped { get; set; }
        public int PointsTotal { get; set; }
        public Round(string name)
        {
            TeamName = name;
            WordsGuessed = 0;
            WordsSkipped = 0;
        }
    }
}
