using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;

namespace KDTrees.Utility
{
    public static class Extensions
    {
        /// <summary>
        ///  Returns a copy of formatString in which the format items have been replaced by the string
        ///  representation of the corresponding objects in args.
        /// </summary>
        public static string Format(this string formatString, params object[] args)
        {
            return string.Format(formatString, args);
        }

        /// <summary>
        /// Moves all of the elements out of *this* list and returns them as a result.
        /// </summary>
        /// <returns> all of the elements of the original list</returns>
        public static List<T> TakeAll<T>(this IList<T> list)
        {
            return list.TakeIf(_ => true);
        }

        /// <summary>
        /// Moves the elements for which the condition is satisfied
        /// out of *this* list and returns them as a result.
        /// </summary>
        /// <returns>the elements that satisfy the provided condition</returns>
        public static List<T> TakeIf<T>(this IList<T> list, Predicate<T> condition)
        {
            var result = new List<T>();

            for (int i = 0; i < list.Count; i++)
            {
                if (condition(list[i]))
                {
                    result.Add(list[i]);
                    list.RemoveAt(i--);
                }
            }

            return result;
        }

        /// <summary>
        /// Removes all elements that satisfy the condition from *this* list.
        /// </summary>
        public static void RemoveIf<T>(this IList<T> list, Predicate<T> condition)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (condition(list[i]))
                    list.RemoveAt(i--);
            }
        }

        /// <summary>
        /// Removes all elements that satisfy the condition from *this* list.
        /// </summary>
        public static void RemoveIf(this IList list, Predicate<object> condition)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (condition(list[i]))
                    list.RemoveAt(i--);
            }
        }

        /// <summary>
        /// Removes all elements that are instances of type T (or derived from it).
        /// </summary>
        public static void RemoveElementsOfType<T>(this IList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is T)
                    list.RemoveAt(i--);
            }
        }

        /// <summary>
        /// Checks whether the specified lists *a* and *b* contain equal elements
        /// determined by the IEquatable<T>.Equals().
        /// </summary>
        public static bool IListEquals<T>(IList<T> a, IList<T> b) where T : IEquatable<T>
        {
            return IListEquals(a, b, (x, y) => x.Equals(y));
        }

        /// <summary>
        /// Checks whether the specified lists *a* and *b* contain equal elements
        /// determined by the provided comparer
        /// </summary>
        /// <param name="comparer">The function which takes two elements from the
        /// lists and returns true if they are equal.
        /// </param>
        public static bool IListEquals<T>(IList<T> a, IList<T> b, Func<T, T, bool> comparer)
        {
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
                if (!comparer(a[i], b[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Checks whether the specified sequences *a* and *b* contain equal elements
        /// determined by the IEquatable<T>.Equals().
        /// </summary>
        public static bool IEnumerableEquals<T>(IEnumerable<T> a, IEnumerable<T> b) where T: IEquatable<T>
        {
            return IEnumerableEquals(a, b, (x, y) => x.Equals(y));
        }

        /// <summary>
        /// Checks whether the specified sequences *a* and *b* contain equal elements
        /// determined by the provided comparer
        /// </summary>
        /// <param name="comparer">The function which takes two elements from the
        /// sequences and returns true if they are equal.
        /// </param>
        public static bool IEnumerableEquals<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> comparer)
        {
            bool aHasNext, bHasNext;            

            var iA = a.GetEnumerator();
            var iB = b.GetEnumerator();

            aHasNext = iA.MoveNext();
            bHasNext = iB.MoveNext();

            while (aHasNext && bHasNext)
            {
                if (!comparer(iA.Current, iB.Current))
                    return false;

                aHasNext = iA.MoveNext();
                bHasNext = iB.MoveNext();
            }

            return aHasNext == bHasNext;
        }
    }
}
