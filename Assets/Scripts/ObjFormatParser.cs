using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SOV
{
    public static class ObjFormatParser
    {
        public static void ParseMesh(IEnumerable<string> lines, ref MeshWrapper meshWrapper)
        {
            foreach (var line in lines)
                ParseLine(line, ref meshWrapper);
        }

        private static void ParseLine(string line, ref MeshWrapper meshWrapper)
        {
            var args = line.Split(' ');

            switch (args[0])
            {
                case "v": // vertex
                    meshWrapper.Vertices.Add(ParseVector3Args(args));
                    break;

                case "vt": // uv
                    meshWrapper.UVs.Add(ParseVector3Args(args, true));
                    break;

                case "vn": // normal
                    meshWrapper.Normals.Add(ParseVector3Args(args));
                    break;

                case "f": // face
                    var faceBlocks = ParseFaceArgs(args);

                    // calculating triangles
                    meshWrapper.GlobalFaceBlockIndex += faceBlocks.Count;
                    for (var i = meshWrapper.GlobalFaceBlockIndex - faceBlocks.Count + 1;
                        i + 1 < meshWrapper.GlobalFaceBlockIndex;
                        i++)
                    {
                        meshWrapper.Triangles.Add(meshWrapper.GlobalFaceBlockIndex - faceBlocks.Count);
                        meshWrapper.Triangles.Add(i);
                        meshWrapper.Triangles.Add(i + 1);
                    }

                    meshWrapper.Faces.AddRange(faceBlocks);
                    break;
            }
        }

        private static Vector3 ParseVector3Args(string[] args, bool isVector2 = false)
        {
            return new Vector3(
                float.Parse(args[1], CultureInfo.InvariantCulture),
                float.Parse(args[2], CultureInfo.InvariantCulture),
                !isVector2 ? float.Parse(args[3], CultureInfo.InvariantCulture) : 0);
        }
        
        private static List<Vector3Int> ParseFaceArgs(string[] args)
        {
            var result = new List<Vector3Int>();

            for (var i = 1; i < args.Length; i++)
            {
                if (args[i] == string.Empty) continue;
                var vert = args[i].Split('/');

                var n = new Vector3Int(
                    int.Parse(vert[0], CultureInfo.InvariantCulture),
                    int.Parse(vert[1], CultureInfo.InvariantCulture),
                    int.Parse(vert[2], CultureInfo.InvariantCulture));

                result.Add(n);
            }

            return result;
        }
    }
}