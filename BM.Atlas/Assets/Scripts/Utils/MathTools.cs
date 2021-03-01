//============================================================================================================
/**
 *  @file       Utils.cs
 *  @brief      Utility functions.
 *  @details    This file contains the implementation of the Utils.Math static class.
 *  @author     Omar Mendoza Montoya (email: omendoz@live.com.mx).
 *  @copyright  All rights reserved to BrainModes project of the Charité Universitätsmedizin Berlin.
 */
//============================================================================================================

//============================================================================================================
//        REFERENCES
//============================================================================================================
using System;
using UnityEngine;
using System.Collections.Generic;

//============================================================================================================
namespace Utils
{
    /**
     *  @brief      Mathematical tools.
     *  @details    This class contains static helper functions for mathematical operations.
     */
    static public class MathTools
    {
        /**
         *  @brief      Average.
         *  @details    This method computes the average of the specified array.
         *  @param[in]  data  The input data.
         *  @returns    The average value of the input data.
         */
        public static double Average(double[] data)
        {
            if ((data == null) || (data.Length == 0))
                return 0;

            var n = data.Length;

            double m = 0;
            for (int i = 0; i < n; i++)
                m += data[i];
            m /= n;

            return m;
        }

        /**
         *  @brief      Signal average.
         *  @details    This method computes the average of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The average values of each row in data.
         */
        public static double[] Average(double[,] data)
        {
            if (data == null)
                return null;

            var nr = data.GetLength(0);
            var nc = data.GetLength(1);
            var res = new double[nr];

            if (nc > 0)
            {
                for (int i = 0; i < nr; i++)
                {
                    double m = 0;
                    for (int j = 0; j < nc; j++)
                        m += data[i, j];
                    m /= nc;
                    res[i] = m;
                }
            }

            return res;
        }

        /**
         *  @brief      Variance.
         *  @details    This method computes the variance of the specified array.
         *  @param[in]  data  The input data.
         *  @returns    The variance of the input data.
         */
        public static double Variance(double[] data)
        {
            if ((data == null) || (data.Length < 1))
                return 0;

            var n = data.Length;

            double m = 0;
            for (int i = 0; i < n; i++)
                m += data[i];
            m /= n;

            double v = 0;
            for (int i = 0; i < n; i++)
                v += (data[i] - m) * (data[i] - m);
            v /= (n - 1);

            return m;
        }

        /**
         *  @brief      Variance.
         *  @details    This method computes the sample variance of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The variance of each row in data.
         */
        public static double[] Variance(double[,] data)
        {
            if (data == null)
                return null;

            var nr = data.GetLength(0);
            var nc = data.GetLength(1);
            var res = new double[nr];

            if (nc > 1)
            {
                for (int i = 0; i < nr; i++)
                {
                    double m = 0;
                    for (int j = 0; j < nc; j++)
                        m += data[i, j];
                    m /= nc;

                    double v = 0;
                    for (int j = 0; j < nc; j++)
                        v += (data[i, j] - m) * (data[i, j] - m);
                    v /= (nc - 1);

                    res[i] = v;
                }
            }

            return res;
        }

        /**
         *  @brief      Power.
         *  @details    This method computes the variance of the specified time series.
         *  @param[in]  data  The input data.
         *  @returns    The power of the input data.
         */
        public static double Power(double[] data)
        {
            if ((data == null) || (data.Length < 1))
                return 0;

            var n = data.Length;

            double m = 0;
            for (int i = 0; i < n; i++)
                m += data[i] * data[i];
            m /= n;

            return m;
        }

        /**
         *  @brief      Power.
         *  @details    This method computes the power of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The power of each row in data.
         */
        public static double[] Power(double[,] data)
        {
            if (data == null)
                return null;

            var nr = data.GetLength(0);
            var nc = data.GetLength(1);
            var res = new double[nr];

            if (nc > 1)
            {
                for (int i = 0; i < nr; i++)
                {
                    double m = 0;
                    for (int j = 0; j < nc; j++)
                        m += data[i, j] * data[i, j];
                    m /= nc;

                    res[i] = m;
                }
            }

            return res;
        }

        /**
         *  @brief      Standard deviation.
         *  @details    This method computes the standard deviation of the specified array.
         *  @param[in]  data  The input data.
         *  @returns    The standard deviation of the input data.
         */
        public static double StandardDeviation(double[] data)
        {
            return Math.Sqrt(Variance(data));
        }

