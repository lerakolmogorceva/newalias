using System;
using System.Collections.Generic;
using System.Text;

namespace Alias_Core
{
    public class Round
    {
        public int WordsGuessed { get; set; }
        public int WordsSkipped { get; set; }
        public string TeamName { get; set; }
        public int PointsTotal { get; set; }
        public Round(string name)
        {
            this.TeamName = name;
            this.WordsGuessed = 0;
            this.WordsSkipped = 0;
        }
    }
}
