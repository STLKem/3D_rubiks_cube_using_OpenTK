using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;


namespace rubix_cube_opentk
{
    internal class VBO
    {
        //creating ID for VBO
        public int ID;


        //creating 3d coords VBO
        public void Make(List<Vector3> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }

        //creating 2d texture VBO
        public void Make(List<Vector2> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }

        //Unbinding VBO (using in our VAO)
        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        //Deleting VBO
        public void Delete()
        {
            GL.DeleteBuffer(ID);
        }

    }
}
