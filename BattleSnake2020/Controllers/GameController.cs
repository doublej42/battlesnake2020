using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSnake2020.Models;
using BattleSnake2020.Models.Snake;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleSnake2020.Controllers
{
    [Route("")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpPost("start")]
        public ReturnStart Start(GameState gameState)
        {
            return new ReturnStart()
            {
                Color = "#240A40",
                HeadType = "dead",
                TailType = "block-bum"
            };
        }

        [HttpPost("move")]
        public ReturnMove Move(GameState gameState)
        {
            const double snakeRiskFactor = 1;
            const double wallRiskFactor = 1;

            var scoring = new Dictionary<string,double>();
            var head = gameState.You.Body.GetHead();
            var bestMove = Direction.All.First();
            foreach (var direction in Direction.All)
            {
                var nextLocation = head.NextLocation(direction);
                var allSnakes = gameState.Board.Snakes.AllPoints();
                var allWalls = gameState.Board.AllWalls();
                var badScore = nextLocation.AverageDistance(allSnakes);
                var goodScore = nextLocation.AverageDistance(gameState.Board.Food);
                var wallScore = nextLocation.AverageDistance(allWalls);
                //scoring[direction.Value] = -1 * goodScore;

                //A high score is good
                scoring[direction.Value] = (badScore * snakeRiskFactor)  + (wallScore * wallRiskFactor)  - goodScore;
                if (nextLocation.Collide(allSnakes) || nextLocation.Collide(allWalls))
                {
                    scoring[direction.Value] = long.MinValue;
                }
                Console.WriteLine("Turn:" + gameState.Turn + " Direction: " + direction.Value 
                                  + " goodScore:" + goodScore
                                  + " badScore:" + badScore
                                  + " wallScore:" + wallScore
                                  + " score:" + scoring[direction.Value]);
                if (scoring[bestMove.Value] < scoring[direction.Value])
                {
                    Console.WriteLine("Best Move:" + direction.Value);
                    bestMove = direction;
                }

               
            }

            return new ReturnMove{Move = bestMove.Value, Shout = ""};
        }

        [HttpPost("end")]
        public IActionResult End(GameState gameState)
        {
            return Content("");
        }


        [HttpPost("ping")]
        public IActionResult Ping()
        {
            return Content("");
        }

    }
}