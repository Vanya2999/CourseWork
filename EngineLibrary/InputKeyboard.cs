using OpenTK.Input;

namespace EngineLibrary
{
    /// <summary>
    /// Класс, позволяющий управлять вводом с клавиатуры
    /// </summary>
    public static class InputKeyboard
    {
        /// <summary>
        /// Метод, возращающий значение ввода основных осей направления
        /// </summary>
        /// <param name="axis">Ось направления</param>
        /// <returns>Положительное или отрицательное значение оси</returns>
        public static int GetAxisDirection(DirectionAxes axis)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            int moveObject = 0;

            switch (axis)
            {
                case DirectionAxes.HorizontalAxis:
                    if (keyboardState.IsKeyDown(Key.D)) moveObject++;
                    if (keyboardState.IsKeyDown(Key.A)) moveObject--;
                    break;

                case DirectionAxes.VerticalAxis:
                    if (keyboardState.IsKeyDown(Key.W)) moveObject--;
                    if (keyboardState.IsKeyDown(Key.S)) moveObject++;
                    break;

                case DirectionAxes.AlternativeHorizontalAxis:
                    if (keyboardState.IsKeyDown(Key.Right)) moveObject++;
                    if (keyboardState.IsKeyDown(Key.Left)) moveObject--;
                    break;

                case DirectionAxes.AlternativeVerticalAxis:
                    if (keyboardState.IsKeyDown(Key.Up)) moveObject--;
                    if (keyboardState.IsKeyDown(Key.Down)) moveObject++;
                    break;
            }

            return moveObject;
        }

        /// <summary>
        /// Метод, возращающий реакцию на нажатие клавиши ввода
        /// </summary>
        /// <param name="key">Клавиша ввода</param>
        /// <returns>Реакция true или false</returns>
        public static bool IsButtonDawn(Key key)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            return keyboardState.IsKeyDown(key);
        }
    }
}