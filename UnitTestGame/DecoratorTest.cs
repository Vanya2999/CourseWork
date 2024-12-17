using GameLibrary;
using GameLibrary.GameObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestGame
{
    [TestClass]
    public class DecoratorTest
    {
        private BasicCharacteristic playerProperities;

        [TestInitialize]
        public void InitalizeBullet()
        {
            playerProperities = new BasicCharacteristic();
            playerProperities.SetCharacteristic(CharactersticType.Health, 10);
            playerProperities.SetCharacteristic(CharactersticType.Ammunition, 10);
        }

        [TestMethod]
        public void TestSpeedDecorator()
        {
            SpeedDecorator speedDecorator = new SpeedDecorator(playerProperities);

            float expectedSpeed = playerProperities.Speed * 1.5f;

            Assert.AreEqual(expectedSpeed, speedDecorator.Speed);
        }

        [TestMethod]
        public void TestPowerDecorator()
        {
            PowerDecorator powerDecorator = new PowerDecorator(playerProperities);

            float expectedPower = playerProperities.Power * 2;

            Assert.AreEqual(expectedPower, powerDecorator.Power);
        }

        [TestMethod]
        public void TestReloadTimeDecorator()
        {
            CooldownTimeDecorator reloadTimeDecorator = new CooldownTimeDecorator(playerProperities);

            float expectedReloadTime = playerProperities.CooldownTime / 2;

            Assert.AreEqual(expectedReloadTime, reloadTimeDecorator.CooldownTime);
        }
    }
}