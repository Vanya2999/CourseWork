using EngineLibrary;
using MazeForm.player;
using OpenTK;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Класс фабрики создания элементов лабиринта
    /// </summary>
    public class MazeElementsFactory
    {
        /// <summary>
        /// Создает элемент лабиринта
        /// </summary>
        /// <param name="position">Позиция объекта на сцене</param>
        /// <param name="TagName">Тег игрового объекта</param>
        /// <returns>Созданный игровой объект</returns>
        public GameObject CreateMazeElement(Vector2 position, string TagName)
        {
            float mapScale = Game.instance.HeightOfApplication / 15;

            GameObject gameObject = new GameObject();
            gameObject.SetComponent(new TexturesBox(Content.Load("MazeElements/" + TagName + ".png")));
            gameObject.SetComponent(new ComponentTransform(new Vector2(position.X, position.Y), new Vector2(1, 1) * mapScale / gameObject.Texture.Texture.Width));

            if (TagName == "Wall")
                gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(mapScale, mapScale)));
            else
                gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(10f, 10f)));

            gameObject.GameObjectTag = TagName;

            return gameObject;
        }

        /// <summary>
        /// Создание монстров в лабиринте
        /// </summary>
        /// <param name = "position" > Позиция объекта на сцене</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateMonsters(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.SetComponent(new ComponentTransform(position, new Vector2(1f, 1f)));

            TexturesBox texture1;
            texture1 = new TexturesBox(Content.Load("m_straight.png"));
            texture1.AddTexture("Down", Content.Load("m_straight.png"));
            texture1.AddTexture("Up", Content.Load("m__back.png"));
            texture1.AddTexture("Left", Content.Load("m_left.png"));
            texture1.AddTexture("Right", Content.Load("m_right.png"));

            gameObject.SetComponent(texture1);

            var script = new Monster();
            script.Start(gameObject);

            gameObject.SetComponent(script);

            gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(gameObject.Texture.Texture.Width - 20, gameObject.Texture.Texture.Height - 10), new Vector2(10, 5)));

            gameObject.GameObjectTag = "Monster";

            return gameObject;
        }
    }
}