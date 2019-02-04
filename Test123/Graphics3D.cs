using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Test123
{
    class Graphics3D
    {
        public Textures textures { get; private set; }
        public Graphics3D(string[] paths)
        {
            this.textures = new Textures(paths);
            this.textures.InitTextures();
        }
        public void DrawCube(double x, double y, double z, double size, double red, double green, double blue)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(red, green, blue);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z); //left bottom
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z); // right bottom
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z); // top right
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z); // top left

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);

            GL.End();
        }

        public void DrawCubeTextured(double x, double y, double z, double size, double red, double green, double blue)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(red, green, blue);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z); //left bottom
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z); // right bottom
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z); // top right
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z); // top left

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 2) + z);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 2) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 2) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 2) + z);

            GL.End();
        }
        public void DrawFlatTextured(double x, double y, double z, double size, double red, double green, double blue, double theta)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color4(red, green, blue, theta);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 8) + z); //left bottom
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 8) + z); // right bottom
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 8) + z); // top right
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 8) + z); // top left

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 8) + z);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 8) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 8) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 8) + z);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 8) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 8) + z);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 8) + z);
            GL.TexCoord2(1, 1);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 8) + z);
            GL.TexCoord2(1, 0);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 8) + z);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 8) + z);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, (size / 2) + y, -(size / 8) + z);
            GL.Vertex3(-(size / 2) + x, (size / 2) + y, -(size / 8) + z);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, (size / 8) + z);
            GL.Vertex3((size / 2) + x, -(size / 2) + y, -(size / 8) + z);
            GL.Vertex3(-(size / 2) + x, -(size / 2) + y, -(size / 8) + z);

            GL.End();
        }

        public void DrawMenuItem(double x, double y, double z, double sizex, double sizey, double sizez, int textureId, bool selected)
        {
            double[] colors = new double[3];
            if(selected)
            {
                colors[0] = 0.1;
                colors[1] = 0.9;
                colors[2] = 0.1;
            }
            else
            {
                colors[0] = 0.3;
                colors[1] = 0.3;
                colors[2] = 0.15;
            }

            this.DrawRectangle(x, y, z, sizex + 1.0, sizey + 1.0, sizez - 0.5, colors[0], colors[1], colors[2], 1.0);

            GL.BindTexture(TextureTarget.Texture2D, this.textures.textures[textureId]);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(0.9, 0.9, 0.9);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z); //left bottom
            GL.TexCoord2(1, 1);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z); // right bottom
            GL.TexCoord2(1, 0);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z); // top right
            GL.TexCoord2(0, 0);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z); // top left

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);

            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void DrawRectangle(double x, double y, double z, double sizex, double sizey, double sizez, double red, double green, double blue, double alpha)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color4(red, green, blue, alpha);

            //front
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.TexCoord2(1, 1); //left bottom
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.TexCoord2(1, 0); // right bottom
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.TexCoord2(0, 0); // top right
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z); // top left

            //left
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //right
            GL.Normal3(1.0, 0.0, 0.0);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //back
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //top
            GL.Normal3(0.0, 1.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, (sizey / 2) + y, -(sizez / 2) + z);

            //bottom
            GL.Normal3(0.0, -1.0, 0.0);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, (sizez / 2) + z);
            GL.Vertex3((sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);
            GL.Vertex3(-(sizex / 2) + x, -(sizey / 2) + y, -(sizez / 2) + z);

            GL.End();
        }

        public void DrawRectangleTextured(double x, double y, double z, double sizex, double sizey, double sizez, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            DrawRectangle(x, y, z, sizex, sizey, sizez, 1.0, 1.0, 1.0, 1.0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void DrawTexture(double x, double y, double z, double size, int texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, this.textures.textures[texture]);
            this.DrawCube(x, y, z, size, 0.9, 0.9, 0.9);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void DrawHUD(int mines, int cellsLeft, int flags, int seed, int timeElapsed, int width, int height)
        {
            double[] rows = new double[3] { -0.330, -0.410, -0.490 };

            GL.Rotate(0.0, 1.0, 1.0, 1.0);
            DrawRectangle(0.0, -0.8, -4.0, 2.8, 0.52, 0.1, 0.7, 0.7, 0.7, 0.7);

            DrawRectangleTextured(-0.575, rows[2], -2.0, 0.20, 0.05, 0.02, this.textures.textures[34]);
            DrawRectangleTextured(-0.575, rows[1], -2.0, 0.20, 0.05, 0.02, this.textures.textures[43]);

            DrawRectangleTextured(0.275, rows[2], -2.0, 0.15, 0.05, 0.02, this.textures.textures[41]);
            DrawRectangleTextured(0.275, rows[1], -2.0, 0.15, 0.05, 0.02, this.textures.textures[42]);
            
            DrawRectangleTextured(-0.600, rows[0], -2.0, 0.15, 0.05, 0.02, this.textures.textures[47]);
            DrawRectangleTextured(-0.15, rows[0], -2.0, 0.15, 0.05, 0.02, this.textures.textures[48]);
            DrawRectangleTextured(0.275, rows[0], -2.0, 0.15, 0.05, 0.02, this.textures.textures[49]);

            DrawWhiteNum(cellsLeft, -0.415, rows[1], -2.0, 0.065, false);
            DrawWhiteNum(flags, 0.41, rows[1], -2.0, 0.065, false);
            DrawWhiteNum(seed, 0.41, rows[2], -2.0, 0.065, false);
            DrawWhiteNum(timeElapsed, -0.41, rows[2], -2.0, 0.065, true);

            DrawWhiteNum(width, -0.465, rows[0], -2.0, 0.065, false);
            DrawWhiteNum(height, -0.015, rows[0], -2.0, 0.065, false);
            DrawWhiteNum(mines, 0.41, rows[0], -2.0, 0.065, false);
        }

        public void DrawWhiteNum(int num, double x, double y, double z, double size, bool seconds)
        {
            string strNum = num.ToString();
            int forLength = (seconds) ? strNum.Length + 1 : strNum.Length;
            for(int i = 0; i < forLength; i++)
            {
                if (i == strNum.Length)
                {
                    GL.BindTexture(TextureTarget.Texture2D, this.textures.textures[33]);
                    DrawFlatTextured(((i * size * 1.25) + x)+x/20, y+y/26, z, size/2, 1.0, 1.0, 1.0, 1.0);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                }
                else
                {
                    int curNum = (int)strNum[i] - 48;
                    GL.BindTexture(TextureTarget.Texture2D, this.textures.textures[curNum]);
                    DrawFlatTextured((i * size * 1.25) + x, y, z, size, 1.0, 1.0, 1.0, 1.0);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                }
            }
        }


        public void DrawNum(int num, double x, double y, double z, double size, double shade)
        {
            if(num > 0 && num < 9)
            {
                GL.BindTexture(TextureTarget.Texture2D, this.textures.textures[num+10]);
                DrawFlatTextured(x, y, z, size, shade, shade, shade, shade);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            else
            {
                this.DrawCube(x, y, z, size, 0.1, 0.9, 0.1);
            }
        }
    }
}
