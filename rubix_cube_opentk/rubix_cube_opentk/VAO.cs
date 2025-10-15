using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;


namespace rubix_cube_opentk
{
    internal class VAO
    {
        //creating ID for VAo
        int ID;

        //creating VAO
        public VAO()
        {
            ID = GL.GenVertexArray();
        }

        //Linking VBO to VAO
        public void LinkToVAO(int location, int size, VBO vbo)
        {
            GL.BindVertexArray(ID);
            GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(location);
            vbo.UnBind();
        }

        //Binding VAO
        public void Bind()
        {
            GL.BindVertexArray(ID);
        }

        //Unbinding VAO
        public void UnBind()
        {
            GL.BindVertexArray(0);
        }

        //Deleting VAO
        public void Delete()
        {
            GL.DeleteVertexArray(ID);
        }

    }
}
