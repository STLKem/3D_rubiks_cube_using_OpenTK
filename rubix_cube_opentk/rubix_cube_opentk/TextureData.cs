using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using static rubix_cube_opentk.BlockData;

namespace rubix_cube_opentk
{
    internal class TextureData
    {
        //coordinates for the each color from atlas starting from 0 column and so on
        public static Dictionary<Faces, Vector2> blockTypeUVCoord = new Dictionary<Faces, Vector2>()
        {
            {Faces.Front, new Vector2(0f,0f) },
            {Faces.Left, new Vector2(1f,0f) },
            {Faces.Right, new Vector2(0f,1f) },
            {Faces.Back, new Vector2(1f, 1f) },
            {Faces.Top, new Vector2(0f, 2f) },
            {Faces.Bottom, new Vector2(1f,2f) },
            {Faces.Empty, new Vector2(2f,2f) },

        };

        //alternative way to do coords for texturenot like above it is just normolized
        //public static readonly Dictionary<BlockData.Faces, List<Vector2>> textureData = new Dictionary<BlockData.Faces, List<Vector2>>()
        //{
        //    {BlockData.Faces.Front, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },


        //    {BlockData.Faces.Back, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },


        //    {BlockData.Faces.Right, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },


        //    {BlockData.Faces.Left, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },


        //    {BlockData.Faces.Top, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },


        //    {BlockData.Faces.Bottom, new List<Vector2>()
        //    {
        //        new Vector2(0, 1),
        //        new Vector2(1, 1),
        //        new Vector2(1, 0),
        //        new Vector2(0, 0)
        //    } },
        //};
    }
}
