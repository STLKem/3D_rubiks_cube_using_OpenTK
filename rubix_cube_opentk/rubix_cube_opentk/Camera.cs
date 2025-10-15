using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rubix_cube_opentk
{

    //Creating Camera which is very strange and idk what is going on in some parts
    internal class Camera
    {
        //defining some variables
        private float screenWidth;
        private float screenHeight;
        private float Speed = 8f;
        private float Sensitivity = 200f;


        Vector3 up = Vector3.UnitY;
        Vector3 front = -Vector3.UnitZ;
        Vector3 right = Vector3.UnitX;

        private float pitch;
        private float yaw = -90.0f;

        private bool firstMove = true;
        public Vector2 lastPos;


        Vector3 position;

        //Camera class for setting variables
        public Camera(float width, float height, Vector3 position)
        {
            screenWidth = width;
            screenHeight = height;
            this.position = position;
        }

        public Matrix4 GetProjectionMatrix()
        { 
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), screenWidth/screenHeight, 0.1f, 100f);
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(position, position + front, up);
        }

        //Updating our camera after our inputs
        public void UpdateVector()
        {
            front.X = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Cos(MathHelper.DegreesToRadians(yaw));//разобраться с этой хуйнёй
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Sin(MathHelper.DegreesToRadians(yaw));


            front = Vector3.Normalize(front);
            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));


            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }
        }

        //Moving Camera
        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs args)
        {
            if (input.IsKeyDown(Keys.W))
            {
                position += front * Speed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                position -= front * Speed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                position -= right * Speed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                position += right * Speed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                position -= up * Speed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                position += up * Speed * (float)args.Time;
            }



            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - lastPos.X;
                var deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);

                yaw += deltaX * Sensitivity * (float)args.Time;
                pitch -= deltaY * Sensitivity * (float)args.Time;
            }

            UpdateVector();

        }

        //Updating Inputs
        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs args)
        {
            InputController(input, mouse, args);
        }

}
}
