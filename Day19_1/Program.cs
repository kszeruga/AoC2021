using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Day19// Note: actual namespace depends on the project name.
{

    public class Program
    {

        public static Rotation3D[] Rotations = new Rotation3D[]
        {
            new Rotation3D(){ matrix = new long[3,3]{{  1,  0,  0},
                                                     {  0,  1,  0},
                                                     {  0,  0,  1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0,  1},
                                                     {  0,  1,  0},
                                                     { -1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{ -1,  0,  0},
                                                     {  0,  1,  0},
                                                     {  0,  0, -1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0, -1},
                                                     {  0,  1,  0},
                                                     {  1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0, -1,  0},
                                                     {  1,  0,  0},
                                                     {  0,  0,  1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0,  1},
                                                     {  1,  0,  0},
                                                     {  0,  1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  1,  0},
                                                     {  1,  0,  0},
                                                     {  0,  0, -1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0, -1},
                                                     {  1,  0,  0},
                                                     {  0, -1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  1,  0},
                                                     { -1,  0,  0},
                                                     {  0,  0,  1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0,  1},
                                                     { -1,  0,  0},
                                                     {  0, -1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0, -1,  0},
                                                     { -1,  0,  0},
                                                     {  0,  0, -1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0, -1},
                                                     { -1,  0,  0},
                                                     {  0,  1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  1,  0,  0},
                                                     {  0,  0, -1},
                                                     {  0,  1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  1,  0},
                                                     {  0,  0, -1},
                                                     { -1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{ -1,  0,  0},
                                                     {  0,  0, -1},
                                                     {  0, -1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0, -1,  0},
                                                     {  0,  0, -1},
                                                     {  1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  1,  0,  0},
                                                     {  0, -1,  0},
                                                     {  0,  0, -1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0, -1},
                                                     {  0, -1,  0},
                                                     { -1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{ -1,  0,  0},
                                                     {  0, -1,  0},
                                                     {  0,  0,  1}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  0,  1},
                                                     {  0, -1,  0},
                                                     {  1,  0,  0}}},
             
            new Rotation3D(){ matrix = new long[3,3]{{  1,  0,  0},
                                                     {  0,  0,  1},
                                                     {  0, -1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0, -1,  0},
                                                     {  0,  0,  1},
                                                     { -1,  0,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{ -1,  0,  0},
                                                     {  0,  0,  1},
                                                     {  0,  1,  0}}},

            new Rotation3D(){ matrix = new long[3,3]{{  0,  1,  0},
                                                     {  0,  0,  1},
                                                     {  1,  0,  0}}},
        };


        public static void Main(string[] args)
        {
            var scanners = new List<List<Point3D>>();
            var scanner = new List<Point3D>();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    scanners.Add(scanner);
                    scanner = new List<Point3D>();
                }
                else if (line.Contains("---"))
                {
                    continue;
                }
                else
                {
                    var p = line.Split(',').Select(long.Parse).ToArray();
                    scanner.Add(new Point3D(p[0],p[1],p[2]));
                }

            }

            var matched = new List<List<Point3D>>();
            matched.Add(scanners[0]);
            scanners.RemoveAt(0);
            while (scanners.Count > 0)
            {
                var new_matched = new List<List<Point3D>>();
                var to_delete = new List<List<Point3D>>();
                foreach (var scan in matched)
                {
                    foreach (var a in scanners)
                    {
                        var (ok, add) = IsOverlap(scan, a);
                        if (ok)
                        {
                            new_matched.Add(add.ToList());
                            to_delete.Add(a);
                        }
                    }
                }
                matched.AddRange(new_matched);
                scanners.RemoveAll(list => to_delete.Contains(list));

            }

            System.Console.WriteLine(matched.SelectMany(list => list).Distinct().Count());

        }

        private static (bool, IEnumerable<Point3D> msr2) IsOverlap(List<Point3D> s1, List<Point3D> s2)
        {
            foreach (var p1 in s1)
            {
                foreach (var p2 in s2)
                {
                    var trans_vaector = new long[1, 3] { { p1.x - p2.x, p1.y - p2.y, p1.z - p2.z } };
                    var rev_trans_vaector = new long[1, 3] { { -(p1.x), -(p1.y), -( p1.z) } };

                    var ms2 = s2.Select(point3D => point3D.Move(trans_vaector));
                    foreach (var rot in Rotations)
                    {
                        var msr2 = ms2.Select(point3D => point3D.Move(rev_trans_vaector)).Select(point3D => point3D.Rotate(rot)).Select(point3D => point3D.Move(p1.matrix));
                        if (s1.Intersect(msr2).Count() >= 12)
                            return (true, msr2);
                    }
                }
            }

            return (false, null);
        }
    }

    public class Point3D : IEquatable<Point3D>
    {
        public long x, y, z;
        public long[,] matrix => new long[1, 3] { { x, y, z } };

        public Point3D( long _x, long _y, long _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public Point3D Rotate(Rotation3D rot)
        {
            var a = MatrixOperations.Multiply(this.matrix, rot.matrix);
            return new Point3D(a[0, 0], a[0, 1], a[0, 2]);
        }
        public Point3D Move(long[,] vec)
        {
            return new Point3D(x+vec[0,0],y+vec[0,1],z+vec[0,2]);
        }

        public bool Equals(Point3D? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return x == other.x && y == other.y && z == other.z;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point3D)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }

    public class Translation
    {
        public long[,] matrix = new long[1, 3];
    }

    public class Rotation3D
    {
        public long[,] matrix = new long[3,3];
    }

    public class MatrixOperations
    {
        public static long[,] Multiply(long[,] matrix1, long[,] matrix2)
        {
            // cahing matrix lengths for better performance  
            var matrix1Rows = matrix1.GetLength(0);
            var matrix1Cols = matrix1.GetLength(1);
            var matrix2Rows = matrix2.GetLength(0);
            var matrix2Cols = matrix2.GetLength(1);

            // checking if product is defined  
            if (matrix1Cols != matrix2Rows)
                throw new InvalidOperationException
                    ("Product is undefined. n columns of first matrix must equal to n rows of second matrix");

            // creating the final product matrix  
            long[,] product = new long[matrix1Rows, matrix2Cols];

            // looping through matrix 1 rows  
            for (int matrix1_row = 0; matrix1_row < matrix1Rows; matrix1_row++)
            {
                // for each matrix 1 row, loop through matrix 2 columns  
                for (int matrix2_col = 0; matrix2_col < matrix2Cols; matrix2_col++)
                {
                    // loop through matrix 1 columns to calculate the dot product  
                    for (int matrix1_col = 0; matrix1_col < matrix1Cols; matrix1_col++)
                    {
                        product[matrix1_row, matrix2_col] +=
                            matrix1[matrix1_row, matrix1_col] *
                            matrix2[matrix1_col, matrix2_col];
                    }
                }
            }

            return product;
        }

    }

}
