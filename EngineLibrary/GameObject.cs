namespace EngineLibrary
{
    /// <summary>
    /// Базовый класс.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Свойство, хранящее текстуру объекта
        /// </summary>
        public virtual TexturesBox Texture { get; protected set; }

        /// <summary>
        /// Класс, хранящее позицию объекта
        /// </summary>
        public virtual ComponentTransform Transform { get; protected set; }

        /// <summary>
        /// Сценарий выполения
        /// </summary>
        public virtual ExecutionScript Script { get; protected set; }

        public virtual CheckCollision Collider { get; protected set; }

        /// <summary>
        /// Тэг игрового объекта
        /// </summary>
        public string GameObjectTag { get; set; }

        /// <summary>
        /// Активность игрового объекта
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Конструктор игрового объекта.
        /// </summary>
        public GameObject()
        {
        }
        /// <summary>
        /// Установка компонентов.
        /// </summary>
        /// <param name="component">The component.</param>
        public virtual void SetComponent(object component)
        {
            switch (component)
            {
                case TexturesBox textureBox:
                    Texture = textureBox;
                    break;

                case ExecutionScript objectScript:
                    Script = objectScript;
                    break;

                case CheckCollision systemCollider:
                    Collider = systemCollider;
                    break;

                case ComponentTransform transform:
                    Transform = transform;
                    break;
            }
        }
        /// <summary>
        /// Обновление компонентов.
        /// </summary>
        public void Update()
        {
            if (Collider != null)
                Collider.IsInactive = !IsActive;

            if (Script != null)
                Script.Update(this);
        }
    }
}