using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProjectNew
{
    public class Game
    {
        public int Id { get; set; }

        public int TeamsAmount { get; set; }

        public List<Team> Teams { get; set; }

        public DateTime StartDt { get; set; }

        public DateTime EndDt { get; set; }

        //public DateTime Duration { get; set; }



    }
}
