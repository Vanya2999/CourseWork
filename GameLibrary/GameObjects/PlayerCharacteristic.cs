using EngineLibrary;

namespace GameLibrary.GameObjects
{
    /// <summary>
    /// Характеристики игрока.
    /// </summary>
    public abstract class PlayerCharacteristic
    {
        /// <summary>
        /// Игрок
        /// </summary>
        protected GamePlayer gamePlayer;

        /// <summary>
        /// Время декативации.
        /// </summary>
        protected float timeDeactivate = 4;
        /// <summary>
        /// Таймер
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Запас здоровья игрока
        /// </summary>
        /// <value>
        /// Здоровье.
        /// </value>
        public abstract int Health { get; protected set; }

        /// <summary>
        /// Боезапас
        /// </summary>
        /// <value>
        /// Амуниция.
        /// </value>
        public abstract int Ammunition { get; protected set; }

        /// <summary>
        /// Скорость
        /// </summary>
        /// <value>
        /// Скорость.
        /// </value>
        public abstract float Speed { get; }

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        /// <value>
        /// Время перезарядки оружия.
        /// </value>
        public abstract float CooldownTime { get; }

        /// <summary>
        /// Сила.
        /// </summary>
        /// <value>
        /// Сила.
        /// </value>
        public abstract float Power { get; }

        /// <summary>
        /// Присвоение характеристик.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public virtual void SetCharacteristic(CharactersticType type, float value)
        {
            switch (type)
            {
                case CharactersticType.Health:
                    Health = (int)value;
                    break;

                case CharactersticType.Ammunition:
                    Ammunition = (int)value;
                    break;
            }
        }

        /// <summary>
        /// Обновление врмени действия приза.
        /// </summary>
        /// <param name="player">The player.</param>
        public virtual void UpdateTime(GamePlayer player)
        {
            timer += GameTime.DeltaTimeFrames;

            if (timer >= timeDeactivate)
            {
                timer = 0;
                DeactivateCharacteristic(player);
            }
        }

        /// <summary>
        ///Деактивация характеристик.
        /// </summary>
        /// <param name="player">The player.</param>
        protected abstract void DeactivateCharacteristic(GamePlayer player);
    }
}