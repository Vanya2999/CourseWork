using EngineLibrary;
using GameLibrary.Factory;
using OpenTK;

namespace GameLibrary.GameObjects
{
    /// <summary>
    ///  Поведение пули.
    /// </summary>
    public class Bullet : ExecutionScript
    {
        /// <summary>
        /// Скорость пули
        /// </summary>
        public float Speed { get; } = 300;

        /// <summary>
        /// Экземпляр сцены игры
        /// </summary>
        protected Game _game;
        /// <summary>
        /// Направление полёта.
        /// </summary>
        private Vector2 _flyDirection;
        /// <summary>
        /// Массив тегов-объектв, с которыми проходит проверка на пересечение.
        /// </summary>
        private string[] _interactionTag = new string[2];
        /// <summary>
        /// Тэг объекта к которым проводится проверка на пересчение.
        /// </summary>
        protected string _tag;
        /// <summary>
        /// Сила пули.
        /// </summary>
        protected float _power;

        /// <summary>
        /// Установление направления полета пули
        /// </summary>
        /// <param name="direction">Вектор направления</param>
        /// <param name="tag">Тег игрового объекта, создающий пулю</param>
        public void SetSettings(Vector2 direction, string tag, float power)
        {
            this._tag = tag;
            _flyDirection = direction;
            this._power = power;

            if (tag == "Monster")
            {
                _interactionTag[0] = "PlayerOne";
                _interactionTag[1] = "PlayerTwo";
            }
            else
            {
                _interactionTag[0] = "Monster";
            }
        }

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start(GameObject gameObject = null)
        {
            _game = Game.instance;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update(GameObject gameObject)
        {
            Vector2 movement = _flyDirection * Speed * GameTime.DeltaTimeFrames;

            gameObject.Transform.SetMovement(movement);

            if (_flyDirection.X > 0)
                gameObject.Texture.SetTexture("Right");
            else if (_flyDirection.X < 0)
                gameObject.Texture.SetTexture("Left");
            else if (_flyDirection.Y > 0)
                gameObject.Texture.SetTexture("Down");
            else if (_flyDirection.Y < 0)
                gameObject.Texture.SetTexture("Up");

            if (gameObject.Collider.IsCrossing("Wall"))
            {
                _game.AddObjectsToRemove(gameObject);
            }

            if (gameObject.Collider.IsCrossing(out GameObject monsterGameObject, _interactionTag))
            {
                if (_tag != monsterGameObject.GameObjectTag)
                {
                    PlayerInteraction(monsterGameObject);
                    _game.AddObjectsToRemove(gameObject);
                }
            }
        }

        /// <summary>
        /// Взаимодействие с игроком
        /// </summary>
        public void PlayerInteraction(GameObject playerGameObject)
        {
            DamageFactory factory = new DamageFactory();
            _game.AddObjectOnScene(factory.CreateDamage(playerGameObject, _tag, _power));
        }
    }
}