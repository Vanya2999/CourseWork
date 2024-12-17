﻿using EngineLibrary;
using GameLibrary.GameObjects;
using GameLibrary.Prize;
using OpenTK;

namespace GameLibrary.Factory
{
    /// <summary>
    /// Класс для  создания приза амуниции.
    /// </summary>
    /// <seealso cref="GameLibrary.Factory.PrizeFactory" />
    public class AmmunitionPrizeFactory : PrizeFactory
    {
        /// <summary>
        /// Создание приза амуниции.
        /// </summary>
        /// <param name="position">Позиция появления</param>
        /// <returns>Игровой объект</returns>
        public override GameObject CreatePrize(Vector2 position)
        {
            GameObject gameObject = new GameObject();
            gameObject.SetComponent(new ComponentTransform(position, new Vector2(1f, 1f)));
            gameObject.SetComponent(new TexturesBox(Content.Load("MazeElements/Prize/ammo.png")));
            gameObject.SetComponent(new CheckCollision(gameObject, new Vector2(gameObject.Texture.Texture.Width - 20, gameObject.Texture.Texture.Height - 10), new Vector2(10, 5)));
            gameObject.GameObjectTag = "Spawn";

            PrizeSpawn speedPrize = new PrizeSpawn();
            speedPrize.Initializer(CharactersticType.Ammunition, 10, 5f);

            speedPrize.Start(gameObject);

            gameObject.SetComponent(speedPrize);

            return gameObject;
        }
    }
}