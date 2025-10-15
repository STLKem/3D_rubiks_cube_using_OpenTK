using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace rubix_cube_opentk
{
    internal class EBO
    {
        //making an ID for EBO witch we will refer to in order to identify exact EBO we are using
        int ID;

        //creating EBO
        public EBO(List<uint> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        //Binding EBO
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        //Deleting EBO
        public void Delete()
        {
            GL.DeleteBuffer(ID);
        }

    }
}
