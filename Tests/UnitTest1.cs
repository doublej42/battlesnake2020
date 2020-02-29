using BattleSnake2020.Models;
using BattleSnake2020.Models.Snake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AverageDistance()
        {
            var me = new Location() {X = 5, Y = 5};
            var food = new Location()
                {X = 4, Y = 5};
            var foods = new Location[] {food};
            var distanceToFood = me.AverageDistance(foods);
            Assert.AreEqual(1, distanceToFood );
        }
    }
}
