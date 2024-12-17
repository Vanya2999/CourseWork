using EngineLibrary;
using GameLibrary.GameObjects;
using GameLibrary.Maze;
using MazeForm.player;
using OpenTK;
using System;
using System.Collections.Generic;

namespace GameLibrary
{
    /// <summary>
    /// Описание игрового поля.
    /// </summary>
    /// <seealso cref="OpenTK.GameWindow" />
    public class Game : GameWindow
    {
        /// <summary>
        /// Статическая ссылка на класс
        /// </summary>
        public static Game instance = null;

        /// <summary>
        /// Получение ширины окна.
        /// </summary>
        /// <value>
        /// Ширина окна.
        /// </value>
        public int WidthOfApplication { get; private set; } = 1280;
        /// <summary>
        /// Получение высоты окна.
        /// </summary>
        /// <value>
        /// Высота окна.
        /// </value>
        public int HeightOfApplication { get; private set; } = 720;
        /// <summary>
        /// Массив координат блоков.
        /// </summary>
        /// <value>
        /// Координаты блоков.
        /// </value>
        public List<Vector2> EmptyBlocks { get; private set; } = new List<Vector2>();
        /// <summary>
        /// Список объектов.
        /// </summary>
        private List<GameObject> gameObjects = new List<GameObject>();
        /// <summary>
        /// Спиок удаляемых объектов.
        /// </summary>
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
        /// <summary>
        /// Список добавляемых игровых объектов.
        /// </summary>
        private List<GameObject> gameObjectsToAdd = new List<GameObject>();

        /// <summary>
        /// Конструктор первого игрока
        /// </summary>
        /// <value>
        /// Игрок первый.
        /// </value>
        public PlayerDesigner PlayerOneFactory { get; set; }

        /// <summary>
        /// Конструктор второго игрока
        /// </summary>
        /// <value>
        /// Игрок воторой.
        /// </value>
        public PlayerDesigner PlayerTwoFactory { get; set; }

        /// <summary>
        /// Конструктор <see cref="Game"/> класса.
        /// </summary>
        public Game()
        {
            if (instance == null)
                instance = this;

            PlayerOneFactory = new PlayerDesigner();
            PlayerTwoFactory = new PlayerDesigner();

            MazeField maze = new MazeField();

            maze.CreateGameObjectsOnScene();
            maze.CreateMaze();

            EmptyBlocks = maze.EmptyBlocks;

            var gameObject3 = new GameObject();
            gameObject3.SetComponent(new ComponentTransform(new Vector2(0f, 0f), new Vector2(1, 1)));
            gameObject3.GameObjectTag = "GameManager";

            var script3 = new SpawnManager();
            script3.Start(gameObject3);

            gameObject3.SetComponent(script3);

            GameObject gameObject4 = new GameObject();
            gameObject4.SetComponent(new ComponentTransform(new Vector2(0f, 0f), new Vector2(2.1f, 1.7f)));
            gameObject4.SetComponent(new TexturesBox(Content.Load("Фон.png")));
            gameObject4.GameObjectTag = "Background";

            gameObjects.Add(gameObject4);
            gameObjects.Add(PlayerTwoFactory.CreatePlayer("PlayerOne"));
            gameObjects.Add(PlayerOneFactory.CreatePlayer("PlayerTwo"));
            gameObjects.Add(gameObject3);
        }

        /// <summary>
        /// Добавление объекта в лист отрисовки.
        /// </summary>
        /// <param name="gameObject">Игровой объект</param>
        public void AddObjectOnScene(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        /// <summary>
        /// Удаление игровых объектов.
        /// </summary>
        private void RemoveRenderGameObjects()
        {
            foreach (GameObject removeGameObject in gameObjectsToRemove)
            {
                gameObjects.Remove(removeGameObject);
            }

            gameObjectsToRemove.Clear();
        }

        /// <summary>
        /// Adds the render game objects.
        /// </summary>
        private void AddRenderGameObjects()
        {
            gameObjects.AddRange(gameObjectsToAdd);
            gameObjectsToAdd.Clear();
        }

        /// <summary>
        /// Добавление объекта для удаления.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public void AddObjectsToRemove(GameObject gameObject)
        {
            int count = 0;

            if (gameObject.GameObjectTag == "Spawn")
                EmptyBlocks.Add(gameObject.Transform.ObjectPosition / (instance.HeightOfApplication / 15));

            gameObjectsToRemove.Add(gameObject);

            foreach (var monsterObject in gameObjects)
            {
                if (monsterObject.Script is Monster)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                EndScene();
            }
        }

        /// <summary>
        /// Рендер объектов.
        /// </summary>
        public void Rendering()
        {
            GameTime.UpdateGameTime();

            RemoveRenderGameObjects();
            AddRenderGameObjects();

            foreach (var obj in gameObjects)
            {
                obj.Update();

                if (obj.Texture != null)
                    DrawingTexture.Draw(obj);
            }
        }

        /// <summary>
        /// Рандомное место в лабиринте.
        /// </summary>
        /// <returns>
        /// Позицию
        /// </returns>
        public Vector2 GetRandomPosition()
        {
            Random random = new Random();

            int index = random.Next(0, EmptyBlocks.Count);

            Vector2 position = EmptyBlocks[index];

            EmptyBlocks.Remove(position);

            return position * instance.HeightOfApplication / 15;
        }

        /// <summary>
        /// Поведение при завершении сцены.
        /// </summary>
        protected void EndScene()
        {
            string winPlayer;

            if (GamePlayer.SecondPlayerPoints < GamePlayer.FirstPlayerPoints)
                winPlayer = PlayerTwoFactory.PlayerTag;
            else
                winPlayer = PlayerOneFactory.PlayerTag;

            GameEvents.EndGame?.Invoke(winPlayer);
        }
    }
}