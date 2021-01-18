using System.Collections.Generic;
using UnityEngine;

namespace SOV
{
    public class MeshWrapper
    {
        public string Name;
        public List<int> Triangles = new List<int>();
        public List<Vector3Int> Faces = new List<Vector3Int>();
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector2> UVs = new List<Vector2>();
        public List<Vector3> Normals = new List<Vector3>();
        public int GlobalFaceBlockIndex;
    }
}