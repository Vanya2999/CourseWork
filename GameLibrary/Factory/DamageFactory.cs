using EngineLibrary;
using GameLibrary.Scripts;
using OpenTK;

namespace GameLibrary.Factory
{
    /// <summary>
    /// Класс для создания скрипта урона.
    /// </summary>
    public class DamageFactory
    {
        /// <summary>
        /// Создание скрипта урона.
        /// </summary>
        /// <param name="player">Игровой объект игрока</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateDamage(GameObject gameObj, string tag = null, float power = 1)
        {
            GameObject gameObject = new GameObject();
            gameObject.SetComponent(new ComponentTransform(gameObj.Transform.ObjectPosition, new Vector2(1f, 1f)));
            gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(0.8f, 0.8f)));
            gameObject.GameObjectTag = "Effect";
            DamageScript damageScript = new DamageScript();
            damageScript.Start();
            damageScript.ActivateDamage(gameObj, tag, power);

            gameObject.SetComponent(damageScript);

            return gameObject;
        }
    }
}