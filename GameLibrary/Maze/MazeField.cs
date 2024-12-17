using EngineLibrary;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameLibrary.Maze
{
    /// <summary>
    /// Игровое поле
    /// </summary>
    public class MazeField
    {
        /// <summary>
        /// Фабрика создания элементов лабиринта
        /// </summary>
        /// <value>
        /// Элменты лабиринта.
        /// </value>
        public MazeElementsFactory ElementsFactory { get; private set; }

        /// <summary>
        /// Получение блоков.
        /// </summary>
        /// <value>
        /// Блоки.
        /// </value>
        public List<Vector2> EmptyBlocks { get; private set; } = new List<Vector2>();

        /// <summary>
        /// Создание игровых элментов на сцене.
        /// </summary>
        public void CreateGameObjectsOnScene()
        {
            ElementsFactory = new MazeElementsFactory();
        }

        /// <summary>
        /// Метод создания лабиринта
        /// </summary>
        public void CreateMaze()
        {
            var game = Game.instance;

            Random random = new Random();

            float worldScale = Game.instance.HeightOfApplication / 15;

            Bitmap bitmap = new Bitmap(@"Resources\Mazes\Maze_45.bmp");

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    System.Drawing.Color color = bitmap.GetPixel(j, i);

                    GameObject gameObject = null;

                    if (color.R == 0 && color.G == 0 && color.B == 0)
                        gameObject = ElementsFactory.CreateMazeElement(new Vector2(j, i) * worldScale, "Wall");
                    else if (color.R == 255 && color.G == 0 && color.B == 0)
                        game.PlayerOneFactory.StartPosition = new Vector2(j, i) * worldScale;
                    else if (color.R == 0 && color.G == 0 && color.B == 255)
                        game.PlayerTwoFactory.StartPosition = new Vector2(j, i) * worldScale;
                    else if (color.R == 0 && color.G == 255 && color.B == 0)
                        gameObject = ElementsFactory.CreateMonsters(new Vector2(j, i) * worldScale);
                    else
                        EmptyBlocks.Add(new Vector2(j, i));

                    if (gameObject != null)
                        game.AddObjectOnScene(gameObject);
                }
            }
        }
    }
}