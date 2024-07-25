using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.PLinqs
{  
    public static class PLinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
        {
            var length = source.Count();
           
            for (int i = 0; i < length; i += size)
            {
                yield return source.Skip(i).Take(size);
            }
        }

        public static IEnumerable<List<T>> Split<T>(this List<T> source, int size)
        {
            var length = source.Count;

            for (int i = 0; i < length; i += size)
            {
                yield return source.GetRange(i, Math.Min(size, length - i));
            }
        }

        public static IList<List<T>> ListSplit<T>(this List<T> source, int size)
        {
            var result = new List<List<T>>();
            var length = source.Count;

            for (int i = 0; i < length; i += size)
            {
                result.Add(source.GetRange(i, Math.Min(size, length - i)));
            }

            return result;
        }

        public static IList<List<T>> ListSplit<T>(this IEnumerable<T> source, int size)
        {
            var result = new List<List<T>>();
            var length = source.Count();

            for (int i = 0; i < length; i += size)
            {
                result.Add(new List<T>(source.Skip(i).Take(size)));
            }

            return result;
        }

        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int parts)
        {
            var length = source.Count();
            var size = (int)Math.Ceiling(length / (double)parts);

            for (int i = 0; i < parts; i++)
                yield return source.Skip(size * i).Take(size);
        }

        public static IEnumerable<List<T>> Partition<T>(this IList<T> source, int parts)
        {
            var length = source.Count;
            var size = (int)Math.Ceiling(length / (double)parts);

            for (int i = 0; i < parts; i++)
                yield return new List<T>(source.Skip(size * i).Take(size));
        }

        public static IList<List<T>> ListPartition<T>(this IList<T> source, int parts)
        {
            var result = new List<List<T>>();
            var length = source.Count;
            var size = (int)Math.Ceiling(length / (double)parts);

            for (int i = 0; i < parts; i++)
            {
                result.Add(new List<T>(source.Skip(size * i).Take(size)));
            }

            return result;       
        }

        public static IList<List<T>> ListPartition<T>(this IEnumerable<T> source, int parts)
        {
            var result = new List<List<T>>();
            var length = source.Count();
            var size = (int)Math.Ceiling(length / (double)parts);

            for (int i = 0; i < parts; i++)
            {
                var skip = size * i;
                var list = new List<T>(source.Skip(size * i).Take(size));
                result.Add(list);
            }

            return result;
        }

    }
}
