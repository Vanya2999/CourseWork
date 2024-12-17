using GameLibrary.GameObjects;

namespace GameLibrary
{
    /// <summary>
    /// Класс с характеристиками игрока
    /// </summary>
    public class BasicCharacteristic : PlayerCharacteristic
    {
        /// <summary>
        /// Запас здоровья игрока
        /// </summary>
        public override int Health { get; protected set; }

        /// <summary>
        /// Боезапас
        /// </summary>
        public override int Ammunition { get; protected set; }

        /// <summary>
        /// Скорость
        /// </summary>
        public override float Speed { get; } = 150;

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public override float CooldownTime { get; } = 0.5f;

        /// <summary>
        /// Сила
        /// </summary>
        public override float Power { get; } = 1;
        /// <summary>
        /// Деактивация характеристик.
        /// </summary>
        /// <param name="player">The player.</param>
        protected override void DeactivateCharacteristic(GamePlayer player)
        {
        }
    }
}