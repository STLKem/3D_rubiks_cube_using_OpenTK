using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace rubix_cube_opentk
{
    internal class Chunk
    {
        //making some variables
        public List<Vector2> uv;
        public List<Vector3> vertices;
        public List<uint> indices;
        public uint FaceNum = 0;

        //making "sides" each number means the id of the "cube" starting from 0 and not including the midle "cube" becouse we dont make it
        public int[] bottom_side = new int[] { 0, 1, 2, 11, 19, 18, 17, 9};
        public int[] back_side = new int[] { 17, 20, 23, 14, 6, 3, 0, 9};
        public int[] right_side = new int[] { 19, 22, 25, 24, 23, 20, 17, 18 };
        public int[] front_side = new int[] { 2, 5, 8, 16, 25, 22, 19, 11 };
        public int[] left_side = new int[] { 0, 3, 6, 7, 8, 5, 2, 1 };
        public int[] top_side = new int[] { 8, 7, 6, 14, 23, 24, 25, 16 };

        
        //making some vao, vbo, ebo and so on
        VAO vao;
        VBO vbo = new VBO();
        VBO vboTex = new VBO();
        EBO ebo;
        Texture texture;
        Shader shader;

        public Chunk()
        {
            //making shader and more variables
            shader = new Shader("Default.vert", "Default.frag");
            uv = new List<Vector2>();
            vertices = new List<Vector3>();
            indices = new List<uint>();

            //generating "cube"
            GenSmt();

            //creating vbo, vao, ebo, texture and so on 
            DoChunk();
        }

        public void GenSmt()
        {
            //generating 3 "mini-cubes" for x, y and z
            for (int x = 0; x < 3; x++) 
            {
                for (int y = 0; y < 3; y++)
                {
                    for(int z = 0; z < 3; z++)
                    {

                        //creating block for vertices cordinates and uv's
                        Block block = new Block((x, y, z));

                        //adding vertices and uv's for "mini-block" depending on the position
                        if(x == 0)
                        {
                            AddSmt(block, BlockData.Faces.Left);
                            AddSmt(block, BlockData.Faces.Right, 1); 
                        }
                        if (y == 0)
                        {
                            AddSmt(block, BlockData.Faces.Bottom);
                            AddSmt(block, BlockData.Faces.Top, 1);
                        }
                        if(z == 0)
                        {
                            AddSmt(block, BlockData.Faces.Back);
                            AddSmt(block, BlockData.Faces.Front, 1);
                        }
                        //we are not making the center "mini-cube"
                        if (x == 1)
                        {
                            if(y != 1 || z != 1)
                            {
                                AddSmt(block, BlockData.Faces.Left, 1);
                                AddSmt(block, BlockData.Faces.Right, 1);
                            }
                        }
                        if (y == 1)
                        {
                            if (x != 1 || z != 1)
                            {
                                AddSmt(block, BlockData.Faces.Top, 1);
                                AddSmt(block, BlockData.Faces.Bottom, 1);
                            }
                        }
                        if (z == 1)
                        {
                            if (x != 1 || y != 1)
                            {
                                AddSmt(block, BlockData.Faces.Back, 1);
                                AddSmt(block, BlockData.Faces.Front, 1);
                            }
                        }
                        if (x == 2)
                        {
                            AddSmt(block, BlockData.Faces.Left, 1);
                            AddSmt(block, BlockData.Faces.Right);
                        }
                        if (y == 2)
                        {
                            AddSmt(block, BlockData.Faces.Bottom, 1);
                            AddSmt(block, BlockData.Faces.Top);
                        }
                        if (z == 2)
                        {
                            AddSmt(block, BlockData.Faces.Back, 1);
                            AddSmt(block, BlockData.Faces.Front);
                        }


                    }

                }
            }
            


        }

        //making camera, idk wtf is going on here
        public void MakeMatrix(Camera camera)
        {
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();


            int modelLocation = GL.GetUniformLocation(shader.ID, "model");
            int viewLocation = GL.GetUniformLocation(shader.ID, "view");
            int projectionLocation = GL.GetUniformLocation(shader.ID, "projection");


            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);
        }

        //adding some indices for EBO just to make cubes from triangles
        public void AddIndeces()
        {
            indices.Add(0 + FaceNum);
            indices.Add(1 + FaceNum);
            indices.Add(2 + FaceNum);
            indices.Add(2 + FaceNum);
            indices.Add(3 + FaceNum);
            indices.Add(0 + FaceNum);

            FaceNum += 4;

        }

        //adding vertices and uv's to tha vectors
        public void AddSmt(Block block, BlockData.Faces face, int i = 0)
        {
            if(i == 1)
            {
                uv.AddRange(block.ret(BlockData.Faces.Empty).UV);
                vertices.AddRange(block.ret(face).Vertices);
                AddIndeces();
            }
            else
            {
                uv.AddRange(block.ret(face).UV);
                vertices.AddRange(block.ret(face).Vertices);
                AddIndeces();
            }
            
        }

        //creating and bounding vbo's, vao's, ebo's and creating texture
        public void DoChunk()
        {
            vao = new VAO();
            vbo.Make(vertices);
            vao.LinkToVAO(0, 3, vbo);
            vboTex.Make(uv);
            vao.LinkToVAO(1, 2, vboTex);
            vao.UnBind();

            ebo = new EBO(indices);

            texture = new Texture("Cubix_Sprite.png");
        }

        //rotating "cube" every frame
        public void RotateLeft(float rot)
        {
            //creating rotation matrix fro rotation
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(rot));

            //rotating each vertices one by one
            foreach(int i in left_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    //moving vertices to the point of rotation
                    vertices[o] -= new Vector3(0, 1, 1);
                    //rotation itself
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    //moving back from the point of rotation
                    vertices[o] += new Vector3(0, 1, 1);
                }
            }

            //rotating center "mini-cube"
            for (int o = 4 * 24; o < 4 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(0, 1, 1);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(0, 1, 1);
            }

            //updating vbo for displaying "cube" every frame
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RotateTop(float rot)
        {
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(-rot));

            foreach (int i in top_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    vertices[o] -= new Vector3(1, 2, 1);
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    vertices[o] += new Vector3(1, 2, 1);
                }
            }

            for (int o = 15 * 24; o < 15 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(1, 2, 1);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(1, 2, 1);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RotateFront(float rot)
        {
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(-rot));

            foreach (int i in front_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    vertices[o] -= new Vector3(1, 1, 2);
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    vertices[o] += new Vector3(1, 1, 2);
                }
            }

            for (int o = 13 * 24; o < 13 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(1, 1, 2);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(1, 1, 2);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RotateRight(float rot)
        {
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(-rot));

            foreach (int i in right_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    vertices[o] -= new Vector3(2, 1, 1);
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    vertices[o] += new Vector3(2, 1, 1);
                }
            }

            for (int o = 21 * 24; o < 21 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(2, 1, 1);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(2, 1, 1);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RotateBack(float rot)
        {
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(rot));

            foreach (int i in back_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    vertices[o] -= new Vector3(1, 1, 0);
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    vertices[o] += new Vector3(1, 1, 0);
                }
            }

            for (int o = 12 * 24; o < 12 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(1, 1, 0);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(1, 1, 0);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RotateBottom(float rot)
        {
            Matrix4 rotationMatrix = Matrix4.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(rot));

            foreach (int i in bottom_side)
            {
                for (int o = i * 24; o < i * 24 + 24; o++)
                {
                    vertices[o] -= new Vector3(1, 0, 1);
                    vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                    vertices[o] += new Vector3(1, 0, 1);
                }
            }

            for (int o = 10 * 24; o < 10 * 24 + 24; o++)
            {
                vertices[o] -= new Vector3(1, 0, 1);
                vertices[o] = Vector3.TransformPosition(vertices[o], rotationMatrix);
                vertices[o] += new Vector3(1, 0, 1);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo.ID);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Count * Vector3.SizeInBytes, vertices.ToArray());
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        //deleting chunck
        public void Delete()
        {
            shader.Delete();
            vao.Delete();
            vbo.Delete();
            vboTex.Delete();
            texture.Delete();
            ebo.Delete();
        }

        //rendering and drawing our chunck
        public void Render()
        {
            shader.Bind();
            vao.Bind();
            texture.Bind();
            ebo.Bind();

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

    }
}
