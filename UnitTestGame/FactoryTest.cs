using GameLibrary.Factory;
using GameLibrary.GameObjects;
using GameLibrary.Prize;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;

namespace UnitTestGame
{
    [TestClass]
    public class FactoryTest : GameWindow
    {
        [TestMethod]
        public void TestMethodFactoryBullet()
        {
            var factory = new BulletFactory();

            var bullet = factory.CreateBullet(new Vector2(1, 1), new Vector2(1, 1), "Bullet");
            Assert.IsTrue(bullet.Script is Bullet);
        }

        [TestMethod]
        public void TestMethodFactoryPrize1()
        {
            var factory = new AmmunitionPrizeFactory();

            var prize = factory.CreatePrize(new Vector2(1, 1));
            Assert.IsTrue(prize.Script is PrizeSpawn);
        }

        [TestMethod]
        public void TestMethodFactoryPrize2()
        {
            var factory = new HealthPrizeFactory();

            var prize = factory.CreatePrize(new Vector2(1, 1));
            Assert.IsTrue(prize.Script is PrizeSpawn);
        }

        [TestMethod]
        public void TestMethodFactoryPrize3()
        {
            var factory = new CooldownPrizeFactory();

            var prize = factory.CreatePrize(new Vector2(1, 1));
            Assert.IsTrue(prize.Script is PrizeSpawn);
        }

        [TestMethod]
        public void TestMethodFactoryPrize4()
        {
            var factory = new SpeedPrizeFactory();

            var prize = factory.CreatePrize(new Vector2(1, 1));
            Assert.IsTrue(prize.Script is PrizeSpawn);
        }
    }
}