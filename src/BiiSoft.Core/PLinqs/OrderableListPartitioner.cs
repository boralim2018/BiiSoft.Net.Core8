using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace BiiSoft.PLinqs
{
    public class OrderableListPartitioner<TSource> : OrderablePartitioner<TSource>
    {
        private readonly IList<TSource> m_input;

        // Must override to return true.
        public override bool SupportsDynamicPartitions => true;

        public OrderableListPartitioner(TSource[] input) : base(true, false, true) =>
           m_input = input;

        public OrderableListPartitioner(IList<TSource> input) : base(true, false, true) => 
            m_input = input;

        public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
        {
            var dynamicPartitions = GetOrderableDynamicPartitions();
            var partitions =
                new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];

            for (int i = 0; i < partitionCount; i++)
            {
                partitions[i] = dynamicPartitions.GetEnumerator();
            }
            return partitions;
        }

        public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions() => 
            new ListDynamicPartitions(m_input);

        private class ListDynamicPartitions : IEnumerable<KeyValuePair<long, TSource>>
        {
            private IList<TSource> m_input;
            private int m_pos = 0;

            internal ListDynamicPartitions(TSource[] input) =>
              m_input = input;

            internal ListDynamicPartitions(IList<TSource> input) => 
                m_input = input;

            public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
            {
                while (true)
                {
                    // Each task gets the next item in the list. The index is
                    // incremented in a thread-safe manner to avoid races.
                    int elemIndex = Interlocked.Increment(ref m_pos) - 1;

                    if (elemIndex >= m_input.Count)
                    {
                        yield break;
                    }

                    yield return new KeyValuePair<long, TSource>(
                        elemIndex, m_input[elemIndex]);
                }
            }

            IEnumerator IEnumerable.GetEnumerator() =>
                ((IEnumerable<KeyValuePair<long, TSource>>)this).GetEnumerator();
        }
    }
}
