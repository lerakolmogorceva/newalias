﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alias_Core
{
    public class Game
    {
        public static int CurrentId = 0;
        public int Id { get; set; }

        public int TeamsAmount { get; set; }

        public List<Team> Teams { get; set; }

        public DateTime StartDt { get; set; }

        public DateTime EndDt { get; set; }
        public int Rounds { get; set; } = 0;
        //public DateTime Duration { get; set; }



    }
}
