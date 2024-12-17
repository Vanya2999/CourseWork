using GameLibrary.GameObjects;

namespace GameLibrary
{
    /// <summary>
    /// Декоратор характеристик
    /// </summary>
    /// <seealso cref="GameLibrary.GameObjects.PlayerCharacteristic" />
    public class CharactersticDecorator : PlayerCharacteristic
    {
        /// <summary>
        /// The player properities
        /// </summary>
        protected PlayerCharacteristic playerProperities;

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        /// <param name="playerProperities">The player properities.</param>
        public CharactersticDecorator(PlayerCharacteristic playerProperities)
        {
            this.playerProperities = playerProperities;
        }

        /// <summary>
        /// Запас здоровья игрока
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        public override int Health { get => playerProperities.Health; protected set => playerProperities.SetCharacteristic(CharactersticType.Health, value); }

        /// <summary>
        /// Боезапас
        /// </summary>
        /// <value>
        /// The ammunition.
        /// </value>
        public override int Ammunition { get => playerProperities.Ammunition; protected set => playerProperities.SetCharacteristic(CharactersticType.Ammunition, value); }

        /// <summary>
        /// Скорость
        /// </summary>
        /// <value>
        /// Скорость.
        /// </value>
        public override float Speed { get => playerProperities.Speed; }

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        /// <value>
        /// Время перезарядки оружия.
        /// </value>
        public override float CooldownTime { get => playerProperities.CooldownTime; }

        /// <summary>
        /// Сила
        /// </summary>
        /// <value>
        /// Сила.
        /// </value>
        public override float Power { get => playerProperities.Power; }
        /// <summary>
        /// Деактивация характеристик.
        /// </summary>
        /// <param name="player">The player.</param>
        protected override void DeactivateCharacteristic(GamePlayer player)
        {
            player.SetCharacteristic(new BasicCharacteristic());
        }
    }
}