using EngineLibrary;
using GameLibrary.GameObjects;

namespace GameLibrary.Prize
{
    /// <summary>
    /// Появление призов в лабиринте
    /// </summary>
    public class PrizeSpawn : ExecutionScript
    {
        private Game _game;
        private PlayerCharacteristic _dropOutPrize;
        private CharactersticType _typeProperty;
        private float _cuurentTimeToDisappear;
        private int _value;

        /// <summary>
        /// Инициализация приза
        /// </summary>
        /// <param name="disappearTime">Время исчезнование места</param>
        public void Initializer(PlayerCharacteristic playerProperities, float disappearTime)
        {
            _dropOutPrize = playerProperities;
            _cuurentTimeToDisappear = GameTime.CurrentLaunchTime + disappearTime;
        }

        /// <summary>
        /// Инициализация типа приза.
        /// </summary>
        /// <param name="typeProperty">Тип.</param>
        /// <param name="value">Значение.</param>
        /// <param name="disappearTime">Время деактивации приза.</param>
        public void Initializer(CharactersticType typeProperty, int value, float disappearTime)
        {
            this._typeProperty = typeProperty;
            this._value = value;
            _cuurentTimeToDisappear = GameTime.CurrentLaunchTime + disappearTime;
        }

        /// <summary>
        /// Поведение на момент создание игрового объекта
        /// </summary>
        public override void Start(GameObject gameObject = null)
        {
            _game = Game.instance;
        }

        /// <summary>
        /// Обновление игрового объекта
        /// </summary>
        public override void Update(GameObject gameObject)
        {
            if (_cuurentTimeToDisappear < GameTime.CurrentLaunchTime)
            {
                _game.AddObjectsToRemove(gameObject);
            }

            if (gameObject.Collider.IsCrossing(out GameObject player, "PlayerOne", "PlayerTwo"))
            {
                if (player.GameObjectTag == "PlayerOne" && InputKeyboard.IsButtonDawn((player.Script as GamePlayer).ControlPlayer.GetKey))
                {
                    if (_dropOutPrize == null)
                        (player.Script as GamePlayer).Characteristic.SetCharacteristic(_typeProperty, _value);
                    else
                        (player.Script as GamePlayer).SetCharacteristic(_dropOutPrize);

                    _game.AddObjectsToRemove(gameObject);
                }
                else if (player.GameObjectTag == "PlayerTwo" && InputKeyboard.IsButtonDawn((player.Script as GamePlayer).ControlPlayer.GetKey))
                {
                    if (_dropOutPrize == null)
                        (player.Script as GamePlayer).Characteristic.SetCharacteristic(_typeProperty, _value);
                    else
                        (player.Script as GamePlayer).SetCharacteristic(_dropOutPrize);

                    _game.AddObjectsToRemove(gameObject);
                }
            }
        }
    }
}