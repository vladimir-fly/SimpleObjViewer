using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SOV
{
    public static class ObjLoader
    {
        private const string DEFAULT_MODELS_DIR = "models";
        public static Mesh LoadMeshFromFile(string fileName)
        {
            var lines = File.ReadLines(fileName);
            var meshWrapper = new MeshWrapper {Name = Path.GetFileNameWithoutExtension(fileName)};

            ObjFormatParser.ParseMesh(lines, ref meshWrapper);
            
            var mesh = CreateMesh(meshWrapper);

            mesh.RecalculateBounds();
            mesh.Optimize();

            return mesh;
        }

        private static Mesh CreateMesh(MeshWrapper meshWrapper)
        {
            var vertices = new Vector3[meshWrapper.Faces.Count];
            var uvs = new Vector2[meshWrapper.Faces.Count];
            var normals = new Vector3[meshWrapper.Faces.Count];
            var triangles = meshWrapper.Triangles.ToArray();
            var meshName = meshWrapper.Name;
            
            // 
            for (var i = 0; i < meshWrapper.Faces.Count; i++)
            {
                if (meshWrapper.Faces[i].x >= 1)
                    vertices[i] = meshWrapper.Vertices[meshWrapper.Faces[i].x - 1];

                if (meshWrapper.Faces[i].y >= 1)
                    uvs[i] = meshWrapper.UVs[meshWrapper.Faces[i].y - 1];

                if (meshWrapper.Faces[i].z >= 1)
                    normals[i] = meshWrapper.Normals[meshWrapper.Faces[i].z - 1];
            }

            var mesh = new Mesh
            {
                name = meshName,
                vertices = vertices,
                uv = uvs,
                normals = normals,
                triangles = triangles
            };
            return mesh;
        }
        
        public static List<string> GetFiles()
        {
            var files = new string[] { };
            if (Directory.Exists(DEFAULT_MODELS_DIR))
                files = Directory.GetFiles(DEFAULT_MODELS_DIR, "*.obj");
            
            return files.ToList();
        }
    }
}