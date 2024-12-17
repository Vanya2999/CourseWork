using EngineLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;

namespace UnitTestGame
{
    [TestClass]
    public class ColliderTest
    {
        [TestMethod]
        public void TestCollider()
        {
            GameObject firstObject = new GameObject();
            firstObject.SetComponent(new ComponentTransform(new Vector2(1f, 1f), new Vector2(1, 1)));
            firstObject.SetComponent(new CheckCollision(firstObject, new Vector2(1, 1)));
            firstObject.GameObjectTag = "Test";

            GameObject secondObject = new GameObject();
            secondObject.SetComponent(new ComponentTransform(new Vector2(1f, 3f), new Vector2(1, 1)));
            secondObject.SetComponent(new CheckCollision(secondObject, new Vector2(1, 1)));
            secondObject.GameObjectTag = "Test";

            Assert.IsFalse(firstObject.Collider.IsCrossing("Test"));

            secondObject.Transform.ObjectPosition = new Vector2(1f, 1.5f);

            Assert.IsTrue(firstObject.Collider.IsCrossing("Test"));

            secondObject.GameObjectTag = "";

            Assert.IsFalse(firstObject.Collider.IsCrossing("Test"));
        }
    }
}