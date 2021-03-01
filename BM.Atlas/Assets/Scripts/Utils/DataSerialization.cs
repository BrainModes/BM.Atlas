//============================================================================================================
/**
 *  @file       Utils.cs
 *  @brief      Utility functions.
 *  @details    This file contains the implementation of the Utils.DataSerialization static class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================
using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//============================================================================================================
namespace Utils
{
    /**
     *  @brief      Class for data serialization.
     *  @details    This class contains static helper functions for data serialization.
     */
    static public class DataSerialization
    {
        /**
         *  @brief      Read line from binary stream.
         *  @details    This method reads a text line from a binary stream.
         *  @param[in]  reader  The binary input stream.
         *  @returns    The line extracted from the reader.        
         */
        public static String ReadLine(BinaryReader reader)
        {
            var result = new StringBuilder();
            bool foundEndOfLine = false;            
            while (!foundEndOfLine)
            {
                try
                {
                    var ch = reader.ReadChar();
                    switch (ch)
                    {
                        case '\r':
                            if (reader.PeekChar() == '\n')
                                reader.ReadChar();
                            foundEndOfLine = true;
                            break;
                        case '\n':
                            foundEndOfLine = true;
                            break;
                        default:
                            result.Append(ch);
                            break;
                    }
                }
                catch 
                {
                    if (result.Length == 0)
                        return null;
                    else
                        break;
                }

                
            }
            return result.ToString();
        }

        /**
         *  @brief      Save data.
         *  @details    This method saves an array of data in a binary file.
         *  @param[in]  fileName  The name of the file where the data will be saved.
         *  @param[in]  channels  The names of the channels of the data array.
         *  @param[in]  data  The array of data.         
         */
        public static void SaveData(string fileName, string[] channels, double[,] data)
        {
            if ((channels == null) || (data == null))
                return;

            var n = data.GetLength(0);
            var nc = channels.Length;
            if ((n == 0) || (data.GetLength(1) != nc))
                return;

            var stream = File.Open(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
                        
            writer.Write(Encoding.UTF8.GetBytes("data file\n"));
            writer.Write(Encoding.UTF8.GetBytes("samples = " + n.ToString() + "\n"));
            writer.Write(Encoding.UTF8.GetBytes("channels = " + nc.ToString() + "\n"));
            for (int i = 0; i<nc; i++)
                writer.Write(Encoding.UTF8.GetBytes("\t" + channels[i].Trim() + "\n"));
            
            var vals = new double[n * nc];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < nc; j++)
                    vals[i * nc + j] = data[i,j];

            var dataArray = vals.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
            writer.Write(dataArray);

            stream.Close();
        }

        /**
         *  @brief      Load data from file.
         *  @details    This method loads an array of data from a file.
         *  @param[in]  fileName  The name of the file with the data to load.
         *  @param[out] channels  The names of the channels of the data array.
         *  @param[out] data  The array of data.         
         */
        public static void LoadData(string fileName, out string[] channels, out double[,] data)
        {
            channels = new string[0];
            data = new double[0,0];

            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            try
            {
                var line = ReadLine(reader);
                if (line.Trim().ToLower() != "data file")
                    return;

                int n = 0;
                int nc = 0;

                line = ReadLine(reader).Trim();
                string[] cmps = line.Split('=');
                if ((cmps.Length == 2) && (cmps[0].Trim().ToLower() == "samples"))
                    n = Convert.ToInt32(cmps[1].Trim());

                line = ReadLine(reader).Trim();
                cmps = line.Split('=');
                if ((cmps.Length == 2) && (cmps[0].Trim().ToLower() == "channels"))
                    nc = Convert.ToInt32(cmps[1].Trim());

                data = new double[n, nc];
                channels = new string[nc];

                for (int i = 0; i < nc; i++)
                    channels[i] = ReadLine(reader).Trim();

                byte[] dataArray = reader.ReadBytes(n * nc * sizeof(double));

                var ddata = new double[n * nc];
                Buffer.BlockCopy(dataArray, 0, ddata, 0, dataArray.Length);

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < nc; j++)
                        data[i, j] = ddata[i * nc + j];
            }
            catch
            {
                channels = new string[0];
                data = new double[0, 0];
            }
        }

        /**
         *  @brief      Load mapping from asset.
         *  @details    This method loads a mapping from an asset.
         *  @param[in]  name  The name of the asset with the data.
         *  @returns    The loaded mapping.
         */
        public static void LoadDataFromAsset(string name, out string[] channels, out double[,] data)
        {
            channels = new string[0];
            data = new double[0, 0];

            var asset = Resources.Load<TextAsset>(name);
            if (asset == null)
                return;

            var stream = new MemoryStream(asset.bytes);
            BinaryReader reader = new BinaryReader(stream);

            try
            {
                var line = ReadLine(reader);
                if (line.Trim().ToLower() != "data file")
                    return;

                int n = 0;
                int nc = 0;

                line = ReadLine(reader).Trim();
                string[] cmps = line.Split('=');
                if ((cmps.Length == 2) && (cmps[0].Trim().ToLower() == "samples"))
                    n = Convert.ToInt32(cmps[1].Trim());

                line = ReadLine(reader).Trim();
                cmps = line.Split('=');
                if ((cmps.Length == 2) && (cmps[0].Trim().ToLower() == "channels"))
                    nc = Convert.ToInt32(cmps[1].Trim());

                data = new double[n, nc];
                channels = new string[nc];

                for (int i = 0; i < nc; i++)
                    channels[i] = ReadLine(reader).Trim();

                byte[] dataArray = reader.ReadBytes(n * nc * sizeof(double));

                var ddata = new double[n * nc];
                Buffer.BlockCopy(dataArray, 0, ddata, 0, dataArray.Length);

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < nc; j++)
                        data[i, j] = ddata[i * nc + j];
            }
            catch
            {
                channels = new string[0];
                data = new double[0, 0];
            }
        }

        /**
         *  @brief      Load data from text asset.
         *  @details    This method loads data from a text asset.
         *  @param[in]  name  The name of the asset.
         *  @param[out]  channels  The array with the loaded channels.
         *  @param[out]  data  The array with the loaded data.
         */
        public static void LoadDataFromTextAsset(string name, out string[] channels, out double[,] data)
        {
            channels = new string[0];
            data = new double[0, 0];
            var asset = Resources.Load<TextAsset>(name);
            if (asset == null)
                return;

            var lines = asset.text.Split(new string[] { "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            var nPoints = lines.Length - 1;
            if (nPoints <= 0)
                return;

            lines[0] = lines[0].TrimEnd(new char[] { '\n', '\r' });
            channels = lines[0].Split(new char[] { ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            var nChannels = channels.Length;
            for (int c = 0; c < nChannels; c++)
                channels[c] = channels[c].Trim();

            data = new double[nPoints, nChannels];
            for (int p = 0; p < nPoints; p++)
            {
                lines[p + 1] = lines[p + 1].TrimEnd(new char[] { '\n', '\r' });
                var valueText = lines[p + 1].Split(new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (valueText.Length != nChannels)
                    continue;

                for (int c = 0; c < nChannels; c++)
                    data[p, c] = Convert.ToDouble(valueText[c]);
            }
        }

        /**
         *  @brief      Load data from text file.
         *  @details    This method loads data from a text file.
         *  @param[in]  fileName  The name of the file.
         *  @param[out]  channels  The array with the loaded channels.
         *  @param[out]  data  The array with the loaded data.
         */
        public static void LoadDataFromTextFile(string fileName, out string[] channels, out double[,] data)
        {
            channels = new string[0];
            data = new double[0, 0];

            var lines = File.ReadAllLines(fileName);

            var nPoints = lines.Length - 1;
            if (nPoints <= 0)
                return;

            lines[0] = lines[0].TrimEnd(new char[] { '\n', '\r' });
            channels = lines[0].Split(new char[] { ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            var nChannels = channels.Length;
            for (int c = 0; c < nChannels; c++)
                channels[c] = channels[c].Trim();

            data = new double[nPoints, nChannels];
            for (int p = 0; p < nPoints; p++)
            {
                lines[p + 1] = lines[p + 1].TrimEnd(new char[] { '\n', '\r' });
                var valueText = lines[p + 1].Split(new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (valueText.Length != nChannels)
                    continue;

                for (int c = 0; c < nChannels; c++)
                    data[p, c] = Convert.ToDouble(valueText[c]);
            }
        }

        /**
         *  @brief      Load model.
         *  @details    This method loads a model file.
         *  @param[in]  fileName  The name of the file with the object.         
         */
        public static Mesh[] LoadModel(string fileName)
        {
            //// Load object model ////
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int[]> faces = new List<int[]>();
            List<int[]> faceNormals = new List<int[]>();

            foreach (string ln in File.ReadAllLines(fileName))
            {
                if (ln.Length > 0 && ln[0] != '#')
                {
                    string l = ln.Trim().Replace("  ", " ");
                    string[] cmps = l.Split(' ');

                    if (cmps[0] == "v")
                    {
                        // Vertex.
                        float x = -float.Parse(cmps[1]);
                        float y = float.Parse(cmps[2]);
                        float z = float.Parse(cmps[3]);
                        vertices.Add(new Vector3(x, y, z));
                    }
                    else if (cmps[0] == "vn")
                    {
                        // Vertex normal.
                        float x = -float.Parse(cmps[1]);
                        float y = float.Parse(cmps[2]);
                        float z = float.Parse(cmps[3]);
                        normals.Add(new Vector3(x, y, z));
                    }
                    else if (cmps[0] == "f")
                    {
                        // Face.
                        int nPoints = cmps.Length - 1;
                        int[] indexes = new int[nPoints];
                        int[] normalIndexes = new int[nPoints];
                        for (int i = 0; i < nPoints; i++)
                        {
                            string felement = cmps[i + 1];
                            int vertexIndex = -1;
                            int normalIndex = -1;

                            if (felement.Contains("//"))
                            {
                                // Point and vertex normal.
                                string[] elementComps = felement.Split('/');
                                vertexIndex = int.Parse(elementComps[0]) - 1;
                                normalIndex = int.Parse(elementComps[2]) - 1;
                            }
                            else if (felement.Count(x => x == '/') == 2)
                            {
                                // Point, vextex texture and normal.
                                string[] elementComps = felement.Split('/');
                                vertexIndex = int.Parse(elementComps[0]) - 1;
                                normalIndex = int.Parse(elementComps[2]) - 1;
                            }
                            else if (!felement.Contains("/"))
                            {
                                // Only point index.
                                vertexIndex = int.Parse(felement) - 1;
                            }
                            else
                            {
                                // Vertex and vertex texture. 
                                string[] elementComps = felement.Split('/');
                                vertexIndex = int.Parse(elementComps[0]) - 1;
                            }

                            indexes[i] = vertexIndex;
                            normalIndexes[i] = normalIndex;
                        }

                        if (indexes.Length < 5 && indexes.Length >= 3)
                        {
                            var ind = new int[] { indexes[0], indexes[2], indexes[1] };
                            faces.Add(ind);

                            ind = new int[] { normalIndexes[0], normalIndexes[2], normalIndexes[1] };
                            faceNormals.Add(ind);

                            // Split quadrilateral.
                            if (indexes.Length > 3)
                            {
                                ind = new int[] { indexes[2], indexes[0], indexes[3] };
                                faces.Add(ind);

                                ind = new int[] { normalIndexes[2], normalIndexes[0], normalIndexes[3] };
                                faceNormals.Add(ind);
                            }
                        }
                    }
                }
            }

            bool importNormals = false;
            for (int i = 0; i < faces.Count; i++)
            {
                if ((faceNormals[i][0] != -1) || (faceNormals[i][1] != -1) || (faceNormals[i][2] != -1))
                {
                    importNormals = true;
                    break;
                }
            }

            ////// Create meshes //////
            var nMeshes = faces.Count / 20000 + Convert.ToInt32((faces.Count % 20000) >= 1);
            var meshes = new Mesh[nMeshes];
            var trianglesPerMesh = faces.Count / nMeshes;

            for (int mm = 0; mm < nMeshes; mm++)
            {
                // Process vertices and normals.   
                var faceOffset = mm * trianglesPerMesh;
                var nTriangles = trianglesPerMesh;
                if (mm == (nMeshes - 1))
                    nTriangles = faces.Count - faceOffset;

                var faceEnd = faceOffset + nTriangles;

                List<Vector3> processedVertices = new List<Vector3>();
                List<Vector3> processedNormals = new List<Vector3>();
                List<int[]> processedFaces = new List<int[]>();

                if (importNormals)
                {
                    var nVertices = vertices.Count;
                    var nNormals = normals.Count;
                    var map = new Dictionary<string, int>();

                    for (int i = faceOffset; i < faceEnd; i++)
                    {
                        var np = faces[i].Length;

                        bool validTriangle = true;
                        for (int j = 0; j < np; j++)
                        {
                            if ((faces[i][j] < 0) || (faces[i][j] > nVertices))
                                validTriangle = false;

                            if ((faceNormals[i][j] < 0) || (faceNormals[i][j] > nNormals))
                                validTriangle = false;
                        }

                        if (!validTriangle)
                            continue;

                        var triangleIndices = new int[3];
                        for (int j = 0; j < np; j++)
                        {
                            string key = faces[i][j] + "|" + faceNormals[i][j];

                            if (!map.ContainsKey(key))
                            {
                                processedVertices.Add(vertices[faces[i][j]]);
                                processedNormals.Add(normals[faceNormals[i][j]]);
                                map[key] = processedVertices.Count - 1;
                            }

                            triangleIndices[j] = map[key];
                        }

                        processedFaces.Add(triangleIndices);
                    }
                }
                else
                {
                    var nVertices = vertices.Count;
                    var map = new Dictionary<int, int>();

                    for (int i = faceOffset; i < faceEnd; i++)
                    {
                        var np = faces[i].Length;

                        bool validTriangle = true;
                        for (int j = 0; j < np; j++)
                        {
                            if ((faces[i][j] < 0) || (faces[i][j] > nVertices))
                                validTriangle = false;
                        }

                        if (!validTriangle)
                            continue;

                        var triangleIndices = new int[3];
                        for (int j = 0; j < np; j++)
                        {
                            if (!map.ContainsKey(faces[i][j]))
                            {
                                processedVertices.Add(vertices[faces[i][j]]);
                                map[faces[i][j]] = processedVertices.Count - 1;
                            }

                            triangleIndices[j] = map[faces[i][j]];
                        }

                        processedFaces.Add(triangleIndices);
                    }
                }

                // Create mesh object.    
                Mesh m = new Mesh();
                m.name = "Mesh" + mm;
                m.subMeshCount = 1;

                // Set vertices.
                var vert = processedVertices.ToArray();
                m.vertices = vert;

                // Set triangles.
                nTriangles = processedFaces.Count;
                int[] triangles = new int[nTriangles * 3];

                for (int i = 0; i < nTriangles; i++)
                {
                    triangles[3 * i] = processedFaces[i][0];
                    triangles[3 * i + 1] = processedFaces[i][1];
                    triangles[3 * i + 2] = processedFaces[i][2];
                }

                m.SetTriangles(triangles, 0);

                // Set normals.
                if (importNormals)
                    m.normals = processedNormals.ToArray();
                else
                    m.RecalculateNormals();

                // Set tangents.        
                var nv = vert.Length;
                var norm = m.normals;
                var tn = new Vector4[nv];

                for (int i = 0; i < nv; i++)
                {
                    var v1 = new Vector3(norm[i][1], -norm[i][0], 0);
                    var v2 = new Vector3(-norm[i][2], 0, norm[i][0]);
                    var vt = v1 + v2;
                    if ((vt[0] == 0) && (vt[1] == 0) && (vt[2] == 0))
                        vt = v1 - v2;

                    vt.Normalize();
                    tn[i] = new Vector4(vt[0], vt[1], vt[2], 1);

                }
                m.tangents = tn;

                // Optimize object and store mesh.
                meshes[mm] = m;
            }

            return meshes;
        }
        
        /**
         *  @brief      Save points in file.
         *  @details    This method saves an array of sampling points in a file.
         *  @param[in]  points  The array with the points to save.
         *  @param[in]  fileName  The name of the output file.
         */
        public static void SavePoints(Vector3[] points, string fileName)
        {
            if (points == null)
                return;

            var np = points.Length;
            if (np == 0)
                return;

            var pointsArray = new float[3 * np];
            for (int i = 0; i < np; i++)
            {
                pointsArray[3 * i] = points[i][0];
                pointsArray[3 * i + 1] = points[i][1];
                pointsArray[3 * i + 2] = points[i][2];
            }

            var dataArray = pointsArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray();

            var stream = File.Open(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(np);
            writer.Write(dataArray);

            stream.Close();
        }

        /**
         *  @brief      Load points from file.
         *  @details    This method loads an array of points from a file.
         *  @param[in]  fileName  The name of the file with the data.
         *  @returns    The array with the data points.
         */
        public static Vector3[] LoadPoints(string fileName)
        {
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            var np = reader.ReadInt32();
            byte[] dataArray = reader.ReadBytes(np * sizeof(float) * 3);
            stream.Close();

            var pointsArray = new float[3 * np];
            Buffer.BlockCopy(dataArray, 0, pointsArray, 0, dataArray.Length);

            var points = new Vector3[np];
            for (int i = 0; i < np; i++)
            {
                points[i][0] = pointsArray[3 * i];
                points[i][1] = pointsArray[3 * i + 1];
                points[i][2] = pointsArray[3 * i + 2];
            }

            return points;
        }

        /**
         *  @brief      Load points from asset.
         *  @details    This method loads an array of points from an asset.
         *  @param[in]  name  The name of the asset with the data.
         *  @returns    The array with the data points.
         */
        public static Vector3[] LoadPointsFromAsset(string name)
        {
            var asset = Resources.Load<TextAsset>(name);
            if (asset == null)
                return new Vector3[0];

            var stream = new MemoryStream(asset.bytes);
            BinaryReader reader = new BinaryReader(stream);
            var np = reader.ReadInt32();
            byte[] dataArray = reader.ReadBytes(np * sizeof(float) * 3);
            stream.Close();

            var pointsArray = new float[3 * np];
            Buffer.BlockCopy(dataArray, 0, pointsArray, 0, dataArray.Length);

            var points = new Vector3[np];
            for (int i = 0; i < np; i++)
            {
                points[i][0] = pointsArray[3 * i];
                points[i][1] = pointsArray[3 * i + 1];
                points[i][2] = pointsArray[3 * i + 2];
            }

            return points;
        }

        /**
         *  @brief      Save mapping in file.
         *  @details    This method saves a mapping in a file.
         *  @param[in]  mapping  The array with the mapping to save.
         *  @param[in]  fileName  The name of the output file.
         */
        public static void SaveMapping(int[][] mapping, string fileName)
        {
            if (mapping == null)
                return;

            var n = mapping.Length;
            if (n == 0)
                return;

            var stream = File.Open(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(n);

            for (int i = 0; i < n; i++)
            {
                writer.Write(mapping[i].Length);
                var dataArray = mapping[i].SelectMany(value => BitConverter.GetBytes(value)).ToArray();
                writer.Write(dataArray);
            }
            stream.Close();
        }

        /**
         *  @brief      Load mapping from file.
         *  @details    This method loads a mapping from a file.
         *  @param[in]  fileName  The name of the file with the data.
         *  @returns    The loaded mapping.
         */
        public static int[][] LoadMapping(string fileName)
        {
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            var n = reader.ReadInt32();
            var mapping = new int[n][];

            for (int i = 0; i < n; i++)
            {
                var ni = reader.ReadInt32();
                byte[] dataArray = reader.ReadBytes(ni * sizeof(int));

                mapping[i] = new int[ni];
                Buffer.BlockCopy(dataArray, 0, mapping[i], 0, dataArray.Length);
            }

            stream.Close();

            return mapping;
        }

        /**
         *  @brief      Load mapping from asset.
         *  @details    This method loads a mapping from an asset.
         *  @param[in]  name  The name of the asset with the data.
         *  @returns    The loaded mapping.
         */
        public static int[][] LoadMappingFromAsset(string name)
        {
            var asset = Resources.Load<TextAsset>(name);
            if (asset == null)
                return new int[0][];

            var stream = new MemoryStream(asset.bytes);
            BinaryReader reader = new BinaryReader(stream);

            var n = reader.ReadInt32();
            var mapping = new int[n][];

            for (int i = 0; i < n; i++)
            {
                var ni = reader.ReadInt32();
                byte[] dataArray = reader.ReadBytes(ni * sizeof(int));

                mapping[i] = new int[ni];
                Buffer.BlockCopy(dataArray, 0, mapping[i], 0, dataArray.Length);
            }

            stream.Close();

            return mapping;
        }

        /**
         *  @brief      Load fiber track files.
         *  @details    This method loads all the text files that are located at the specified path and
         *              returns an array that represent the fiber tracks stored in the files.
         *  @param[in]  path  The path with the files to load.
         *  @returns    The array of tracks.
         */
        public static Vector3[][] LoadFiberTrackFiles(string path)
        {
            // Identify files in the specified path.
            var files = Directory.GetFiles(path);
            var nfiles = files.Length;

            // Load tracks.
            var tracks = new Vector3[nfiles][];
            for (int i = 0; i<nfiles; i++)
            {
                var lines = File.ReadAllLines(files[i]);
                var nPoints = lines.Length;

                if (nPoints > 1)
                {
                    tracks[i] = new Vector3[nPoints];

                    for (int j = 0; j < nPoints; j++)
                    {
                        string l = lines[j].Trim().Replace("  ", " ");
                        string[] cmps = l.Split(' ');

                        if (cmps.Length == 3)
                        {
                            float x = float.Parse(cmps[0]);
                            float y = float.Parse(cmps[1]);
                            float z = float.Parse(cmps[2]);

                            tracks[i][j] = new Vector3(x, y, z);
                        }
                        else
                        {
                            throw new OperationCanceledException("Bad track file.");
                        }
                    }
                }
                else
                {
                    throw new OperationCanceledException("Bad track file.");
                }
            }

            // Return fiber tracks.
            return tracks;
        }

        /**
         *  @brief      Load fiber track file.
         *  @details    This method loads a file that contains fiber tracks. 
         *  @param[in]  fileName  The name of the file with the data.
         *  @returns    The array of tracks.
         */
        public static Vector3[][] LoadFiberTracks(string fileName)
        {
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            var n = reader.ReadInt32();
            var tracks = new Vector3[n][];

            for (int i = 0; i < n; i++)
            {
                var ni = reader.ReadInt32();
                byte[] dataArray = reader.ReadBytes(3*ni * sizeof(float));

                var data = new float[ni * 3];
                Buffer.BlockCopy(dataArray, 0, data, 0, dataArray.Length);

                tracks[i] = new Vector3[ni];
                for (int j = 0; j < ni; j++)
                {
                    tracks[i][j][0] = data[3 * j];
                    tracks[i][j][1] = data[3 * j + 1];
                    tracks[i][j][2] = data[3 * j + 2];
                }
            }

            stream.Close();

            return tracks;
        }

        /**
         *  @brief      Load fiber tracks from asset.
         *  @details    This method loads fiber tracks from an asset file.
         *  @param[in]  name  The name of the asset with the data.
         *  @returns    The array of tracks.
         */
        public static Vector3[][] LoadFiberTracksFromAsset(string name)
        {
            var asset = Resources.Load<TextAsset>(name);
            if (asset == null)
                return new Vector3[0][];

            var stream = new MemoryStream(asset.bytes);
            BinaryReader reader = new BinaryReader(stream);

            var n = reader.ReadInt32();
            var tracks = new Vector3[n][];

            for (int i = 0; i < n; i++)
            {
                var ni = reader.ReadInt32();
                byte[] dataArray = reader.ReadBytes(3*ni * sizeof(float));

                var data = new float[ni * 3];
                Buffer.BlockCopy(dataArray, 0, data, 0, dataArray.Length);

                tracks[i] = new Vector3[ni];
                for (int j = 0; j < ni; j++)
                {
                    tracks[i][j][0] = data[3 * j];
                    tracks[i][j][1] = data[3 * j + 1];
                    tracks[i][j][2] = data[3 * j + 2];
                }
            }

            stream.Close();

            return tracks;
        }

        /**
         *  @brief      Save tracks in file.
         *  @details    This method saves an array of tracks in a file.
         *  @param[in]  tracks  The array with the mapping to save.
         *  @param[in]  fileName  The name of the output file.
         */
        public static void SaveFiberTracks(Vector3[][] tracks, string fileName)
        {
            if (tracks == null)
                return;

            var n = tracks.Length;
            if (n == 0)
                return;

            var stream = File.Open(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(n);
            

            for (int i = 0; i < n; i++)
            {
                var ni = tracks[i].Length;
                writer.Write(ni);

                var vals = new float[3 * ni];
                for (int j = 0; j < ni; j++)
                {
                    vals[3 * j] = tracks[i][j][0];
                    vals[3 * j + 1] = tracks[i][j][1];
                    vals[3 * j + 2] = tracks[i][j][2];
                }

                var dataArray = vals.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
                writer.Write(dataArray);
            }
            stream.Close();
        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================