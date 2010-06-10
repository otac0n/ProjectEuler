namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// Three distinct points are plotted at random on a Cartesian plane, for which -1000 ≤ x, y ≤ 1000, such that a triangle is formed.
    /// 
    /// Consider the following two triangles:
    /// 
    /// A(-340,495), B(-153,-910), C(835,-947)
    /// 
    /// X(-175,41), Y(-421,-714), Z(574,-645)
    /// 
    /// It can be verified that triangle ABC contains the origin, whereas triangle XYZ does not.
    /// 
    /// Using triangles.txt, a 27K text file containing the co-ordinates of one thousand "random" triangles, find the number of triangles for which the interior contains the origin.
    /// </summary>
    [ProblemResource("triangles")]
    [Result(Name = "inside", Expected = "228")]
    public class Problem102 : Problem
    {
        public override string Solve(string resource)
        {
            var triangles = (from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                             let points = (from p in l.Split(',')
                                           select int.Parse(p)).ToArray()
                             let PointA = new Point(points[0], points[1])
                             let PointB = new Point(points[2], points[3])
                             let PointC = new Point(points[4], points[5])
                             select new
                             {
                                 Points = new[] { PointA, PointB, PointC },
                                 Vectors = new[]
                                 {
                                     new long[] { PointB.X - PointA.X, PointB.Y - PointA.Y },
                                     new long[] { PointC.X - PointB.X, PointC.Y - PointB.Y },
                                     new long[] { PointA.X - PointC.X, PointA.Y - PointC.Y },
                                 },
                             }).ToList();

            var count = 0;
            var inside = 0;
            foreach (var triangle in triangles)
            {
                var signs = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    var point = triangle.Points[i];
                    var vec = triangle.Vectors[i];

                    var a = point.X;
                    var b = point.Y;
                    var u = vec[0];
                    var v = vec[1];

                    var sign = Math.Sign(a * v - b * u) * Math.Sign(v * v + u * u);

                    signs[sign + 1]++;
                }

                count++;
                if (signs[0] + signs[1] == 3 ||
                    signs[2] + signs[1] == 3)
                {
                    inside++;
                }
            }

            return inside.ToString();
        }
    }
}
