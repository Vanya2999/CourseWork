using System;
using System.Diagnostics;

namespace EngineLibrary
{
    /// <summary>
    /// Статический класс управления временем
    /// </summary>
    public static class GameTime
    {
        private readonly static Stopwatch _watch;

        private static long _previousTicks;

        /// <summary>
        /// Текущее время с запуска приложения
        /// </summary>
        public static float CurrentLaunchTime { get; private set; }

        /// <summary>
        /// Разница во времени между кадрами
        /// </summary>
        public static float DeltaTimeFrames { get; private set; }

        /// <summary>
        /// Конструктори статического класса
        /// </summary>
        static GameTime()
        {
            _watch = new Stopwatch();
            Reset();
        }

        /// <summary>
        /// Обновление подсчитанных значений
        /// </summary>
        public static void UpdateGameTime()
        {
            long ticks = _watch.Elapsed.Ticks;

            CurrentLaunchTime = (float)ticks / TimeSpan.TicksPerSecond;
            DeltaTimeFrames = (float)(ticks - _previousTicks) / TimeSpan.TicksPerSecond;

            _previousTicks = ticks;
        }

        /// <summary>
        /// Сброс таймера и счетчика
        /// </summary>
        public static void Reset()
        {
            _watch.Reset();
            _watch.Start();
            _previousTicks = _watch.Elapsed.Ticks;
        }
    }
}