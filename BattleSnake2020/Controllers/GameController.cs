using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return new ReturnMove()
            {
                Move = "down",
                Shout = "sample"
            };
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