using GameLibrary.GameObjects;

namespace GameLibrary
{
    /// <summary>
    /// Декоратор времени перезарядки
    /// </summary>
    /// <seealso cref="GameLibrary.CharactersticDecorator" />
    public class CooldownTimeDecorator : CharactersticDecorator
    {
        /// <summary>
        /// Констурктор <see cref="CooldownTimeDecorator"/> класса.
        /// </summary>
        /// <param name="playerProperities"></param>
        public CooldownTimeDecorator(PlayerCharacteristic playerProperities) : base(playerProperities)
        {
        }

        /// <summary>
        /// Время перезарядки оружия
        /// </summary>
        public override float CooldownTime { get => playerProperities.CooldownTime / 2; }
    }
}