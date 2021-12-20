﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public struct GenericPosition2D<T> : IComparable<GenericPosition2D<T>> where T : notnull
    {
        public T x;
        public T y;

        public GenericPosition2D(GenericPosition2D<T> p)
        {
            x = p.x;
            y = p.y;
        }
        public GenericPosition2D(T x, T y)
        {
            this.x = x;
            this.y = y;
        }
        public int CompareTo(GenericPosition2D<T> p)    // Reading order
        {
            if (!EqualityComparer<T>.Default.Equals(y, p.y))
                return Comparer<T>.Default.Compare(y, p.y);
            else
                return Comparer<T>.Default.Compare(x, p.x);
        }
        public override bool Equals(Object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            return obj is GenericPosition2D<T> d && Equals(d);
        }
        public bool Equals(GenericPosition2D<T> p)
        {
            return EqualityComparer<T>.Default.Equals(x, p.x) && EqualityComparer<T>.Default.Equals(y, p.y);
        }
        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + x!.GetHashCode();
            hashCode = hashCode * -1521134295 + y!.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return p1.Equals(p2); }
        public static bool operator !=(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return !p1.Equals(p2); }
        public static bool operator <(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return p1.CompareTo(p2) < 0; }
        public static bool operator <=(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return p1.CompareTo(p2) <= 0; }
        public static bool operator >(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return p1.CompareTo(p2) > 0; }
        public static bool operator >=(GenericPosition2D<T> p1, GenericPosition2D<T> p2) { return p1.CompareTo(p2) >= 0; }
        public static GenericPosition2D<T> operator +(GenericPosition2D<T> p1, T k)
        {
            return p1 + new GenericPosition2D<T>(k, k);
        }
        public static GenericPosition2D<T> operator +(GenericPosition2D<T> p1, GenericPosition2D<T> p2)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Add(p.x, p2.x);
            p.y = Extensions.Add(p.y, p2.y);
            return p;
        }
        public static GenericPosition2D<T> operator -(GenericPosition2D<T> p1, T k)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Subtract(p.x, k);
            p.y = Extensions.Subtract(p.y, k);
            return p;
        }
        public static GenericPosition2D<T> operator -(GenericPosition2D<T> p1, GenericPosition2D<T> p2)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Subtract(p.x, p2.x);
            p.y = Extensions.Subtract(p.y, p2.y);
            return p;
        }
        public static GenericPosition2D<T> operator *(GenericPosition2D<T> p1, T k)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Multiply(p.x, k);
            p.y = Extensions.Multiply(p.y, k);
            return p;
        }
        public static GenericPosition2D<T> operator *(GenericPosition2D<T> p1, GenericPosition2D<T> p2)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Multiply(p.x, p2.x);
            p.y = Extensions.Multiply(p.y, p2.y);
            return p;
        }
        public static GenericPosition2D<T> operator /(GenericPosition2D<T> p1, T k)
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(p1);
            p.x = Extensions.Divide(p.x, k);
            p.y = Extensions.Divide(p.y, k);
            return p;
        }
        public T ManhattanDistance(GenericPosition2D<T> p = new GenericPosition2D<T>())
        {
            return Extensions.Add(
                Extensions.Abs(Extensions.Subtract(x, p.x!)),
                Extensions.Abs(Extensions.Subtract(y, p.y!))
                );
        }
        public GenericPosition2D<T> SwitchXY()
        {
            return new GenericPosition2D<T>(y, x);
        }
        // Rotates pos n steps clock-wize around center
        public static GenericPosition2D<T> Rotate4Steps(GenericPosition2D<T> pos, int n, GenericPosition2D<T> center = new GenericPosition2D<T>())
        {
            GenericPosition2D<T> p = new GenericPosition2D<T>(pos - center);
            GenericPosition2D<T> r = new GenericPosition2D<T>(p);
            n = Utils.Modulo(n, 4);
            if (n == 1)
            {
                r.x = p.y;
                r.y = Extensions.Subtract(default!, p.x);
            }
            else if (n == 2)
            {
                r.x = Extensions.Subtract(default!, p.x);
                r.y = Extensions.Subtract(default!, p.y);
            }
            else if (n == 3)
            {
                r.x = Extensions.Subtract(default!, p.y);
                r.y = p.x;
            }
            return r + center;
        }
    }

    public struct GenericPosition3D<T> : IComparable<GenericPosition3D<T>> where T : notnull
    {
        public T x;
        public T y;
        public T z;

        public GenericPosition3D(GenericPosition3D<T> p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }
        public GenericPosition3D(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public int CompareTo(GenericPosition3D<T> p)
        {
            if (!EqualityComparer<T>.Default.Equals(z, p.z))
                return Comparer<T>.Default.Compare(z, p.z);
            else if (!EqualityComparer<T>.Default.Equals(y, p.y))
                return Comparer<T>.Default.Compare(y, p.y);
            else
                return Comparer<T>.Default.Compare(x, p.x);
        }
        public override bool Equals(Object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
                return obj is GenericPosition2D<T> d && Equals(d);
        }
        public bool Equals(GenericPosition3D<T> p)
        {
            return
                EqualityComparer<T>.Default.Equals(x, p.x) &&
                EqualityComparer<T>.Default.Equals(y, p.y) &&
                EqualityComparer<T>.Default.Equals(z, p.z);
        }
        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + x!.GetHashCode();
            hashCode = hashCode * -1521134295 + y!.GetHashCode();
            hashCode = hashCode * -1521134295 + z!.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return p1.Equals(p2); }
        public static bool operator !=(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return !p1.Equals(p2); }
        public static bool operator <(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return p1.CompareTo(p2) < 0; }
        public static bool operator <=(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return p1.CompareTo(p2) <= 0; }
        public static bool operator >(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return p1.CompareTo(p2) > 0; }
        public static bool operator >=(GenericPosition3D<T> p1, GenericPosition3D<T> p2) { return p1.CompareTo(p2) >= 0; }
        public static GenericPosition3D<T> operator +(GenericPosition3D<T> p1, T k)
        {
            return p1 + new GenericPosition3D<T>(k, k, k);
        }
        public static GenericPosition3D<T> operator +(GenericPosition3D<T> p1, GenericPosition3D<T> p2)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Add(p.x, p2.x);
            p.y = Extensions.Add(p.y, p2.y);
            p.z = Extensions.Add(p.z, p2.z);
            return p;
        }
        public static GenericPosition3D<T> operator -(GenericPosition3D<T> p1, T k)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Subtract(p.x, k);
            p.y = Extensions.Subtract(p.y, k);
            p.z = Extensions.Subtract(p.z, k);
            return p;
        }
        public static GenericPosition3D<T> operator -(GenericPosition3D<T> p1, GenericPosition3D<T> p2)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Subtract(p.x, p2.x);
            p.y = Extensions.Subtract(p.y, p2.y);
            p.z = Extensions.Subtract(p.z, p2.z);
            return p;
        }
        public static GenericPosition3D<T> operator *(GenericPosition3D<T> p1, T k)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Multiply(p.x, k);
            p.y = Extensions.Multiply(p.y, k);
            p.z = Extensions.Multiply(p.z, k);
            return p;
        }
        public static GenericPosition3D<T> operator *(GenericPosition3D<T> p1, GenericPosition3D<T> p2)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Multiply(p.x, p2.x);
            p.y = Extensions.Multiply(p.y, p2.y);
            p.z = Extensions.Multiply(p.z, p2.z);
            return p;
        }
        public static GenericPosition3D<T> operator /(GenericPosition3D<T> p1, T k)
        {
            GenericPosition3D<T> p = new GenericPosition3D<T>(p1);
            p.x = Extensions.Divide(p.x, k);
            p.y = Extensions.Divide(p.y, k);
            p.z = Extensions.Divide(p.z, k);
            return p;
        }
        public T ManhattanDistance(GenericPosition3D<T> p = new GenericPosition3D<T>())
        {
            return Extensions.Add(
                Extensions.Abs(Extensions.Subtract(x, p.x!)),
                Extensions.Abs(Extensions.Subtract(y, p.y!)),
                Extensions.Abs(Extensions.Subtract(z, p.z!))
                );
        }
    }

    public class Map : IEquatable<Map>
    {
        public int width;
        public int height;
        public GenericPosition2D<int> pos;
        public char[,] data;

        public Map(int w, int h, char fill, GenericPosition2D<int> p = new GenericPosition2D<int>())
        {
            width = w;
            height = h;
            pos = p;
            data = new char[w, h];
            for (int i = 0; i < w * h; i++)
            {
                data[i % w, i / w] = fill;
            }
        }

        public Map(Map m)
        {
            width = m.width;
            height = m.height;
            pos = m.pos;
            data = new char[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    data[x, y] = m.data[x, y];
        }

        public static Map Build(IList<string> list)
        {
            int w = list[0].Length;
            int h = list.Count;
            Map m = new Map(w, h, ' ');
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    m.data[x, y] = list[y][x];
            return m;
        }

        public char this[GenericPosition2D<int> p]
        {
            get
            {
                return data[p.x, p.y];
            }
            set
            {
                data[p.x, p.y] = value;
            }
        }

        public bool HasPosition(GenericPosition2D<int> p)
        {
            return p.x >= 0 && p.x < width && p.y >= 0 && p.y < height;
        }

        public List<GenericPosition2D<int>> Positions(GenericPosition2D<int> p0 = new GenericPosition2D<int>(), int w0 = 0, int h0 = 0)
        {
            int w = (w0 < 1) ? width - p0.x : w0;
            int h = (h0 < 1) ? height - p0.y : h0;
            var positions = new List<GenericPosition2D<int>>(w * h);
            for (int y = p0.y; y < p0.y + h; y++)
                for (int x = p0.x; x < p0.x + w; x++)
                    positions.Add(new GenericPosition2D<int>(x, y));
            return positions;
        }

        public void Expand(int n, char fill) { Expand(n, n, n, n, fill); }
        public void Expand(int top, int right, int bottom, int left, char fill)
        {
            int w = left + right + width;
            int h = top + bottom + height;
            GenericPosition2D<int> s = new GenericPosition2D<int>(pos.x + left, pos.y + top);
            Map m = new Map(w, h, fill, s);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    m.data[x + left, y + top] = data[x, y];
            width = m.width;
            height = m.height;
            pos = m.pos;
            data = m.data;
        }

        public string PrintToString()
        {
            string s = "";
            for (int y = 0; y < height; y++)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < width; x++)
                {
                    sb.Append(data[x, y]);
                }
                s += sb.ToString() + Environment.NewLine;
            }
            return s;
        }

        public void Print()
        {
            Console.WriteLine(PrintToString());
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
                return obj is Map d && Equals(d);
        }

        public bool Equals(Map? other)
        {
            return !(other is null) &&
                   width == other.width &&
                   height == other.height &&
                   data.Cast<char>().SequenceEqual(other.data.Cast<char>());
            //EqualityComparer<char[,]>.Default.Equals(data, other.data);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(width, height, data);
        }

        public static bool operator ==(Map map1, Map map2)
        {
            return EqualityComparer<Map>.Default.Equals(map1, map2);
        }

        public static bool operator !=(Map map1, Map map2)
        {
            return !(map1 == map2);
        }
    }

    public static class CoordsRC
    {
        public static readonly GenericPosition2D<int> goUpLeft = new GenericPosition2D<int>(-1, -1);
        public static readonly GenericPosition2D<int> goUp = new GenericPosition2D<int>(0, -1);
        public static readonly GenericPosition2D<int> goUpRight = new GenericPosition2D<int>(1, -1);
        public static readonly GenericPosition2D<int> goRight = new GenericPosition2D<int>(1, 0);
        public static readonly GenericPosition2D<int> goDownRight = new GenericPosition2D<int>(1, 1);
        public static readonly GenericPosition2D<int> goDown = new GenericPosition2D<int>(0, 1);
        public static readonly GenericPosition2D<int> goDownLeft = new GenericPosition2D<int>(-1, 1);
        public static readonly GenericPosition2D<int> goLeft = new GenericPosition2D<int>(-1, 0);
        public static readonly List<GenericPosition2D<int>> directions4 = new List<GenericPosition2D<int>>()
        {
            goUp, goRight, goDown, goLeft
        };
        public static readonly List<GenericPosition2D<int>> directions8 = new List<GenericPosition2D<int>>()
        {
            goUpLeft, goUp, goUpRight, goRight, goDownRight, goDown, goDownLeft, goLeft
        };
        public static List<GenericPosition2D<int>> Neighbours4(GenericPosition2D<int> p) =>
            CoordsRC.directions4.Select(x => p + x).ToList();
        public static List<GenericPosition2D<int>> Neighbours8(GenericPosition2D<int> p) =>
            CoordsRC.directions8.Select(x => p + x).ToList();

        public static readonly List<GenericPosition2D<int>> d8 = new List<GenericPosition2D<int>>()
        {
            goUpLeft, goUp, goUpRight, goLeft, goRight, goDownLeft, goDown, goDownRight
        };
        public static List<GenericPosition2D<int>> N8(GenericPosition2D<int> p) =>
            CoordsRC.d8.Select(x => p + x).ToList();
    }

    public static class CoordsXY
    {
        public static readonly GenericPosition2D<int> goUpLeft = new GenericPosition2D<int>(-1, 1);
        public static readonly GenericPosition2D<int> goUp = new GenericPosition2D<int>(0, 1);
        public static readonly GenericPosition2D<int> goUpRight = new GenericPosition2D<int>(1, 1);
        public static readonly GenericPosition2D<int> goRight = new GenericPosition2D<int>(1, 0);
        public static readonly GenericPosition2D<int> goDownRight = new GenericPosition2D<int>(1, -1);
        public static readonly GenericPosition2D<int> goDown = new GenericPosition2D<int>(0, -1);
        public static readonly GenericPosition2D<int> goDownLeft = new GenericPosition2D<int>(-1, -1);
        public static readonly GenericPosition2D<int> goLeft = new GenericPosition2D<int>(-1, 0);
        public static readonly List<GenericPosition2D<int>> directions4 = new List<GenericPosition2D<int>>()
        {
            goUp, goRight, goDown, goLeft
        };
        public static readonly List<GenericPosition2D<int>> directions8 = new List<GenericPosition2D<int>>()
        {
            goUpLeft, goUp, goUpRight, goRight, goDownRight, goDown, goDownLeft, goLeft
        };
        public static List<GenericPosition2D<int>> Neighbours4(GenericPosition2D<int> p) =>
            CoordsXY.directions4.Select(x => p + x).ToList();
        public static List<GenericPosition2D<int>> Neighbours8(GenericPosition2D<int> p) =>
            CoordsXY.directions8.Select(x => p + x).ToList();
    }

    public static class CoordsHex
    {
        // Numbering system for the hexagonal tiles:
        //
        //  0,0     2,0     4,0     6,0
        //      1,1     3,1     5,1
        //  0,2     2,2     4,2     6,2
        //      1,3     3,3     5,3
        //  0,4     2,4     4,4     6,4
        //      1,5     3,5     5,5
        //
        // - Vertical neighbors have same x and y +- 2
        // - Horizontal neighbors have same x +- 2 and same y
        // - Diagonal neighbors have x +- 1 and y +- 1
        // 
        public static readonly Dictionary<string, GenericPosition2D<int>> directionsHigh = new Dictionary<string, GenericPosition2D<int>>()
        {
            { "n", new GenericPosition2D<int>(0, -2) },
            { "ne", new GenericPosition2D<int>(1, -1) },
            { "se", new GenericPosition2D<int>(1, 1) },
            { "s", new GenericPosition2D<int>(0, 2) },
            { "sw", new GenericPosition2D<int>(-1, 1) },
            { "nw", new GenericPosition2D<int>(-1, -1) },
        };
        public static readonly Dictionary<string, GenericPosition2D<int>> directionsWide = new Dictionary<string, GenericPosition2D<int>>()
        {
            { "ne", new GenericPosition2D<int>(1, -1) },
            { "e", new GenericPosition2D<int>(2, 0) },
            { "se", new GenericPosition2D<int>(1, 1) },
            { "sw", new GenericPosition2D<int>(-1, 1) },
            { "w", new GenericPosition2D<int>(-2, 0) },
            { "nw", new GenericPosition2D<int>(-1, -1) },
        };
    }

    public static class Utils
    {
        // Modulo i.e. mod (instead of the % operator)
        public static int Modulo(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        // Createst Common Factor i.e. Createst Common Divisor (GCD)
        public static long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Least Common Multiple i.e. Lowest Common Denominator (LCD)
        public static long LCM(long a, long b)
        {
            return (a / GCF(a, b)) * b;
        }

        // g = ax + by = gcd(a, b)
        public static (long g, long x, long y) EGCD(long a, long b)
        {
            if (a == 0)
                return (b, 0, 1);
            else
            {
                (long g, long y, long x) = EGCD(b % a, a);
                return (g, x - (b / a) * y, y);
            }
        }

        public static long ModInverse(long a, long m)
        {
            (long g, long x, long y) = EGCD(a, m);
            if (g != 1)
                throw new ArgumentOutOfRangeException();
            return x % m;
        }

        // Chinese Remainder Theorem (CRT)
        public static long CRT(IList<int> num, IList<int> rem)
        {
            long prod = num.Aggregate(1L, (a, b) => a * b);
            long sum = 0;
            for (int i = 0; i < num.Count; i++)
            {
                long p = prod / num[i];
                sum += rem[i] * Utils.ModInverse(p, num[i]) * p;
            }
            return sum % prod;
        }
    }

    public static class Algorithms
    {
        public static List<List<T>> HeapPermutation<T>(IList<T> a)
        {
            List<List<T>> result = new List<List<T>>();
            void Swap(ref List<T> b, int i1, int i2)
            {
                T temp = b[i1];
                b[i1] = b[i2];
                b[i2] = temp;
            }
            void Permute(ref List<T> b, int size)
            {
                if (size == 1)
                {
                    result.Add(new List<T>(b));
                }
                else
                {
                    for (int i = 0; i < size - 1; i++)
                    {
                        Permute(ref b, size - 1);
                        Swap(ref b, (size % 2 == 0) ? i : 0, size - 1);
                    }
                    Permute(ref b, size - 1);
                }
            }
            List<T> copy = new List<T>(a);
            Permute(ref copy, copy.Count);
            return result;
        }

        public static List<List<T>> GetCombinations<T>(IList<T> input)
        {
            return GetCombinations(input, input.Count);
        }
        public static List<List<T>> GetCombinations<T>(IList<T> input, int maxLength)
        {
            List<List<T>> results = new List<List<T>>();
            for (int i = 0; i < input.Count; i++)
            {
                List<T> current = new List<T>() { input[i] };
                int size = results.Count;
                for (int r = 0; r < size; r++)
                {
                    List<T> list = current.Concat(results[r]).ToList();
                    if (list.Count <= maxLength)
                        results.Add(list);
                }
                results.Add(current);
            }
            return results;
        }
    }

    public static class Extensions
    {
        public static T Add<T>(T number1, T number2) where T : notnull
        {
            dynamic a = number1;
            dynamic b = number2;
            return a + b;
        }
        public static T Add<T>(T number1, T number2, T number3) where T: notnull
        {
            dynamic a = number1;
            dynamic b = number2;
            dynamic c = number3;
            return a + b + c;
        }
        public static T Subtract<T>(T number1, T number2) where T : notnull
        {
            dynamic a = number1;
            dynamic b = number2;
            return a - b;
        }
        public static T Multiply<T>(T number1, T number2) where T : notnull
        {
            dynamic a = number1;
            dynamic b = number2;
            return a * b;
        }
        public static T Divide<T>(T number1, T number2) where T : notnull
        {
            dynamic a = number1;
            dynamic b = number2;
            return a / b;
        }
        public static T Abs<T>(T number) where T : notnull
        {
            dynamic a = number;
            return Math.Abs(a);
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static IEnumerable<GenericPosition2D<T>> Flip<T>(this IEnumerable<GenericPosition2D<T>> elements, bool enable) where T : notnull
        {
            return enable ? elements.Select(p => p.SwitchXY()) : elements;
        }

        public static void Inc<T, U>(this Dictionary<T, U> dictionary, T key, U value) where T : notnull where U : notnull
        {
#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
            dictionary[key] = Add(dictionary.GetValueOrDefault(key, default!), value!)!;
#pragma warning restore CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'notnull' constraint.
        }

        public static Dictionary<T, int> Counter<T>(this IEnumerable<T> elements) where T : notnull
        {
            return elements.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }
        public static Dictionary<T, long> CounterLong<T>(this IEnumerable<T> elements) where T : notnull
        {
            return elements.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
        }
    }

    public static class ArrayExt
    {
        public static void Deconstruct<T>(this T[] s, out T a0)
        {
            a0 = s[0];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1)
        {
            a0 = s[0]; a1 = s[1];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2, out T a3)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2]; a3 = s[3];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2, out T a3, out T a4)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2]; a3 = s[3]; a4 = s[4];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2, out T a3, out T a4, out T a5)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2]; a3 = s[3]; a4 = s[4]; a5 = s[5];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2, out T a3, out T a4, out T a5, out T a6)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2]; a3 = s[3]; a4 = s[4]; a5 = s[5]; a6 = s[6];
        }
        public static void Deconstruct<T>(this T[] s, out T a0, out T a1, out T a2, out T a3, out T a4, out T a5, out T a6, out T a7)
        {
            a0 = s[0]; a1 = s[1]; a2 = s[2]; a3 = s[3]; a4 = s[4]; a5 = s[5]; a6 = s[6]; a7 = s[7];
        }
    }

    public struct Range<T> where T : IComparable<T>
    {
        public T lo { get; set; }
        public T hi { get; set; }

        public override string ToString() => string.Format("[{0} - {1}]", lo, hi);
        public bool IsValid() => lo.CompareTo(hi) <= 0;
        public bool Contains(T value) => (lo.CompareTo(value) <= 0) && (value.CompareTo(hi) <= 0);
        public bool IsInside(Range<T> range) =>
            IsValid() && range.IsValid() && range.Contains(lo) && range.Contains(hi);
        public bool Contains(Range<T> range) =>
            IsValid() && range.IsValid() && this.Contains(range.lo) && this.Contains(range.hi);
    }

    public static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next! ?? current.List!.First!;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            return current.Previous! ?? current.List!.Last!;
        }
    }

    public static class Graph
    {
        public class Node<T>
        {
            public T t;
            public HashSet<Node<T>> edges;
            public Node(T tp = default!)
            {
                t = tp;
                edges = new HashSet<Node<T>>();
            }
        };

        public static HashSet<T> Union<T>(HashSet<T> a, HashSet<T> b)
        {
            HashSet<T> c = new HashSet<T>(a);
            c.UnionWith(b);
            return c;
        }

        public static HashSet<T> Intersection<T>(HashSet<T> a, HashSet<T> b)
        {
            HashSet<T> c = new HashSet<T>(a);
            c.IntersectWith(b);
            return c;
        }

        public static HashSet<T> Difference<T>(HashSet<T> a, HashSet<T> b)
        {
            HashSet<T> c = new HashSet<T>(a);
            c.ExceptWith(b);
            return c;
        }

        public static void BronKerbosch1<T>(HashSet<Node<T>> R, HashSet<Node<T>> P, HashSet<Node<T>> X, ref List<HashSet<Node<T>>> cliques)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                cliques.Add(R);
                //Console.WriteLine("BK1: Found clique with {0} nodes.", R.Count);
            }
            else
            {
                HashSet<Node<T>> Pc = new HashSet<Node<T>>(P.OrderByDescending(x => x.edges.Count));
                foreach (Node<T> n in Pc)
                {
                    HashSet<Node<T>> s = new HashSet<Node<T>>() { n };
                    BronKerbosch1(Union<Node<T>>(R, s), Intersection<Node<T>>(P, n.edges), Intersection<Node<T>>(X, n.edges), ref cliques);
                    P = Difference<Node<T>>(P, s);
                    X = Union<Node<T>>(X, s);
                }
            }
        }

        public static void BronKerbosch2<T>(HashSet<Node<T>> R, HashSet<Node<T>> P, HashSet<Node<T>> X, ref List<HashSet<Node<T>>> cliques)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                cliques.Add(R);
                //Console.WriteLine("BK2: Found clique with {0} nodes.", R.Count);
            }
            else
            {
                Node<T> pivot = Union<Node<T>>(P, X).OrderBy(x => x.edges.Count).Last();
                HashSet<Node<T>> Pu = Difference<Node<T>>(P, pivot.edges);
                foreach (Node<T> n in Pu)
                {
                    HashSet<Node<T>> s = new HashSet<Node<T>>() { n };
                    BronKerbosch2(Union<Node<T>>(R, s), Intersection<Node<T>>(P, n.edges), Intersection<Node<T>>(X, n.edges), ref cliques);
                    P = Difference<Node<T>>(P, s);
                    X = Union<Node<T>>(X, s);
                }
            }
        }
    }

    public static class Tree
    {
        public class Node<T>
        {
            public Node<T>? parent;
            public T t;
            public HashSet<Node<T>> children;
            public Node(T v = default!, Node<T>? p = default)
            {
                t = v;
                parent = p;
                children = new HashSet<Node<T>>();
            }
        };
        public class BinNode<T>
        {
            public BinNode<T>? parent;
            public T t;
            public BinNode<T>? left;
            public BinNode<T>? right;
            public BinNode(T v, BinNode<T>? p)
            {
                t = v;
                parent = p;
                left = null;
                right = null;
            }
        };
    }

    public static class ReadInput
    {
        public static string GetPath(string nsname, string file)
        {
            return Path.Combine(AppDomain.CurrentDomain!.BaseDirectory!, @"..\..\..\..\" + nsname + "\\" + file);
        }

        public static List<int> Ints(string day, string file, char delimiter = ',')
        {
            var list = new List<int>();
            foreach (var line in File.ReadAllLines(ReadInput.GetPath(day, file)))
                list.AddRange(line.Split(delimiter).Select(int.Parse));
            return list;
        }

        public static List<long> Longs(string day, string file, char delimiter = ',')
        {
            var list = new List<long>();
            foreach (var line in File.ReadAllLines(ReadInput.GetPath(day, file)))
                list.AddRange(line.Split(delimiter).Select(long.Parse));
            return list;
        }

        public static List<string> Strings(string day, string file)
        {
            return File.ReadAllLines(ReadInput.GetPath(day, file)).ToList();
        }

        public static List<List<string>> StringLists(string day, string file, string delimiter = " ")
        {
            return Strings(day, file).Select(x => x.Split(delimiter).ToList()).ToList();
        }
    }
    public static class Extract
    {
        public static int[] Ints(string s, int base_ = 10) => Longs(s, base_).Select(x => (int)x).ToArray();
        public static long[] Longs(string s, int base_ = 10)
        {
            var validChars = "+-0123456789abcdef".Substring(0, base_ + 2);
            s = new string(s.ToLower().Select(c => validChars.Contains(c) ? c : ' ').ToArray());
            var v = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return v.Select(x => Convert.ToInt64(x, base_)).ToArray();
        }
    }
    public static class Aoc
    {
        static Object FixOutput(Object x)
        {
            string xStr = x.ToString() ?? "null";
            if (xStr != "0")
                TextCopy.ClipboardService.SetText(xStr);
            return (x is string && xStr.Contains(Environment.NewLine)) ? Environment.NewLine + x : x;
        }
        public static void Execute(string day, Func<string, Object> PartA, Func<string, Object> PartB, bool example = false)
        {
            Console.WriteLine("AoC 2021 - " + day + ":");
            var w = System.Diagnostics.Stopwatch.StartNew();
            string file = example ? "example.txt" : "input.txt";
            Object a = FixOutput(PartA(file));
            Console.WriteLine("Part A - Result is: {0}", a);
            Object b = FixOutput(PartB(file));
            Console.WriteLine("Part B - Result is: {0}", b);
            w.Stop();
            Console.WriteLine("[Execution took {0} ms]", w.ElapsedMilliseconds);
        }
        public static void Execute(string day, Func<string, (Object, Object)> Puzzle, bool example = false)
        {
            Console.WriteLine("AoC 2021 - " + day + ":");
            var w = System.Diagnostics.Stopwatch.StartNew();
            string file = example ? "example.txt" : "input.txt";
            (var a, var b) = Puzzle(file);
            a = FixOutput(a);
            b = FixOutput(b);
            Console.WriteLine("Puzzle A: {0}", a);
            Console.WriteLine("Puzzle B: {0}", b);
            w.Stop();
            Console.WriteLine("[Execution took {0} ms]", w.ElapsedMilliseconds);
        }

        public static string Day(System.Reflection.MethodBase mb)
        {
            return (mb == null) ? "null" : mb!.ReflectedType!.FullName!.Split('.').Last().ToLower();
        }
    }
}