        /**
         *  @brief      Standard deviation.
         *  @details    This method computes the sample standard deviation of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The standard deviation of each row in data.
         */
        public static double[] StandardDeviation(double[,] data)
        {
            var res = Variance(data);

            if (res == null)
                return res;

            var nr = res.Length;
            for (int i = 0; i < nr; i++)
                res[i] = Math.Sqrt(res[i]);

            return res;
        }

        /**
         *  @brief      Maximum.
         *  @details    This method computes the maximum of the specified array.
         *  @param[in]  data  The input data.
         *  @returns    The maximum of the input data.
         */
        public static double Max(double[] data)
        {
            if ((data == null) || (data.Length < 1))
                return double.MinValue;

            var n = data.Length;

            double m = double.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (data[i] > m)
                    m = data[i];
            }

            return m;
        }

        /**
         *  @brief      Signal maximum values.
         *  @details    This method computes the maximum values of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The maximum values of each row in data.
         */
        public static double[] Max(double[,] data)
        {
            if (data == null)
                return null;

            var nr = data.GetLength(0);
            var nc = data.GetLength(1);
            var res = new double[nr];

            if (nc > 0)
            {
                for (int i = 0; i < nr; i++)
                {
                    double m = double.MinValue;
                    for (int j = 0; j < nc; j++)
                    {
                        if (data[i, j] > m)
                            m = data[i, j];
                    }
                    res[i] = m;
                }
            }

            return res;
        }

        /**
         *  @brief      Minimum.
         *  @details    This method computes the minimum of the specified array.
         *  @param[in]  data  The input data.
         *  @returns    The minimum of the input data.
         */
        public static double Min(double[] data)
        {
            if ((data == null) || (data.Length < 1))
                return double.MaxValue;

            var n = data.Length;

            double m = double.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (data[i] < m)
                    m = data[i];
            }

