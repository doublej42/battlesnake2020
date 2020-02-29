using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSnake2020.Models.Snake
{
    public partial class Board
    {
        public long Height { get; set; }
        public long Width { get; set; }
        public Location[] Food { get; set; }
        public Snake[] Snakes { get; set; }
    }
}
