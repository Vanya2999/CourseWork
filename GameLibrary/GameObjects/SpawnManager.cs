using EngineLibrary;
using GameLibrary.Factory;
using OpenTK;
using System;

namespace GameLibrary.GameObjects
{
    /// <summary>
    /// Класс игрового менеджера
    /// </summary>
    /// <seealso cref="EngineLibrary.ExecutionScript" />
    public class SpawnManager : ExecutionScript
    {
        /// <summary>
        /// Время, через которое спавнится приз
        /// </summary>
        private const float _timeToSpawn = 0.5f;
        /// <summary>
        /// Таймер
        /// </summary>
        private float _currentTimeToSpawn;

        /// <summary>
        /// Ссылка на игру
        /// </summary>
        private Game _game;
        /// <summary>
        /// Фабрика спавна
        /// </summary>
        private PrizeFactory _spawnFactory;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Start(GameObject gameObject)
        {
            _game = Game.instance;
            _currentTimeToSpawn = GameTime.CurrentLaunchTime;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Update(GameObject gameObject)
        {
            if (_currentTimeToSpawn < GameTime.CurrentLaunchTime)
            {
                Random random = new Random();
                int chance = random.Next(0, 101);

                if (_game.EmptyBlocks.Count == 0) return;

                Vector2 position = _game.GetRandomPosition();

                if (chance < 20)
                {
                    _spawnFactory = new SpeedPrizeFactory();
                }
                else if (chance > 20 && chance <= 40)
                {
                    _spawnFactory = new BulletPowerPrizeFactory();
                }
                else if (chance > 40 && chance <= 60)
                {
                    _spawnFactory = new CooldownPrizeFactory();
                }
                else if (chance > 60 && chance <= 80)
                    _spawnFactory = new AmmunitionPrizeFactory();
                else
                    _spawnFactory = new HealthPrizeFactory();

                _game.AddObjectOnScene(_spawnFactory.CreatePrize(position));

                _currentTimeToSpawn += _timeToSpawn;
            }
        }
    }
}