            return m;
        }

        /**
         *  @brief      Signal minumum values.
         *  @details    This method computes the minumum values of the specified timeseries along the 
         *              column dimension.
         *  @param[in]  data  The input data.
         *  @returns    The minumum values of each row in data.
         */
        public static double[] Min(double[,] data)
        {
            if (data == null)
                return null;

            var nr = data.GetLength(0);
            var nc = data.GetLength(1);
            var res = new double[nr];

            if (nc > 0)
            {
                for (int i = 0; i < nr; i++)
                {
                    double m = double.MaxValue;
                    for (int j = 0; j < nc; j++)
                    {
                        if (data[i, j] < m)
                            m = data[i, j];
                    }
                    res[i] = m;
                }
            }

            return res;
        }

        /**
         *  @brief      Intersection between a line and a plane.
         *  @details    This method computes the intersection point between a line and a plane.
         *  @param[in]  linePoint  Point that belongs to the line.
         *  @param[in]  lineVec  Vector in the direction of the line.
         *  @param[in]  planePoint  Point that belongs to the plane.  
         *  @param[in]  planeNormal  Normal vector to the plane.
         *  @returns    The intersection point between the specified line and plane.
         */
        public static Vector3 LinePlaneIntersection(Vector3 linePoint, Vector3 lineVec,
            Vector3 planePoint, Vector3 planeNormal)
        {
            Vector3 intersection = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

            var num = Vector3.Dot((planePoint - linePoint), planeNormal);
            var den = Vector3.Dot(lineVec, planeNormal);

            if (den != 0.0f)
            {
                lineVec *= num / den;
                intersection = linePoint + lineVec;
            }

            return intersection;
        }

        /**
         *  @brief      Vertex boundaries.
         *  @details    This method calculates the boundaries of a set of vertices.
         *  @param[in]  vertices  The array with the points.
         *  @param[out]  min  The lower limits of the boundaries.
         *  @param[out]  max  The upper limits of the boundaries.
         */
        public static void Boundaries(Vector3[] vertices, out Vector3 min, out Vector3 max)
        {
            if (vertices == null)
            {
                max = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                min = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                return;
            }

            max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            for (int i = 0; i < vertices.Length; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (vertices[i][k] > max[k])
                        max[k] = vertices[i][k];

                    if (vertices[i][k] < min[k])
                        min[k] = vertices[i][k];
                }
            }
        }

        /**
         *  @brief      Line boundaries.
         *  @details    This method calculates the boundaries of a set of lines.
         *  @param[in]  lines  The array with the points of each line.
         *  @param[out]  min  The lower limits of the boundaries.
         *  @param[out]  max  The upper limits of the boundaries.
         */
        public static void Boundaries(Vector3[][] lines, out Vector3 min, out Vector3 max)
        {
            if (lines == null)
            {
                max = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                min = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                return;
            }

            max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (lines[i][j][k] > max[k])
                            max[k] = lines[i][j][k];

                        if (lines[i][j][k] < min[k])
                            min[k] = lines[i][j][k];
                    }
                }
            }
        }

        /**
         *  @brief      Center lines.
         *  @details    This method centers the specified array of lines.
         *  @param[in]  lines  The array with the points of each line.
         *  @returns    The centered lines.
         */
        public static Vector3[][] CenterLines(Vector3[][] lines)
        {
            if (lines == null)
                return null;

            Vector3 max;
            Vector3 min;
            Boundaries(lines, out min, out max);

            var center = (max + min) / 2f;

            var newLines = new Vector3[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                newLines[i] = new Vector3[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                    newLines[i][j] = lines[i][j] - center;
            }

            return newLines;
        }

        /**
         *  @brief      Reorder point lines.
         *  @details    This method reorders the points of each specified line according to their
         *              distance to the center.
         *  @param[in]  lines  The array with the points of each line.
         *  @returns    The reordered lines.
         */
        public static Vector3[][] ReorderPointLines(Vector3[][] lines)
        {
            if (lines == null)
                return null;

            var newLines = new Vector3[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                newLines[i] = new Vector3[lines[i].Length];

                var l = lines[i].Length;
                var d1 = (lines[i][0]).sqrMagnitude;
                var d2 = (lines[i][l - 1]).sqrMagnitude;

                if (d1 > d2)
                {
                    for (int j = 0; j < l; j++)
                        newLines[i][j] = lines[i][l - j - 1];
                }
                else
                {
                    for (int j = 0; j < l; j++)
                        newLines[i][j] = lines[i][j];
                }

            }

            return newLines;
        }

        /**
         *  @brief      Filter lines.
         *  @details    This method removes the lines which length is shorter than the specified values.
         *  @param[in]  lines  The array with the points of each line.
         *  @param[in]  minLenght  The minimum length to accept a line.
         *  @returns    The filtered lines.
         */
        public static Vector3[][] FilterLines(Vector3[][] lines, float minLenght)
        {
            if (lines == null)
                return null;

            var newLines = new List<Vector3[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                var l = lines[i].Length;
                float lineLenght = 0;
                for (int j = 0; j < (l - 1); j++)
                    lineLenght += (lines[i][j + 1] - lines[i][j]).magnitude;

                if (lineLenght > minLenght)
                    newLines.Add(lines[i]);
            }

            return newLines.ToArray();
        }

        /**
         *  @brief      Downsample lines.
         *  @details    This method downsample the set of specified lines.
         *  @param[in]  lines  The array with the points of each line.
         *  @param[in]  rate  The downsample rate.
         *  @returns    The new lines.
         */
        public static Vector3[][] DownsampleLines(Vector3[][] lines, double rate)
        {
            if (lines == null)
                return null;

            if (rate < 1)
                return null;

            var newLines = new Vector3[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                var l = lines[i].Length;
                if (l < 2)
                {
                    newLines[i] = new Vector3[l];
                    for (int j = 0; j < (l - 1); j++)
                        newLines[i][j] = lines[i][j];
                    continue;
                }

                var npoints = System.Convert.ToInt32(l / rate);
                if (npoints < 2)
                    npoints = 2;

                newLines[i] = new Vector3[npoints];

                float lineLenght = 0;
                for (int j = 0; j < (l - 1); j++)
                    lineLenght += (lines[i][j + 1] - lines[i][j]).magnitude;

                int pindex = 1;
                float segLength = (lines[i][1] - lines[i][0]).magnitude;
                float accLength = segLength;

                for (int k = 0; k < npoints; k++)
                {
                    var t = Convert.ToSingle(k) / Convert.ToSingle(npoints - 1);
                    var s = lineLenght * t;
                    while ((s > accLength) && (pindex < (l - 1)))
                    {
                        pindex++;
                        segLength = (lines[i][pindex] - lines[i][pindex - 1]).magnitude;
                        accLength += segLength;
                    }

                    var si = accLength - segLength - s;
                    var ti = si / segLength;

                    newLines[i][k] = (1 - ti) * lines[i][pindex - 1] + ti * lines[i][pindex];
                }

            }

            return newLines;
        }
    }
}

//============================================================================================================
//        END OF FILE
//============================================================================================================