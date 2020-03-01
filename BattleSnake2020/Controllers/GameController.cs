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
                TailType = "pixel"
            };
        }

        [HttpPost("move")]
        public ReturnMove Move(GameState gameState)
        {
            const double snakeRiskFactor = 2.5;
            const double wallRiskFactor = 1;
            const double foodRiskFactor = 2.7;

            var scoring = new Dictionary<string,double>();
            var head = gameState.You.Body.GetHead();
            var bestMove = Direction.All.First();
            foreach (var direction in Direction.All)
            {
                var nextLocation = head.NextLocation(direction);
                var allSnakes = gameState.Board.Snakes.AllPoints();
                var allWalls = gameState.Board.AllWalls();
                var snakeScore = nextLocation.AverageDistancePow(allSnakes, snakeRiskFactor);
                var wallScore = nextLocation.AverageDistancePow(allWalls, wallRiskFactor);
                var foodScore = nextLocation.AverageDistancePow(gameState.Board.Food, foodRiskFactor);
                
                //scoring[direction.Value] = -1 * goodScore;

                //A high score is good
                scoring[direction.Value] = snakeScore + wallScore - foodScore;

                var allOtherLargerSnakeHeads = gameState.Board.Snakes.AllOtherLargerSnakeHeads(gameState.You);
                var minSnake = nextLocation.MinDistance(allOtherLargerSnakeHeads);
                if (minSnake <= 1)
                {
                    Console.WriteLine("Too close to other snake:"  + minSnake);
                    scoring[direction.Value] = -10000000 + 100 * minSnake;
                }

                // var allOtherSmallerSnakeHeads = gameState.Board.Snakes.AllOtherSmallerSnakeHeads(gameState.You);
                // var minSmallerSnake = nextLocation.MinDistance(allOtherSmallerSnakeHeads);
                // if (minSmallerSnake <= 2)
                // {
                //     Console.WriteLine("MURDER!!:" + minSmallerSnake);
                //     scoring[direction.Value] = scoring[direction.Value] * 10;
                // }

                if (nextLocation.Collide(allSnakes) || nextLocation.Collide(allWalls))
                {
                    scoring[direction.Value] = -10000000;
                }
                Console.WriteLine("Turn:" + gameState.Turn + " Direction: " + direction.Value 
                                  + " foodScore:" + foodScore
                                  + " snakeScore:" + snakeScore
                                  + " wallScore:" + wallScore
                                  + " score:" + scoring[direction.Value]);
                if (scoring[bestMove.Value] < scoring[direction.Value])
                {
                    
                    bestMove = direction;
                }
            }
            Console.WriteLine("Best Move:" + bestMove.Value);
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