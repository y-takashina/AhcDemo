using System;

namespace AhcDemo
{
    public abstract class Cluster
    {
        public abstract double DistanceTo(Cluster cluster);
        public abstract void Print(string indent = "");
    }

    public class Single : Cluster
    {
        public int Value;

        public Single(int value)
        {
            Value = value;
        }

        public override double DistanceTo(Cluster cluster)
        {
            if (cluster is Single single) return Math.Abs(Value - single.Value);
            var couple = (Couple) cluster;
            var left = couple.Left.DistanceTo(this);
            var right = couple.Right.DistanceTo(this);
            return left < right ? left : right;
        }

        public override void Print(string indent = "") => Console.WriteLine(indent + Value);
    }

    public class Couple : Cluster
    {
        public Cluster Left, Right;

        public Couple(Cluster left, Cluster right)
        {
            Left = left;
            Right = right;
        }

        public override double DistanceTo(Cluster cluster)
        {
            var left = Left.DistanceTo(cluster);
            var right = Right.DistanceTo(cluster);
            return left < right ? left : right;
        }

        public override void Print(string indent = "")
        {
            Left.Print(indent + "|-");
            Right.Print(indent.Replace('-', ' ').Replace('+', ' ') + "+-");
        }
    }
}