using System;
using System.Collections.Generic;
using System.Linq;

namespace AhcDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = new[] {9, 3, 5, 8, -1, 100, 72, 99};
            var c = AggregativeHierarchicalClustering(stream);
            c.Print();
        }

        public static Cluster AggregativeHierarchicalClustering(IEnumerable<int> data)
        {
            var clusters = data.Select(v => new Single(v)).Cast<Cluster>().ToList();
            while (clusters.Count != 1)
            {
                var min = double.MaxValue;
                Cluster c1 = null, c2 = null;
                for (var i = 0; i < clusters.Count; i++)
                {
                    for (var j = i + 1; j < clusters.Count; j++)
                    {
                        var d = clusters[i].DistanceTo(clusters[j]);
                        if (d < min)
                        {
                            min = d;
                            c1 = clusters[i];
                            c2 = clusters[j];
                        }
                    }
                }
                clusters.Add(new Couple(c1, c2));
                clusters.Remove(c1);
                clusters.Remove(c2);
            }
            return clusters.Single();
        }
    }
}