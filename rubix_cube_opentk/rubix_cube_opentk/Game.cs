using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
//using rubix_cube_opentk.Graphics;
//using rubix_cube_opentk.World;
using StbImageSharp;



namespace rubix_cube_opentk
{
    internal class Game : GameWindow
    {
        //seting Width and Height in a Program.cs
        int screenWidth;
        int screenHeight;

        //making our "cube" instance
        Chunk chunk;

        //making Rot per frame
        float Rot = 0.08f;
        //making number of rotations: 90/Rot to retate for 90 degrees
        int NumRot = 1125;// 90/Rot

        //making rotationSide to determin wich side to rotate and rotation counter to set rotationSide to 0 after Count hiting NumRot
        int rotationSide = 0;
        int rotationCount = 0;


        //creating camera to move and look around
        Camera camera;



        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            //creating window in the midle of the screen
            this.CenterWindow(new Vector2i(width, height));
            screenWidth = width;
            screenHeight = height;
        }


        protected override void OnResize(ResizeEventArgs args)
        {
            base.OnResize(args);
            //making sure that we seethe cube after resizing and after moving the window 
            GL.Viewport(0, 0, args.Width, args.Height);
            this.screenHeight = args.Width;
            this.screenHeight = args.Height;

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            //creating our "cube"
            chunk = new Chunk();
            

            //making sure we dont render what we cant see to save some memory and speed
            GL.CullFace(CullFaceMode.Back);

            //creating our camera and moving it to 0,0,3 from the 0,0,0 coordinates
            camera = new Camera(screenWidth, screenHeight, (0,0,3));

            //it is for making 3d, without it we wouldnt be able to tell what is in front and what is in the back
            GL.Enable(EnableCap.DepthTest);

            //grabbing our cursos
            CursorState = CursorState.Grabbed;



        }

