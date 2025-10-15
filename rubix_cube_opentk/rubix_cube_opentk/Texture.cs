using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace rubix_cube_opentk
{
    internal class Texture
    {
        //creating ID for the texture
        int ID;

        //creating texture
        public Texture(String filepath) 
        {

            ID = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            //loading and fliping image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult Tex = ImageResult.FromStream(File.OpenRead("../../../Textures/" + filepath), ColorComponents.RedGreenBlueAlpha);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Tex.Width, Tex.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Tex.Data);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        //Binding Image
        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        //Deleting Image
        public void Delete()
        {
            GL.DeleteTexture(ID);
        }
    }
}
