using EngineLibrary;
using GameLibrary.Factory;
using GameLibrary.Maze;
using OpenTK;
using OpenTK.Input;
using static EngineLibrary.InputKeyboard;

namespace GameLibrary.GameObjects
{
    /// <summary>
    /// Поведение игрока.
    /// </summary>
    /// <seealso cref="EngineLibrary.ExecutionScript" />
    public class GamePlayer : ExecutionScript
    {
        /// <summary>
        /// Управление игроком
        /// </summary>
        public PlayerControl ControlPlayer { get; private set; }

        /// <summary>
        /// Возможность двигаться у игрока
        /// </summary>
        public bool IsCanMove { get; set; } = true;

        /// <summary>
        /// Получение очков первого персонажа.
        /// </summary>
        /// <value>
        /// Очки.
        /// </value>
        public static int FirstPlayerPoints { get; private set; } = 10;
        /// <summary>
        /// Получение очков второго персонажа.
        /// </summary>
        /// <value>
        /// Очки.
        /// </value>
        public static int SecondPlayerPoints { get; private set; } = 10;
        /// <summary>
        /// Получение характеристик игрока.
        /// </summary>
        /// <value>
        /// Характеристики.
        /// </value>
        public PlayerCharacteristic Characteristic { get; private set; }
        /// <summary>
        /// Таймер
        /// </summary>
        private float currentReloadTime;
        /// <summary>
        /// Направление взгяла игрока.
        /// </summary>
        private Vector2 view = new Vector2(0, 1);
        /// <summary>
        /// Присвоение характеристики.
        /// </summary>
        /// <param name="property">The property.</param>
        public void SetCharacteristic(PlayerCharacteristic property)
        {
            property.SetCharacteristic(CharactersticType.Health, Characteristic.Health);
            property.SetCharacteristic(CharactersticType.Ammunition, Characteristic.Ammunition);

            Characteristic = property;
        }
        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Start(GameObject gameObject = null)
        {
            Characteristic = new BasicCharacteristic();
            Characteristic.SetCharacteristic(CharactersticType.Health, 10);
            Characteristic.SetCharacteristic(CharactersticType.Ammunition, 10);

            if (gameObject.GameObjectTag == "PlayerOne")
                ControlPlayer = new PlayerControl(DirectionAxes.HorizontalAxis, DirectionAxes.VerticalAxis, Key.Space, Key.C);
            else if (gameObject.GameObjectTag == "PlayerTwo")
                ControlPlayer = new PlayerControl(DirectionAxes.AlternativeHorizontalAxis, DirectionAxes.AlternativeVerticalAxis, Key.K, Key.L);
        }
        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Update(GameObject gameObject)
        {
            if (gameObject.IsActive && IsCanMove)
                PlayerMove(gameObject);

            if (Characteristic != null)
                Characteristic.UpdateTime(this);

            if (IsCanMove && InputKeyboard.IsButtonDawn(ControlPlayer.ShootKey) && currentReloadTime < GameTime.CurrentLaunchTime && Characteristic.Ammunition > 0)
                Shoot(gameObject);

            if (Characteristic.Ammunition >= 0)
                GameEvents.ChangeCount?.Invoke(gameObject.GameObjectTag, Characteristic.Ammunition);

            GameEvents.ChangeHealth?.Invoke(gameObject.GameObjectTag, Characteristic.Health);
        }
        /// <summary>
        /// Выстрел.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        private void Shoot(GameObject gameObject)
        {
            Vector2 bulletSpawnPosition = gameObject.Transform.ObjectPosition;
            Vector2 bulletDirection = view;
            SpawnBullet(bulletSpawnPosition, bulletDirection, gameObject.GameObjectTag, Characteristic.Power);
            Characteristic.SetCharacteristic(CharactersticType.Ammunition, Characteristic.Ammunition - 1);

            currentReloadTime = GameTime.CurrentLaunchTime + Characteristic.CooldownTime;
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
        private void PlayerMove(GameObject gameObject)
        {
            int directionX, directionY;

            directionX = GetAxisDirection(ControlPlayer.HorizontalAxis);
            directionY = GetAxisDirection(ControlPlayer.VerticalAxis);

            if (directionX > 0)
                gameObject.Texture.SetTexture("Right");
            else if (directionX < 0)
                gameObject.Texture.SetTexture("Left");
            if (directionY > 0)
                gameObject.Texture.SetTexture("Down");
            else if (directionY < 0)
                gameObject.Texture.SetTexture("Up");

            if (directionX != 0)
                view = new Vector2(directionX, 0);
            else if (directionY != 0)
                view = new Vector2(0, directionY);

            Vector2 direction = new Vector2(directionX, directionY);

            gameObject.Transform.SetMovement(direction * Characteristic.Speed * GameTime.DeltaTimeFrames);

            CollisionDetector(gameObject);
        }

        /// <summary>
        /// Распознавание столкновений и реакция на них
        /// </summary>
        private void CollisionDetector(GameObject gameObject)
        {
            if (gameObject.Collider.IsCrossing("Wall"))
            {
                gameObject.Transform.ResetMovement();
            }
        }

        /// <summary>
        /// Изменение значения характеристик игрока
        /// </summary>
        /// <param name="value">Значение, которое прибавляется к текущему значению пуль</param>
        public void ChangeStatsCharacteristic(GameObject gameObject, float value)
        {
            if (gameObject.Collider.IsCrossing("Bullet") || Characteristic.Health <= 10)
            {
                Characteristic.SetCharacteristic(CharactersticType.Health, Characteristic.Health + value);
                GameEvents.ChangeHealth?.Invoke(gameObject.GameObjectTag, Characteristic.Health);
            }
        }
        /// <summary>
        /// Изменение характеристик.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <param name="gameObjectTag">Тэг текущего обьекта.</param>
        /// <param name="gametag">Тэг постраавшего.</param>
        public void ChangeStatsCharacteristic(float value , string gameObjectTag, string gametag)
        {
            if (gametag == "Death")
            {
                Characteristic.SetCharacteristic(CharactersticType.Health, value);
                GameEvents.ChangeHealth?.Invoke(gameObjectTag, Characteristic.Health);
            }
        }
        /// <summary>
        /// Изменение количества очков для игроков.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public static void SetPoins(string tag, int value)
        {
            if (tag == "PlayerOne")
            {
                FirstPlayerPoints += value;
                GameEvents.ChangePoins?.Invoke(tag, value);
            }
            else
            {
                SecondPlayerPoints += value;
                GameEvents.ChangePoins?.Invoke(tag, value);
            }
        }
    }
}