        protected override void OnUnload()
        {
            base.OnUnload();

            //deleting out chunk
            chunk.Delete();

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //coloring the background
            GL.ClearColor(0.6f, 0.3f, 1f, 1f);

            //refreshing background and depth every frame
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //finishing creating our camera
            chunk.MakeMatrix(camera);

            //rotating sides every frame
            if (rotationSide == 1)//left
            {
                //rotating on screen
                chunk.RotateLeft(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    //reseting rotation
                    rotationSide = 0;
                    rotationCount = 0;
                    //rotating side in circle
                    ShiftLeft();

                    //changing rotated side for other sides
                    chunk.top_side[0] = chunk.left_side[4];
                    chunk.top_side[1] = chunk.left_side[3];
                    chunk.top_side[2] = chunk.left_side[2];

                    chunk.front_side[0] = chunk.left_side[6];
                    chunk.front_side[1] = chunk.left_side[5];
                    chunk.front_side[2] = chunk.left_side[4];

                    chunk.back_side[4] = chunk.left_side[2];
                    chunk.back_side[5] = chunk.left_side[1];
                    chunk.back_side[6] = chunk.left_side[0];

                    chunk.bottom_side[0] = chunk.left_side[0];
                    chunk.bottom_side[1] = chunk.left_side[7];
                    chunk.bottom_side[2] = chunk.left_side[6];

                }

            }
            else if (rotationSide == 2)//top
            {
                chunk.RotateTop(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    rotationSide = 0;
                    rotationCount = 0;

                    ShifTop();

                    chunk.left_side[4] = chunk.top_side[0];
                    chunk.left_side[3] = chunk.top_side[1];
                    chunk.left_side[2] = chunk.top_side[2];

                    chunk.front_side[4] = chunk.top_side[6];
                    chunk.front_side[3] = chunk.top_side[7];
                    chunk.front_side[2] = chunk.top_side[0];

                    chunk.right_side[4] = chunk.top_side[4];
                    chunk.right_side[3] = chunk.top_side[5];
                    chunk.right_side[2] = chunk.top_side[6];

                    chunk.back_side[4] = chunk.top_side[2];
                    chunk.back_side[3] = chunk.top_side[3];
                    chunk.back_side[2] = chunk.top_side[4];

                }
            }
            else if (rotationSide == 3)//front
            {
                chunk.RotateFront(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    rotationSide = 0;
                    rotationCount = 0;

                    ShifFront();

                    chunk.left_side[4] = chunk.front_side[2];
                    chunk.left_side[5] = chunk.front_side[1];
                    chunk.left_side[6] = chunk.front_side[0];

                    chunk.top_side[0] = chunk.front_side[2];
                    chunk.top_side[7] = chunk.front_side[3];
                    chunk.top_side[6] = chunk.front_side[4];

                    chunk.right_side[0] = chunk.front_side[6];
                    chunk.right_side[1] = chunk.front_side[5];
                    chunk.right_side[2] = chunk.front_side[4];

                    chunk.bottom_side[2] = chunk.front_side[0];
                    chunk.bottom_side[3] = chunk.front_side[7];
                    chunk.bottom_side[4] = chunk.front_side[6];

                }
            }
            else if (rotationSide == 4)//right
            {
                chunk.RotateRight(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    rotationSide = 0;
                    rotationCount = 0;

                    ShifRight();

                    chunk.front_side[4] = chunk.right_side[2];
                    chunk.front_side[5] = chunk.right_side[1];
                    chunk.front_side[6] = chunk.right_side[0];

                    chunk.top_side[4] = chunk.right_side[4];
                    chunk.top_side[5] = chunk.right_side[3];
                    chunk.top_side[6] = chunk.right_side[2];

                    chunk.back_side[0] = chunk.right_side[6];
                    chunk.back_side[1] = chunk.right_side[5];
                    chunk.back_side[2] = chunk.right_side[4];

                    chunk.bottom_side[4] = chunk.right_side[0];
                    chunk.bottom_side[5] = chunk.right_side[7];
                    chunk.bottom_side[6] = chunk.right_side[6];

                }
            }
            else if (rotationSide == 5)//bottom
            {
                chunk.RotateBottom(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    rotationSide = 0;
                    rotationCount = 0;

                    ShifBottom();

                    chunk.front_side[0] = chunk.bottom_side[2];
                    chunk.front_side[7] = chunk.bottom_side[3];
                    chunk.front_side[6] = chunk.bottom_side[4];

                    chunk.right_side[0] = chunk.bottom_side[4];
                    chunk.right_side[7] = chunk.bottom_side[5];
                    chunk.right_side[6] = chunk.bottom_side[6];

                    chunk.left_side[0] = chunk.bottom_side[0];
                    chunk.left_side[7] = chunk.bottom_side[1];
                    chunk.left_side[6] = chunk.bottom_side[2];

                    chunk.back_side[0] = chunk.bottom_side[6];
                    chunk.back_side[7] = chunk.bottom_side[7];
                    chunk.back_side[6] = chunk.bottom_side[0];

                }
            }
            else if (rotationSide == 6)//back
            {
                chunk.RotateBack(Rot);
                rotationCount++;
                if (rotationCount >= NumRot)
                {

                    rotationSide = 0;
                    rotationCount = 0;

                    ShifBack();

                    chunk.left_side[0] = chunk.back_side[6];
                    chunk.left_side[1] = chunk.back_side[5];
                    chunk.left_side[2] = chunk.back_side[4];

                    chunk.right_side[4] = chunk.back_side[2];
                    chunk.right_side[5] = chunk.back_side[1];
                    chunk.right_side[6] = chunk.back_side[0];

                    chunk.top_side[2] = chunk.back_side[4];
                    chunk.top_side[3] = chunk.back_side[3];
                    chunk.top_side[4] = chunk.back_side[2];

                    chunk.bottom_side[0] = chunk.back_side[6];
                    chunk.bottom_side[7] = chunk.back_side[7];
                    chunk.bottom_side[6] = chunk.back_side[0];

                }
            }

            //render chunk
            chunk.Render();

            //this is here for making everything work
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //inputing mouse and keyboard arguments
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;


            //checking which side to rotate
            base.OnUpdateFrame(args);
            if(rotationSide == 0)
            {
                if (input.IsKeyPressed(Keys.T))
                {
                    rotationSide = 1;
                }

                if (input.IsKeyPressed(Keys.Y))
                {
                    rotationSide = 2;
                }

                if (input.IsKeyPressed(Keys.U))
                {
                    rotationSide = 3;
                }

                if (input.IsKeyPressed(Keys.G))
                {
                    rotationSide = 4;
                }

                if (input.IsKeyPressed(Keys.H))
                {
                    rotationSide = 5;
                }

                if (input.IsKeyPressed(Keys.J))
                {
                    rotationSide = 6;
                }
            }
            
            //updating camera position with mouse and keyboard arguments
            camera.Update(input, mouse, args);
        }

