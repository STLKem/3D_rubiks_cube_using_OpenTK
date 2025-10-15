using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using static rubix_cube_opentk.BlockData;

namespace rubix_cube_opentk
{
    internal class Block
    {
        //making some variables
        Vector3 position;
        public Dictionary<BlockData.Faces, BlockData.Things> faces;

        //some uv's coordinates
        public Dictionary<Faces, List<Vector2>> blockUV = new Dictionary<Faces, List<Vector2>>()
        {
            {Faces.Front, new List<Vector2>()},
            {Faces.Back, new List<Vector2>()},
            {Faces.Left, new List<Vector2>()},
            {Faces.Right, new List<Vector2>()},
            {Faces.Top, new List<Vector2>()},
            {Faces.Bottom, new List<Vector2>()},
            {Faces.Empty, new List<Vector2>()}
        };

        //making normolized uv coords for the atlas from 0 to 1
        public Dictionary<Faces, List<Vector2>> GetUVsFromCoordinates(Dictionary<Faces, Vector2> coords)
        {
            Dictionary<Faces, List<Vector2>> faceData = new Dictionary<Faces, List<Vector2>>();

            foreach (var faceCoord in coords)
            {
                faceData[faceCoord.Key] = new List<Vector2>()
                {
                    new Vector2((faceCoord.Value.X+1f)/3, (faceCoord.Value.Y+1f)/3),
                    new Vector2(faceCoord.Value.X/3, (faceCoord.Value.Y+1f)/3),
                    new Vector2(faceCoord.Value.X/3, faceCoord.Value.Y/3),
                    new Vector2((faceCoord.Value.X+1f)/3, faceCoord.Value.Y/3),

                };

            }

            return faceData;

        }

        //making vertices and uv's for every side of the block
        public Block(Vector3 position) 
        {
            this.position = position;

            blockUV = GetUVsFromCoordinates(TextureData.blockTypeUVCoord);

            faces = new Dictionary<BlockData.Faces, BlockData.Things>()
            {
                {BlockData.Faces.Front, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Front]),
                    //UV = TextureData.textureData[BlockData.Faces.Front]
                    UV = blockUV[Faces.Front]
                } },


                {BlockData.Faces.Back, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Back]),
                    //UV = TextureData.textureData[BlockData.Faces.Back]
                    UV = blockUV[Faces.Back]
                } },


                {BlockData.Faces.Right, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Right]),
                    //UV = TextureData.textureData[BlockData.Faces.Right]
                    UV = blockUV[Faces.Right]
                } },


                {BlockData.Faces.Left, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Left]),
                    //UV = TextureData.textureData[BlockData.Faces.Left]
                    UV = blockUV[Faces.Left]
                } },


                {BlockData.Faces.Top, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Top]),
                    //UV = TextureData.textureData[BlockData.Faces.Top]
                    UV = blockUV[Faces.Top]
                } },


                {BlockData.Faces.Bottom, new BlockData.Things
                {
                    Vertices = move(BlockData.blockData[BlockData.Faces.Bottom]),
                    //UV = TextureData.textureData[BlockData.Faces.Bottom]
                    UV = blockUV[Faces.Bottom]
                } },


                {BlockData.Faces.Empty, new BlockData.Things
                {
                    Vertices = null,
                    //UV = TextureData.textureData[BlockData.Faces.Bottom]
                    UV = blockUV[Faces.Empty]
                } },

            };

        }

        //moving "mini-cubes" along vertices to make "mini-cubes" not spawn on top of each other
        public List<Vector3> move (List<Vector3> smt)
        {
            List<Vector3> tmp = new List<Vector3>();
            foreach (var v in smt) 
            {
                tmp.Add(v+position);
            }
            return tmp;
        }

        //returning uv's and vertices of requested side
        public BlockData.Things ret(BlockData.Faces face)
        {
            return faces[face];
        }

    }
}
