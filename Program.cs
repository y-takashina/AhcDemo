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
            var clusters = data.Select(value => (Cluster) new Single(value)).ToList();
            while (clusters.Count != 1)
            {
                var min = double.MaxValue;
                var c1 = clusters.First();
                var c2 = clusters.Last();
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
            return clusters.First();
        }
    }
}