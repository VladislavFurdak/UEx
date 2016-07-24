using System;
using System.Collections.Generic;
using System.Linq;

namespace Uex
{ 
    public static class MergeExtensions
    {
        /// <summary>
        /// Merge sequence with sequence and make decision about results of merge
        /// </summary>
        /// <typeparam name="TInput1">Sequence one type</typeparam>
        /// <typeparam name="TInput2">Sequence two type</typeparam>
        /// <typeparam name="TOutput">Type of result</typeparam>
        /// <param name="source">source sequence</param>
        /// <param name="mergewith">destination sequence</param>
        /// <param name="Comparer">Delegate of comparation of TInput1 and TInput2 </param>
        /// <param name="SourceOnly">Action if element only in first sequence</param>
        /// <param name="SecondOnly">Acion if element only in second sequence</param>
        /// <param name="Both">Action if element in both sequences</param>
        /// <returns></returns>
        public static IEnumerable<TOutput> Merge<TInput1, TInput2, TOutput>(
            this IEnumerable<TInput1> source,
            IEnumerable<TInput2> mergewith,
            Func<TInput1, TInput2, bool> Comparer,
            Func<TInput1, TOutput> SourceOnly,
            Func<TInput2, TOutput> SecondOnly,
            Func<TInput1, TInput2, TOutput> Both
            )
        {
            TOutput defaultOutput = default(TOutput);

            IEnumerable<TOutput> onlySource;
            IEnumerable<TOutput> onlyDest;
            IEnumerable<TOutput> both;

            both = source.SelectMany(x => mergewith.Select(y => Comparer(x, y) ? Both(x, y) : defaultOutput))
                                    .Where(x => x != null && !x.Equals(defaultOutput));

            onlySource = source.Select(x => !mergewith.Any(y => Comparer(x, y)) ? SourceOnly(x) : defaultOutput)
                                     .Where(x => x != null && !x.Equals(defaultOutput));

            onlyDest = mergewith.Select(x => !source.Any(y => Comparer(y, x)) ? SecondOnly(x) : defaultOutput)
                                     .Where(x => x != null && !x.Equals(defaultOutput));

            return 
                both
                .Union(onlySource)
                .Union(onlyDest)
                .ToList();
        }
    }
}
