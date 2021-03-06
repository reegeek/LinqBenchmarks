﻿using BenchmarkDotNet.Attributes;
using NetFabric.Hyperlinq;
using StructLinq;

namespace LinqBenchmarks.ImmutableArray.Int32
{
    public class ImmutableArrayInt32Contains: ImmutableArrayInt32BenchmarkBase
    {
        int value = int.MaxValue;

        [Benchmark(Baseline = true)]
        public bool ForLoop()
        {
            var array = source;
            for (var index = 0; index < array.Length; index++)
            {
                var item = array[index];
                if (item == value)
                    return true;
            }
            return true;
        }

        [Benchmark]
        public bool ForeachLoop()
        {
            foreach (var item in source)
            {
                if (item == value)
                    return true;
            }
            return true;
        }

        [Benchmark]
        public bool Linq()
            => source
                .Contains(value);

        [Benchmark]
        public bool StructLinq()
            => source
                .ToStructEnumerable()
                .Contains(value);

        [Benchmark]
        public bool StructLinq_IFunction()
        {
            return source
                .ToStructEnumerable()
                .Contains(value, x => x);
        }

        [Benchmark]
        public bool Hyperlinq()
            => source
                .AsValueEnumerable()
                .Contains(value);

        [Benchmark]
        public bool Hyperlinq_SIMD()
            => source
                .AsValueEnumerable()
                .ContainsVector(value);
    }
}
