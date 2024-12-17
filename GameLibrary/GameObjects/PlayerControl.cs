using EngineLibrary;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.GameObjects
{
    /// <summary>
    /// Структура игрового управления персонажа
    /// </summary>
    public struct PlayerControl
    {
        /// <summary>
        /// Горизонтальная ось передвижения
        /// </summary>
        public DirectionAxes HorizontalAxis { get; private set; }

        /// <summary>
        /// Вертикальная ось передвижения
        /// </summary>
        public DirectionAxes VerticalAxis { get; private set; }

        /// <summary>
        /// Кнопка стрельбы
        /// </summary>
        public Key ShootKey { get; private set; }

        /// <summary>
        /// Кнопка стрельбы
        /// </summary>
        public Key GetKey { get; private set; }

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="horizontalAxis">Горизонтальная ось передвижения</param>
        /// <param name="verticalAxis"> Вертикальная ось передвижения</param>
        /// <param name="shootKey">Кнопка стрельбы</param>
        public PlayerControl(DirectionAxes horizontalAxis, DirectionAxes verticalAxis, Key shootKey, Key getKey)
        {
            HorizontalAxis = horizontalAxis;
            VerticalAxis = verticalAxis;
            ShootKey = shootKey;
            GetKey = getKey;
        }
    }
}
