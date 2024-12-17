using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EngineLibrary
{
    /// <summary>
    /// Инициализация текстуры
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Загрузка текстуры.
        /// </summary>
        /// <param name="path">Путь к текстуре.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">File not found at `Resources\" + path + "`</exception>
        public static TextureStorage Load(string path)
        {
            if (!File.Exists(@"Resources\" + path))
                throw new FileNotFoundException(@"File not found at `Resources\" + path + "`");

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(@"Resources\" + path);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            int width = bmp.Width;
            int height = bmp.Height;

            bmp.Dispose();

            return new TextureStorage(id, width, height);
        }
        /// <summary>
        /// Удаление текстуры.
        /// </summary>
        /// <param name="texturesId">The textures identifier.</param>
        public static void Delete(List<int> texturesId)
        {
            foreach (var id in texturesId)
                GL.DeleteTexture(id);
        }
    }
}