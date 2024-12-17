namespace EngineLibrary
{
    /// <summary>
    /// Класс для хранения текстуры
    /// </summary>
    public class TextureStorage
    {
        private int _textureId;
        private int _textureWidth, _textureHeight;

        /// <summary>
        /// Свойство, хранящее айдишник текстуры
        /// </summary>
        public int ID
        {
            get
            {
                return _textureId;
            }
        }

        /// <summary>
        /// Ширина текстуры
        /// </summary>
        public int Width
        {
            get 
            { 
                return _textureWidth;
            }
        }

        /// <summary>
        /// Высота текстуры
        /// </summary>
        public int Height
        {
            get
            { 
                return _textureHeight;
            }
        }

        /// <summary>
        /// Конструктор создания объекта класса
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TextureStorage(int id, int width, int height)
        {
            this._textureId = id;
            this._textureWidth = width;
            this._textureHeight = height;
        }
    }
}