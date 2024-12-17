using EngineLibrary;
using GameLibrary.GameObjects;
using OpenTK;

namespace GameLibrary.Factory
{
    public class BulletFactory
    {
        /// <summary>
        /// Создание игрового объекта пули.
        /// </summary>
        /// <param name="position">Позиция появления пули</param>
        /// <param name="direction">Направление пули</param>
        /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateBullet(Vector2 position, Vector2 direction, string tag, float power = 1)
        {
            GameObject gameObject = new GameObject();
            gameObject.SetComponent(new ComponentTransform(position, new Vector2(1f, 1f)));

            TexturesBox texture;
            texture = new TexturesBox(Content.Load("Bullets/b_right.png"));
            texture.AddTexture("Down", Content.Load("Bullets/b_down.png"));
            texture.AddTexture("Up", Content.Load("Bullets/b_up.png"));
            texture.AddTexture("Left", Content.Load("Bullets/b_left.png"));
            texture.AddTexture("Right", Content.Load("Bullets/b_right.png"));

            gameObject.SetComponent(texture);
            gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(20, 20), new Vector2(10, 10)));
            gameObject.GameObjectTag = "Bullet";

            Bullet bullet = new Bullet();
            bullet.SetSettings(direction, tag, power);
            bullet.Start(gameObject);

            gameObject.SetComponent(bullet);

            return gameObject;
        }
    }
}