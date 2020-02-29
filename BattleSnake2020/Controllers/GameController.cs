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
            const double snakeRiskFactor = 2;
            const double wallRiskFactor = 2;
            const double foodRiskFactor = 4;

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
                if (nextLocation.Collide(allSnakes) || nextLocation.Collide(allWalls))
                {
                    scoring[direction.Value] = long.MinValue;
                }

                var allOtherSnakes = gameState.Board.Snakes.AllOtherSnakePoints(gameState.You);
                var minSnake = nextLocation.MinDistance(allOtherSnakes);
                if (minSnake < 2)
                {
                    scoring[direction.Value] += minSnake;
                }
                Console.WriteLine("Turn:" + gameState.Turn + " Direction: " + direction.Value 
                                  + " foodScore:" + foodScore
                                  + " snakeScore:" + snakeScore
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