using GameLibrary.GameObjects;

namespace GameLibrary
{
    /// <summary>
    /// Декоратор силы.
    /// </summary>
    /// <seealso cref="GameLibrary.CharactersticDecorator" />
    public class PowerDecorator : CharactersticDecorator
    {
        /// <summary>
        /// Инициализация <see cref="PowerDecorator"/> класса.
        /// </summary>
        /// <param name="playerProperities">The player properities.</param>
        public PowerDecorator(PlayerCharacteristic playerProperities) : base(playerProperities)
        {
        }
        /// <summary>
        /// Сила
        /// </summary>
        /// <value>
        /// Сила.
        /// </value>
        public override float Power { get => playerProperities.Power * 2; }
    }
}