using GameLibrary.GameObjects;

namespace GameLibrary
{
    /// <summary>
    /// Декоратор скорости.
    /// </summary>
    /// <seealso cref="GameLibrary.CharactersticDecorator" />
    public class SpeedDecorator : CharactersticDecorator
    {
        /// <summary>
        /// Инициализация <see cref="SpeedDecorator"/> класса.
        /// </summary>
        /// <param name="playerProperities">The player properities.</param>
        public SpeedDecorator(PlayerCharacteristic playerProperities) : base(playerProperities)
        {
        }
        /// <summary>
        /// Скорость
        /// </summary>
        /// <value>
        /// Скорость.
        /// </value>
        public override float Speed { get => playerProperities.Speed * 1.5f; }
    }
}