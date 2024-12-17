using EngineLibrary;
using GameLibrary.GameObjects;
using MazeForm.player;

namespace GameLibrary.Scripts
{
    /// <summary>
    /// Описание скрипта урона.
    /// </summary>
    /// <seealso cref="EngineLibrary.ExecutionScript" />
    public class DamageScript : ExecutionScript
    {
        /// <summary>
        /// Экземпляр сцены игры
        /// </summary>
        private Game game;

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start(GameObject gameObject = null)
        {
            game = Game.instance;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update(GameObject gameObject) { }

        /// <summary>
        /// Активация эффекта
        /// </summary>
        /// <param name="player">Игровой объект, на который будет наложен эффект</param>
        public void ActivateDamage(GameObject playerGameObject, string tag = null, float power = 1)
        {
            playerGameObject.IsActive = true;

            if (playerGameObject.Script is GamePlayer)
            {
                if ((playerGameObject.Script as GamePlayer).Characteristic.Health > 0)
                {
                    (playerGameObject.Script as GamePlayer).ChangeStatsCharacteristic(playerGameObject, -power);
                }

                if ((playerGameObject.Script as GamePlayer).Characteristic.Health <= 0)
                {
                    playerGameObject.IsActive = false;

                    if (tag != null)
                    {
                        GamePlayer.SetPoins(tag, 10);

                        if (tag == "PlayerOne")
                            GamePlayer.SetPoins("PlayerTwo", -10);
                        else
                            GamePlayer.SetPoins("PlayerOne", -10);
                    }

                    if (playerGameObject.GameObjectTag == "PlayerOne" && GamePlayer.FirstPlayerPoints != 1)
                        (playerGameObject.Script as GamePlayer).ChangeStatsCharacteristic((-GamePlayer.FirstPlayerPoints / 2), "Death", playerGameObject.GameObjectTag);
                    else if (playerGameObject.GameObjectTag == "PlayerTwo" && GamePlayer.SecondPlayerPoints != 1)
                        (playerGameObject.Script as GamePlayer).ChangeStatsCharacteristic((-GamePlayer.SecondPlayerPoints / 2), "Death", playerGameObject.GameObjectTag);
                    else
                        (playerGameObject.Script as GamePlayer).ChangeStatsCharacteristic(-power, "Death", playerGameObject.GameObjectTag);
                    //GameEvents.ChangeEffect?.Invoke(playerGameObject.GameObjectTag, "Death");

                    DeactivateDamage(playerGameObject);
                }
            }

            if (playerGameObject.Script is Monster)
            {
                if ((playerGameObject.Script as Monster).Health > 0)
                {
                    (playerGameObject.Script as Monster).ChangeStatsValue(playerGameObject, -power);
                    //playerGameObject.Texture.Texture = null;
                }

                if ((playerGameObject.Script as Monster).Health <= 0)
                {
                    playerGameObject.IsActive = false;
                    playerGameObject.Collider.IsInactive = true;

                    if (tag != null)
                    {
                        GamePlayer.SetPoins(tag, 10);
                    }

                    DeactivateDamage(playerGameObject);
                }
            }
        }

        /// <summary>
        /// Деактивация скрипта урона
        /// </summary>
        protected void DeactivateDamage(GameObject playerGameObject)
        {
            if (!playerGameObject.IsActive)
            {
                if (playerGameObject.GameObjectTag == "PlayerTwo")
                {
                    playerGameObject.Transform.ObjectPosition = game.PlayerOneFactory.StartPosition;
                    (playerGameObject.Script as GamePlayer).Characteristic.SetCharacteristic(CharactersticType.Health, 10);
                }
                else if (playerGameObject.GameObjectTag == "PlayerOne")
                {
                    playerGameObject.Transform.ObjectPosition = game.PlayerTwoFactory.StartPosition;
                    (playerGameObject.Script as GamePlayer).Characteristic.SetCharacteristic(CharactersticType.Health, 10);
                }

                playerGameObject.IsActive = true;
            }
        }
    }
}