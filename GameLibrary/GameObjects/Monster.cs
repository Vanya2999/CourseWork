using EngineLibrary;
using GameLibrary;
using GameLibrary.Factory;
using GameLibrary.GameObjects;
using OpenTK;
using System;

namespace MazeForm.player
{
    /// <summary>
    /// Описание монстра.
    /// </summary>
    /// <seealso cref="EngineLibrary.ExecutionScript" />
    public class Monster : ExecutionScript
    {
        /// <summary>
        /// Запас здоровья монстра
        /// </summary>
        public int Health { get; private set; } = 2;

        /// <summary>
        /// Скорость монстра
        /// </summary>
        public float Speed { get; set; } = 150;

        /// <summary>
        /// Возможность двигаться у монстра
        /// </summary>
        public bool IsCanMove { get; set; } = true;
        /// <summary>
        /// Рандомер.
        /// </summary>
        private Random _random;
        /// <summary>
        /// Направления осей.
        /// </summary>
        private int _directionX = 0, _directionY = 0;
        /// <summary>
        /// Ссылка на игру.
        /// </summary>
        private Game _game;
        /// <summary>
        /// Таймер.
        /// </summary>
        private float _currentReloadTime;
        /// <summary>
        /// Направление взгляда игрока
        /// </summary>
        private Vector2 _view;
        /// <summary>
        /// Таймер.
        /// </summary>
        private float _timer = 0.5f;
        /// <summary>
        /// Время перезарядки.
        /// </summary>
        /// <value>
        /// Время перезарядки.
        /// </value>
        public float CooldownTime { get; } = 1f;
        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Start(GameObject gameObject = null)
        {
            _random = new Random();

            _game = Game.instance;

            if (_random.Next(0, 2) == 0)
                _directionX = 1;
            else
                _directionX = -1;
        }
        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Update(GameObject gameObject)
        {
            if (gameObject.IsActive && IsCanMove)
                Move(gameObject);

            if (IsCanMove && _currentReloadTime < GameTime.CurrentLaunchTime)
                Shoot(gameObject);
        }
        /// <summary>
        /// Выстрел игрового объекта.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        private void Shoot(GameObject gameObject)
        {
            Vector2 bulletSpawnPosition = gameObject.Transform.ObjectPosition;
            Vector2 bulletDirection = _view;
            SpawnBullet(bulletSpawnPosition, bulletDirection, gameObject.GameObjectTag, 1);

            _currentReloadTime = GameTime.CurrentLaunchTime + CooldownTime;
        }

        /// <summary>
        /// Создание пули из фабрики
        /// </summary>
        /// <param name="position">Позиция создания</param>
        /// <param name="direction">Направление пули</param>
        private void SpawnBullet(Vector2 position, Vector2 direction, string gameObjectTag, float power = 1)
        {
            BulletFactory factory = new BulletFactory();
            Game.instance.AddObjectOnScene(factory.CreateBullet(position, direction, gameObjectTag, power));
        }

        /// <summary>
        /// Метод движения игрока
        /// </summary>
        private void Move(GameObject gameObject)
        {
            if (_directionX > 0)
                gameObject.Texture.SetTexture("Right");
            else if (_directionX < 0)
                gameObject.Texture.SetTexture("Left");
            if (_directionY > 0)
                gameObject.Texture.SetTexture("Down");
            else if (_directionY < 0)
                gameObject.Texture.SetTexture("Up");

            if (_directionX != 0)
                _view = new Vector2(_directionX, 0);

            Vector2 direction = new Vector2(_directionX, _directionY);

            gameObject.Transform.SetMovement(direction * Speed * GameTime.DeltaTimeFrames);

            DetectCollision(gameObject);
        }

        /// <summary>
        /// Распознавание столкновений и реакция на них
        /// </summary>
        private void DetectCollision(GameObject gameObject)
        {
            if (gameObject.Collider.IsCrossing("Wall"))
            {
                gameObject.Transform.ResetMovement();

                if (_directionX == -1)
                    _directionX = 1;
                else if (_directionX == 1)
                    _directionX = -1;
            }

            if (gameObject.Collider.IsCrossing(out GameObject player, "PlayerOne", "PlayerTwo"))
            {
                //timer += GameTime.DeltaTimeFrames;
                if (player.GameObjectTag == "PlayerTwo")
                    player.Transform.ObjectPosition = _game.PlayerOneFactory.StartPosition;
                else
                    player.Transform.ObjectPosition = _game.PlayerTwoFactory.StartPosition;

                GamePlayer.SetPoins(player.GameObjectTag, -10);

                (player.Script as GamePlayer).Characteristic.SetCharacteristic(CharactersticType.Health, 10);

                if (player.GameObjectTag == "PlayerOne" && GamePlayer.FirstPlayerPoints != 1)
                    (player.Script as GamePlayer).ChangeStatsCharacteristic((-GamePlayer.FirstPlayerPoints / 2), "Death", player.GameObjectTag);
                else if (player.GameObjectTag == "PlayerTwo" && GamePlayer.SecondPlayerPoints != 1)
                    (player.Script as GamePlayer).ChangeStatsCharacteristic((-GamePlayer.SecondPlayerPoints / 2), "Death", player.GameObjectTag);
                else
                    (player.Script as GamePlayer).ChangeStatsCharacteristic(-1, "Death", player.GameObjectTag);
            }
        }

        /// <summary>
        /// Изменение значения характеристик игрока.
        /// </summary>
        /// <param name="value">Значение, которое прибавляется к текущему значению </param>
        public void ChangeStatsValue(GameObject gameObject, float value)
        {
            if (gameObject.Collider.IsCrossing("Bullet"))
            {
                Health += (int)value;
            }

            if (Health <= 0)
            {
                _game.AddObjectsToRemove(gameObject);
            }
        }
    }
}