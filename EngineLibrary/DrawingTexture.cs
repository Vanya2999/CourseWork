using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EngineLibrary
{
    /// <summary>
    /// Класс для отрисовки тексуры
    /// </summary>
    public class DrawingTexture
    {
        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-screenWidth / 2f, screenWidth / 2f, screenHeight / 2f, -screenHeight / 2f, 0f, 1f);
        }

        /// <summary>
        /// Метод для отрисовки текстуры
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Draw(GameObject gameObject)
        {
            var texture = gameObject.Texture.Texture;
            var size = gameObject.Transform.ObjectSize;
            var position = gameObject.Transform.ObjectPosition;

            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };

            GL.BindTexture(TextureTarget.Texture2D, gameObject.Texture.Texture.ID);
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);

                vertices[i].X *= texture.Width;
                vertices[i].Y *= texture.Height;
                vertices[i] *= size;
                vertices[i] += position;

                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }
    }
}