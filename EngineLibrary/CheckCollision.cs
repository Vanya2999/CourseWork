using OpenTK;
using System.Collections.Generic;

namespace EngineLibrary
{
    /// <summary>
    /// Класс компонента, описывающий твердое тело
    /// </summary>
    public class CheckCollision
    {
        private static List<GameObject> collidersOfGameObjects;

        private readonly Vector2[] boundCorners;

        private readonly GameObject gameObject;

        private Vector2 _colliderScale;

        private Vector2 _offsetCollider;

        /// <summary>
        /// Неактивность элемента
        /// </summary>
        public bool IsInactive { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="gameObject">Игровой объект, которому принадлежит компонент</param>
        /// <param name="scale">Размер коллайдера</param>
        /// <param name="offset">Cмещения коллайдера от центра</param>
        public CheckCollision(GameObject gameObject, Vector2 scale, Vector2 offset = new Vector2())
        {
            if (collidersOfGameObjects == null)
                collidersOfGameObjects = new List<GameObject>();

            collidersOfGameObjects.Add(gameObject);
            this.gameObject = gameObject;

            boundCorners = new Vector2[4];
            _colliderScale = scale;
            _offsetCollider = offset;
            IsInactive = false;
        }

        /// <summary>
        /// Обновление границ твердого тела
        /// </summary>
        private void UpdateBorders()
        {
            Vector2 position = gameObject.Transform.ObjectPosition;

            float offsetWidth = _colliderScale.X;
            float offsetHeight = _colliderScale.Y;

            boundCorners[0] = new Vector2(position.X + _offsetCollider.X, position.Y + _offsetCollider.Y);
            boundCorners[1] = new Vector2(position.X + _offsetCollider.X, position.Y + _offsetCollider.Y + offsetHeight);
            boundCorners[2] = new Vector2(position.X + _offsetCollider.X + offsetWidth, position.Y + _offsetCollider.Y + offsetHeight);
            boundCorners[3] = new Vector2(position.X + _offsetCollider.X + offsetWidth, position.Y + _offsetCollider.Y);
        }

        /// <summary>
        /// Проверка на пересечние компонента твердого тела с другими компонентами твердого тела, имеющие тег у игрового объекта
        /// </summary>
        /// <param name="tagNames">Теги игровых объектов, с которыми ожидается столкновение</param>
        /// <returns>Реакция на проверку</returns>
        public bool IsCrossing(params string[] tagNames)
        {
            foreach (GameObject otherGameObject in collidersOfGameObjects)
            {
                if (otherGameObject == gameObject || otherGameObject.Collider.IsInactive) continue;

                bool hasTag = false;

                for (int i = 0; i < tagNames.Length && !hasTag; i++)
                {
                    hasTag = otherGameObject.GameObjectTag == tagNames[i];
                }

                if (hasTag)
                {
                    if (IsObjectCrossing(otherGameObject))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка на пересечние компонента твердого тела с другими компонентами твердого тела, имеющие тег у игрового объекта
        /// </summary>
        /// <param name="crossedObject">Пересаемый объект</param>
        /// <param name="tags">Теги игровых объектов, с которыми ожидается столкновение</param>
        /// <returns>Реакция на проверку</returns>
        public bool IsCrossing(out GameObject crossedObject, params string[] tags)
        {
            foreach (GameObject anotherObject in collidersOfGameObjects)
            {
                if (anotherObject == gameObject || anotherObject.Collider.IsInactive) continue;

                bool hasTag = false;

                for (int i = 0; i < tags.Length && !hasTag; i++)
                {
                    hasTag = anotherObject.GameObjectTag == tags[i];
                }

                if (hasTag)
                {
                    if (anotherObject.GameObjectTag != null && IsObjectCrossing(anotherObject))
                    {
                        crossedObject = anotherObject;
                        return true;
                    }
                }
            }

            crossedObject = null;
            return false;
        }

        /// <summary>
        /// Проверка на пересечение компонента твердого тела с другими компонентами твердого тела, имеющие конкретный сценарий выполнения T
        /// </summary>
        /// <typeparam name="T">Конкретный сценарий выполнения</typeparam>
        /// <param name="objectScript">Сценарий выполнения игрового объекта, с которым столкнулся</param>
        /// <returns>Реакция на столкновение</returns>
        public bool IsCrossing<T>(out T objectScript)
            where T : ExecutionScript
        {
            foreach (GameObject otherGameObject in collidersOfGameObjects)
            {
                if (otherGameObject == gameObject || otherGameObject.Script == null || otherGameObject.Collider.IsInactive) continue;

                if (otherGameObject.Script is T)
                {
                    if (IsObjectCrossing(otherGameObject))
                    {
                        objectScript = (T)otherGameObject.Script;
                        return true;
                    }
                }
            }

            objectScript = null;
            return false;
        }

        /// <summary>
        /// Проверка на пересечение компонента твердого тела с другим компонентам твердого тела
        /// </summary>
        /// <param name="otherGameObject">Игровой объект с компонетом твердого тела</param>
        /// <returns>Реакция на столкновение</returns>
        private bool IsObjectCrossing(GameObject otherGameObject)
        {
            UpdateBorders();
            otherGameObject.Collider.UpdateBorders();

            CheckCollision collider = otherGameObject.Collider;

            int count = boundCorners.Length + collider.boundCorners.Length;

            Vector2[] allCorners = new Vector2[count];
            boundCorners.CopyTo(allCorners, 0);
            collider.boundCorners.CopyTo(allCorners, boundCorners.Length);

            Vector2 normal;

            bool isInteresect = false;

            for (int i = 0; i < count && !isInteresect; i++)
            {
                normal = CreateNormal(allCorners, i);

                Vector2 currentProjection = CreateProjection(normal);
                Vector2 otherProjection = collider.CreateProjection(normal);

                if (currentProjection.X < otherProjection.Y || otherProjection.X < currentProjection.Y)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Создание нормали
        /// </summary>
        /// <param name="corners">Углы двух компонетов</param>
        /// <param name="index">Номер угла</param>
        /// <returns>Нормаль</returns>
        private Vector2 CreateNormal(Vector2[] componentAngles, int angleIndex)
        {
            int next = angleIndex + 1;
            next = next == componentAngles.Length ? 0 : next;

            Vector2 pointFirst = componentAngles[angleIndex];
            Vector2 pointSecond = componentAngles[next];

            Vector2 edge = new Vector2(pointSecond.X - pointFirst.X, pointSecond.Y - pointFirst.Y);

            return new Vector2(-edge.Y, edge.X);
        }

        /// <summary>
        /// Создание проекции
        /// </summary>
        /// <param name="normal">Нормаль</param>
        /// <returns>Проекцию</returns>
        private Vector2 CreateProjection(Vector2 normal)
        {
            Vector2 result = new Vector2();
            bool isNull = true;

            foreach (Vector2 current in boundCorners)
            {
                float projection = normal.X * current.X + normal.Y * current.Y;

                if (isNull)
                {
                    result = new Vector2(projection, projection);
                    isNull = false;
                }

                if (projection > result.X)
                    result.X = projection;
                if (projection < result.Y)
                    result.Y = projection;
            }

            return result;
        }
    }
}