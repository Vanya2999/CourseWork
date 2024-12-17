using EngineLibrary;
using OpenTK;

namespace GameLibrary.Factory
{
    public abstract class PrizeFactory
    {
        /// <summary>
        /// Фабрика создания призов.
        /// </summary>
        /// <param name="position">Позиция появления</param>
        /// <returns>Игровой объект</returns>
        public abstract GameObject CreatePrize(Vector2 position);
    }
}