        //shifting sides after rotation
        void ShiftLeft()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.left_side[0];
                chunk.left_side[0] = chunk.left_side[7];
                chunk.left_side[7] = chunk.left_side[6];
                chunk.left_side[6] = chunk.left_side[5];
                chunk.left_side[5] = chunk.left_side[4];
                chunk.left_side[4] = chunk.left_side[3];
                chunk.left_side[3] = chunk.left_side[2];
                chunk.left_side[2] = chunk.left_side[1];
                chunk.left_side[1] = temp;
            }
            
        }

        void ShifTop()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.top_side[0];
                chunk.top_side[0] = chunk.top_side[7];
                chunk.top_side[7] = chunk.top_side[6];
                chunk.top_side[6] = chunk.top_side[5];
                chunk.top_side[5] = chunk.top_side[4];
                chunk.top_side[4] = chunk.top_side[3];
                chunk.top_side[3] = chunk.top_side[2];
                chunk.top_side[2] = chunk.top_side[1];
                chunk.top_side[1] = temp;
            }

        }

        void ShifFront()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.front_side[0];
                chunk.front_side[0] = chunk.front_side[7];
                chunk.front_side[7] = chunk.front_side[6];
                chunk.front_side[6] = chunk.front_side[5];
                chunk.front_side[5] = chunk.front_side[4];
                chunk.front_side[4] = chunk.front_side[3];
                chunk.front_side[3] = chunk.front_side[2];
                chunk.front_side[2] = chunk.front_side[1];
                chunk.front_side[1] = temp;
            }

        }

        void ShifRight()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.right_side[0];
                chunk.right_side[0] = chunk.right_side[7];
                chunk.right_side[7] = chunk.right_side[6];
                chunk.right_side[6] = chunk.right_side[5];
                chunk.right_side[5] = chunk.right_side[4];
                chunk.right_side[4] = chunk.right_side[3];
                chunk.right_side[3] = chunk.right_side[2];
                chunk.right_side[2] = chunk.right_side[1];
                chunk.right_side[1] = temp;
            }

        }

        void ShifBack()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.back_side[0];
                chunk.back_side[0] = chunk.back_side[7];
                chunk.back_side[7] = chunk.back_side[6];
                chunk.back_side[6] = chunk.back_side[5];
                chunk.back_side[5] = chunk.back_side[4];
                chunk.back_side[4] = chunk.back_side[3];
                chunk.back_side[3] = chunk.back_side[2];
                chunk.back_side[2] = chunk.back_side[1];
                chunk.back_side[1] = temp;
            }

        }

        void ShifBottom()
        {
            for (int i = 0; i < 2; i++)
            {
                int temp = chunk.bottom_side[0];
                chunk.bottom_side[0] = chunk.bottom_side[7];
                chunk.bottom_side[7] = chunk.bottom_side[6];
                chunk.bottom_side[6] = chunk.bottom_side[5];
                chunk.bottom_side[5] = chunk.bottom_side[4];
                chunk.bottom_side[4] = chunk.bottom_side[3];
                chunk.bottom_side[3] = chunk.bottom_side[2];
                chunk.bottom_side[2] = chunk.bottom_side[1];
                chunk.bottom_side[1] = temp;
            }

        }

    }
}

