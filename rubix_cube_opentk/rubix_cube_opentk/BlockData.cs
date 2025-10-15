using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace rubix_cube_opentk
{
    internal class BlockData
    {

        //Creating some variables
        public enum Faces
        {
            Front,
            Back,
            Right,
            Left,
            Top,
            Bottom,
            Empty
        }

        public struct Things
        {
            public List<Vector2> UV;
            public List<Vector3> Vertices;
        }

        //Coordinates of every face of the "mini-cube"
        public static readonly Dictionary<Faces, List<Vector3>> blockData = new Dictionary<Faces, List<Vector3>>()
        {
            {Faces.Front, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
            }
            },


            {Faces.Back, new List<Vector3>
            {
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
            }
            },


            {Faces.Right, new List<Vector3>
            {
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
            }
            },


            {Faces.Left, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
            }
            },


            {Faces.Top, new List<Vector3>
            {
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f),
            }
            },


            {Faces.Bottom, new List<Vector3>
            {
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f),
            }
            },
        };

    }
}
