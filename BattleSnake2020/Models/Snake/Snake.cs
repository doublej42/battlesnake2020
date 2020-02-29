using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSnake2020.Models.Snake
{
    public class Snake
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Health { get; set; }
        public Location[] Body { get; set; }
        public string Shout { get; set; }
    }
}
