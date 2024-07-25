using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BiiSoft.PLinqs
{  
    public class ListPartitioner<TSource> : Partitioner<TSource>
    {
        IList<TSource> source;
        double rateOfIncrease = 0;

        public ListPartitioner(TSource[] source, double rate)
        {
            this.source = source;
            rateOfIncrease = rate;
        }

        public ListPartitioner(IList<TSource> source, double rate)
        {
            this.source = source;
            rateOfIncrease = rate;
        }

        public override IEnumerable<TSource> GetDynamicPartitions()
        {
            throw new NotImplementedException();
        }

        // Not consumable from Parallel.ForEach.
        public override bool SupportsDynamicPartitions => false;
        
        public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
        {
            List<IEnumerator<TSource>> _list = new List<IEnumerator<TSource>>();
            int end = 0;
            int start = 0;
            int[] nums = CalculatePartitions(partitionCount, source.Count);

            for (int i = 0; i < nums.Length; i++)
            {
                start = nums[i];
                if (i < nums.Length - 1)
                    end = nums[i + 1];
                else
                    end = source.Count;

                _list.Add(GetItemsForPartition(start, end));

                //// For demonstratation.
                //Console.WriteLine("start = {0} b (end) = {1}", start, end);
            }
            return _list;
        }
        /*
         *
         *
         *                                                               B
          // Model increasing workloads as a right triangle           /  |
             divided into equal areas along vertical lines.         / |  |
             Each partition  is taller and skinnier               /   |  |
             than the last.                                     / |   |  |
                                                              /   |   |  |
                                                            /     |   |  |
                                                          /  |    |   |  |
                                                        /    |    |   |  |
                                                A     /______|____|___|__| C
         */
        private int[] CalculatePartitions(int partitionCount, int sourceLength)
        {
            // Corresponds to the opposite side of angle A, which corresponds
            // to an index into the source array.
            int[] partitionLimits = new int[partitionCount];
            partitionLimits[0] = 0;

            // Represent total work as rectangle of source length times "most expensive element"
            // Note: RateOfIncrease can be factored out of equation.
            double totalWork = sourceLength * (sourceLength * rateOfIncrease);
            // Divide by two to get the triangle whose slope goes from zero on the left to "most"
            // on the right. Then divide by number of partitions to get area of each partition.
            totalWork /= 2;
            double partitionArea = totalWork / partitionCount;

            // Draw the next partitionLimit on the vertical coordinate that gives
            // an area of partitionArea * currentPartition.
            for (int i = 1; i < partitionLimits.Length; i++)
            {
                double area = partitionArea * i;

                // Solve for base given the area and the slope of the hypotenuse.
                partitionLimits[i] = (int)Math.Floor(Math.Sqrt((2 * area) / rateOfIncrease));
            }
            return partitionLimits;
        }

        IEnumerator<TSource> GetItemsForPartition(int start, int end)
        {
            //// For demonstration purpsoes. Each thread receives its own enumerator.
            //Console.WriteLine("called on thread {0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = start; i < end; i++)
                yield return source[i];
        }
    }
}
