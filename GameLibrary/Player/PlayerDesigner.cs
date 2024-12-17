using EngineLibrary;
using GameLibrary.GameObjects;
using OpenTK;

namespace GameLibrary
{
    /// <summary>
    /// Конcтруктор игрока/
    /// </summary>
    public class PlayerDesigner
    {
        /// <summary>
        /// Тег плеера
        /// </summary>
        /// <value>
        /// Тэг игрока.
        /// </value>
        public string PlayerTag { get; private set; }

        /// <summary>
        /// Начальная позиция игрока в лабиринте
        /// </summary>
        /// <value>
        /// The start position.
        /// </value>
        public Vector2 StartPosition { get; set; }

        /// <summary>
        /// Создание игрового объекта персонажа
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// Игровой объект
        /// </returns>
        public GameObject CreatePlayer(string path)
        {
            PlayerTag = path;

            var gameObject = new GameObject();
            gameObject.GameObjectTag = path;

            TexturesBox texture1;
            texture1 = new TexturesBox(Content.Load(path + "_straight.png"));
            
            texture1.AddTexture("Down", Content.Load(path + "_straight.png"));
            texture1.AddTexture("Up", Content.Load(path + "__back.png"));
            texture1.AddTexture("Left", Content.Load(path + "_left.png"));
            texture1.AddTexture("Right", Content.Load(path + "_right.png"));

            var script = new GamePlayer();
            script.Start(gameObject);

            gameObject.SetComponent(texture1);
            gameObject.SetComponent(script);
            gameObject.SetComponent(new ComponentTransform(StartPosition, new Vector2(1f, 1f)));
            gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(gameObject.Texture.Texture.Width - 20, gameObject.Texture.Texture.Height - 10), new Vector2(10, 5)));

            return gameObject;
        }
    }
}