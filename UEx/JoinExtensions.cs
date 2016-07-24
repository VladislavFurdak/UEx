using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Uex
{
    public static class JoinExtensions
    {
        /// <summary>
        /// Perform an inner join of two sequences
        /// </summary>
        /// <typeparam name="T">Sequence element type</typeparam>
        /// <typeparam name="TJoinMemberType">Join field type</typeparam>
        /// <param name="sequence">Source sequence</param>
        /// <param name="sourceField">Join source sequence by this field</param>
        /// <param name="joindeSequence">Joined sequence</param>
        /// <param name="joinedField">Join second sequence by this field</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T,T>> InnerJoin<T, TJoinMemberType>(
            this IEnumerable<T> sequence,
            Expression<Func<T, TJoinMemberType>> sourceField,  
            IEnumerable<T> joindeSequence,
            Expression<Func<T, TJoinMemberType>> joinedField) 
            where TJoinMemberType : struct
        {
            var duplicatesJoined = joindeSequence.GroupByExpression(joinedField);

            return sequence
                .GroupByExpression(sourceField)
                .SelectMany(
                     x => x.Value.SelectMany(y => duplicatesJoined.Keys.Contains(x.Key) 
                                                ? duplicatesJoined[x.Key].Select(z => new Tuple<T, T>(y, z))
                                                : Enumerable.Empty<Tuple<T,T>>())
             ).ToList();
        }

        /// <summary>
        /// Perform a left join of two sequences, with source sequence as a base
        /// </summary>
        /// <typeparam name="T">Sequence element type</typeparam>
        /// <typeparam name="TJoinMemberType">Join field type</typeparam>
        /// <param name="sequence">Source sequence</param>
        /// <param name="sourceField">Join source sequence by this field</param>
        /// <param name="joindeSequence">Joined sequence</param>
        /// <param name="joinedField">Join second sequence by this field</param>
        public static IEnumerable<Tuple<T,T>> LeftJoin<T, TJoinMemberType>(
            this IEnumerable<T> sequence,
            Expression<Func<T, TJoinMemberType>> sourceField,
            IEnumerable<T> joindeSequence,
            Expression<Func<T, TJoinMemberType>> joinedField)
            where TJoinMemberType : struct
        {
            var duplicatesJoined = joindeSequence.GroupByExpression(joinedField);

            return sequence
                .GroupByExpression(sourceField)
                .SelectMany(
                     x => x.Value.SelectMany(y => duplicatesJoined.Keys.Contains(x.Key) 
                                                ? duplicatesJoined[x.Key].Select(z => new Tuple<T, T>(y, z)) 
                                                : Enumerable.Repeat(new Tuple<T, T>(y, default(T)), 1))
             ).ToList();
        }

        /// <summary>
        /// Perform a cross join of two sequences
        /// </summary>
        /// <typeparam name="T">Type of sequences element</typeparam>
        /// <param name="sequence">First sequence</param>
        /// <param name="joindeSequence">Second sequence</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T, T>> CrossJoin<T>(
              this IEnumerable<T> sequence,
              IEnumerable<T> joindeSequence
            )
        {
            return sequence.SelectMany(x => joindeSequence.Select(y => new Tuple<T, T>(x, y))).ToList();
        }
       
     }
}
