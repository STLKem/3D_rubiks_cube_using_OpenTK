using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace rubix_cube_opentk
{
    internal class Shader
    {
        //creating ID for Shader
        public int ID;

        //creating Shader itself
        public Shader(string vertexshaderFilepath, string fragmentshaderFilepath) 
        {
            ID = GL.CreateProgram();

            //creating vertex shader Default.vert
            int vertexshader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexshader, LoadShaderSource(vertexshaderFilepath));
            GL.CompileShader(vertexshader);

            //creating fragment shader Default.frag
            int fragmentshader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentshader, LoadShaderSource(fragmentshaderFilepath));
            GL.CompileShader(fragmentshader);

            //attaching shaders
            GL.AttachShader(ID, vertexshader);
            GL.AttachShader(ID, fragmentshader);

            //Linking Shader
            GL.LinkProgram(ID);

            //Deleting vertex/fragment shaders
            GL.DeleteShader(vertexshader);
            GL.DeleteShader(fragmentshader);
        }

        //Binding Shader
        public void Bind()
        {
            GL.UseProgram(ID);
        }

        //Deleting Shader
        public void Delete()
        {
            GL.DeleteProgram(ID);
        }

        //Geting File Data From File Path
        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader read = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = read.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("АНЛАК: " + ex.Message);
            }

            return shaderSource;

        }


    }
}
