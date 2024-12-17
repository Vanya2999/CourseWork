using OpenTK;

namespace EngineLibrary
{
    /// <summary>
    /// Трансофрамация игрового объекта.
    /// </summary>
    public class ComponentTransform
    {
        /// <summary>
        /// Позиция игрового объекта
        /// </summary>
        public Vector2 ObjectPosition { get; set; }

        /// <summary>
        /// Размер игрового объекта
        /// </summary>
        public Vector2 ObjectSize { get; set; }

        private Vector2 _movementInCurrentFrame;

        /// <summary>
        /// Конструктор компонента
        /// </summary>
        /// <param name="startingPosition">начальная позиция</param>
        /// <param name="startingScale">Начальный размер</param>
        public ComponentTransform(Vector2 startingPosition, Vector2 startingScale)
        {
            ObjectPosition = startingPosition;
            ObjectSize = startingScale;
        }

        /// <summary>
        /// Перемещение объкта
        /// </summary>
        /// <param name="movemenVectort">Вектор перемещения</param>
        public void SetMovement(Vector2 movemenVectort)
        {
            _movementInCurrentFrame = movemenVectort;

            ObjectPosition += movemenVectort;
        }

        /// <summary>
        /// Возврат позиция в этом кадре
        /// </summary>
        public void ResetMovement()
        {
            ObjectPosition -= _movementInCurrentFrame;
        }
    }
}