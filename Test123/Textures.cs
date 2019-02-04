using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Test123
{
    class Textures
    {
        public int[] textures { get; private set; }
        public string[] paths { get; private set; }
        public Textures(string[] texturePaths)
        {
            this.paths = texturePaths;
            this.textures = new int[this.paths.Length];
        }

        public void InitTextures()
        {
            for (int i = 0; i < this.paths.Length; i++)
            {
                textures[i] = LoadTexture(this.paths[i]);
            }
        }
        private int LoadTexture(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            System.Drawing.Imaging.BitmapData bitmap_data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, bitmap.Width, bitmap.Height, PixelFormat.Bgra, PixelType.UnsignedByte, bitmap_data.Scan0);

            bitmap.UnlockBits(bitmap_data);
            bitmap.Dispose();

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }
    }
}
