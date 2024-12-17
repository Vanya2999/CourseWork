namespace GameLibrary.Maze
{
    /// <summary>
    /// Статический класс событий игры
    /// </summary>
    public static class GameEvents
    {
        /// <summary>
        /// Делегат события изменения количества здоровья
        /// </summary>
        /// <param name="tagPlayer">Тег игрового объекта игрока</param>
        /// <param name="value">Значение здоровья</param>
        public delegate void HealthDelegate(string tagPlayer, int value);

        /// <summary>
        /// Событие изменения количества здоровья
        /// </summary>
        public static HealthDelegate ChangeHealth { get; set; }

        /// <summary>
        /// Делегат события изменения количества очков
        /// </summary>
        /// <param name="tagPlayer">Тег игрового объекта игрока</param>
        /// <param name="value">Значение собранных очков</param>
        public delegate void PoinsDelegate(string tagPlayer, int value);

        /// <summary>
        /// Событие изменения количества очков
        /// </summary>
        ///
        public static PoinsDelegate ChangePoins { get; set; }

        /// <summary>
        /// Делегат события окончания игры
        /// </summary>
        public delegate void EndGameDelegate(string winPlayer);

        /// <summary>
        /// Событие окончания игры
        /// </summary>
        public static EndGameDelegate EndGame { get; set; }

        /// <summary>
        /// Делегат события получения пули игроком
        /// </summary>
        /// <param name="tagPlayer">Тег игрового объекта игрока</param>
        public delegate void CountBullets(string tagPlayer, int count);

        /// <summary>
        /// Событие получения пуль игроком
        /// </summary>
        public static CountBullets ChangeCount { get; set; }
    }
}