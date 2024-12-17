using System;
using System.Collections.Generic;

namespace EngineLibrary
{
    /// <summary>
    /// Контейнер для текстуры
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TexturesBox : IDisposable
    {
        /// <summary>
        /// Текущая текстура.
        /// </summary>
        /// <value>
        /// Текстура.
        /// </value>
        public TextureStorage Texture { get; private set; }

        /// <summary>
        /// Словарь для текстуры.
        /// </summary>
        private Dictionary<string, TextureStorage> TextureDictionary;

        /// <summary>
        /// Время, через которое можно изменить текстуру.
        /// </summary>
        private float _delta = 0;

        /// <summary>
        /// Таймер
        /// </summary>
        private float _currentTime = 0;

        /// <summary>
        /// Конструктор <see cref="TexturesBox"/> класса.
        /// </summary>
        /// <param name="texture">Текстура.</param>
        public TexturesBox(TextureStorage texture)
        {
            TextureDictionary = new Dictionary<string, TextureStorage>();

            TextureDictionary.Add("default", texture);

            Texture = texture;
        }

        /// <summary>
        /// Контруктор cref="TexturesBox"/> класса.
        /// </summary>
        public TexturesBox()
        {
            TextureDictionary = new Dictionary<string, TextureStorage>();
        }

        /// <summary>
        /// Добавление текстуры.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="texture2D">The texture2 d.</param>
        public void AddTexture(string name, TextureStorage texture2D)
        {
            TextureDictionary.Add(name, texture2D);
        }

        /// <summary>
        /// Удаление текстуры.
        /// </summary>
        /// <param name="name">The name.</param>
        public void DeleteTexture(string name)
        {
            TextureDictionary.Remove(name);
        }

        /// <summary>
        /// Редактирование текстуры.
        /// </summary>
        /// <param name="texture2D">The texture2 d.</param>
        /// <param name="name">Имя.</param>
        public void EdiTexturet(TextureStorage texture2D, string name)
        {
            TextureDictionary[name] = texture2D;
        }

        /// <summary>
        /// Установка выводимой текстуры.
        /// </summary>
        /// <param name="name">Имя.</param>
        public void SetTexture(string name)
        {
            if (GameTime.CurrentLaunchTime - _currentTime >= _delta)
            {
                Texture = TextureDictionary[name];

                _currentTime = GameTime.CurrentLaunchTime;

                _delta = 0;
            }
        }

        /// <summary>
        /// Установка выводимой текстуры на заданное время.
        /// </summary>
        /// <param name="name">Имя текстуры.</param>
        /// <param name="delta">Время изменения текстуры.</param>
        public void SetTexture(string name, float delta)
        {
            if (GameTime.CurrentLaunchTime - _currentTime >= this._delta)
            {
                Texture = TextureDictionary[name];

                this._delta = delta;
                _currentTime = GameTime.CurrentLaunchTime;
            }
        }

        /// <summary>
        /// Получение айдишников текстур.
        /// </summary>
        /// <returns></returns>
        public List<int> GetTextureId()
        {
            List<int> result = new List<int>(10);

            foreach (var textur in TextureDictionary)
                result.Add(textur.Value.ID);

            return result;
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            Content.Delete(GetTextureId());
            TextureDictionary.Clear();
        }
    }
}