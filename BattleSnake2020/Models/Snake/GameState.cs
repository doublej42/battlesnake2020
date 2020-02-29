

namespace BattleSnake2020.Models.Snake
{
    public partial class GameState
    {
        public Game Game { get; set; }

        public long Turn { get; set; }

        public Board Board { get; set; }

        public Snake You { get; set; }
    }
}
