using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using BattleSnake2020.Models.Snake;

namespace BattleSnake2020.Models
{
    public static class LocationHelper
    {
        public static double Distance(this Location start, Location destination)
        {
            return Math.Sqrt(Math.Pow(Math.Abs((double) (start.X - destination.X)), 2) +
                             Math.Pow(Math.Abs((double) (start.Y - destination.Y)), 2));
        }

        public static double AverageDistance(this Location start, Location[] points)
        {
            if (points.Length == 0) return 0;
            double totalDistance = 0;
            foreach (var point in points)
            {
                totalDistance = start.Distance(point);
            }

            return totalDistance / points.Length;
        }

        public static bool Collide(this Location start, Location[] points)
        {
            return points.Any(point => start.X == point.X & start.Y == point.Y);
        }

        public static Location GetHead(this Location[] points)
        {
            return points[0];
        }

        public static Location NextLocation(this Location start, Direction direction)
        {
            
            if (direction.Value == Direction.Up.Value)
            {
                return new Location {X = start.X , Y = start.Y - 1};
            }
            if (direction.Value == Direction.Down.Value)
            {
                return new Location { X = start.X, Y = start.Y + 1 };
            }
            if (direction.Value == Direction.Left.Value)
            {
                return new Location { X = start.X - 1, Y = start.Y};
            }
            if (direction.Value == Direction.Right.Value)
            {
                return new Location { X = start.X + 1, Y = start.Y};
            }

            return start;
        }

        public static Location[] AllPoints(this Snake.Snake[] snakes)
        {
            var ret = new List<Location>();
            foreach (var snake in snakes)
            {
                ret.AddRange(snake.Body);
            }

            return ret.ToArray();
        }

        public static Location[] AllWalls(this Board board)
        {
            var ret = new List<Location>();
            for (var x = 0; x < board.Width; x++)
            {
                ret.Add(new Location { X = x, Y = -1});
                ret.Add(new Location { X = x, Y = board.Height });
            }
            for (var y = 0; y < board.Height; y++)
            {
                ret.Add(new Location { X = -1, Y = y});
                ret.Add(new Location { X = board.Width, Y = y });
            }
            return ret.ToArray();
        }

    }
}
