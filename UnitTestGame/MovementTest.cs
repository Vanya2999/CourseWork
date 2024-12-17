using EngineLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;

namespace UnitTestGame
{
    [TestClass]
    public class MovementTest
    {
        [TestMethod]
        public void TestGameObjectMovement()
        {
            var gameObject = new GameObject();
            gameObject.SetComponent(new ComponentTransform(new Vector2(0, 0), new Vector2(1f, 1f)));

            Vector2 offset = new Vector2(1f, 1f);

            gameObject.Transform.SetMovement(offset);

            Vector2 expected = new Vector2(1f, 1f);
            Vector2 actual = gameObject.Transform.ObjectPosition;

            Assert.AreEqual(expected, actual);
        }
    }
}