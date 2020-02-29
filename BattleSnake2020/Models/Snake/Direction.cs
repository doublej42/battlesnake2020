using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BattleSnake2020.Models.Snake
{
    public class Direction
    {

        private Direction(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static List<Direction> All => new List<Direction> {Up, Down, Left, Right};

        public static Direction Up => new Direction("up");
        public static Direction Down => new Direction("down");
        public static Direction Left => new Direction("left");
        public static Direction Right => new Direction("right");
    }
}
