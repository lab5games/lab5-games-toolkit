using System;
using System.Collections.Generic;

namespace Lab5Games
{
    public static class ListExtension
    {
        public static T First<T>(this IList<T> inList)
        {
            return inList[0];
        }

        public static T Last<T>(this IList<T> inList)
        {
            return inList[inList.Count - 1];
        }

        public static List<string> GetStringList<T>(this IList<T> inList)
        {
            List<string> result = new List<string>();

            foreach(var obj in inList)
            {
                result.Add(obj.IsNull() ? "NULL" : obj.ToString());
            }

            return result;
        }

        public static List<T> Clone<T>(this IList<T> inList)
        {
            List<T> result = new List<T>();

            foreach (var obj in inList)
                result.Add(obj);

            return result;
        }

        public static bool ContainsNull<T>(this IList<T> inList)
        {
            foreach(var obj in inList)
            {
                if (obj.IsNull())
                    return true;
            }

            return false;
        }

        public static int RemoveNulls<T>(this IList<T> inList)
        {
            int cnt = 0;

            for(int i=inList.Count-1; i>=0; i--)
            {
                if (inList[i].IsNull())
                {
                    cnt++;
                    inList.RemoveAt(i);
                }
            }

            return cnt;
        }

        public static bool AddUnique<T>(this IList<T> inList, T obj)
        {
            if (obj.IsNull())
                return false;

            if (inList.Contains(obj))
                return false;

            inList.Add(obj);
            
            return true;
        }

        public static List<T> Distinct<T>(this IList<T> inList)
        {
            List<T> result = new List<T>();

            foreach(var obj in inList)
            {
                inList.AddUnique(obj);
            }

            return result;
        }

        public static List<T> Reverse<T>(this IList<T> inList)
        {
            int start = 0;
            int end = inList.Count - 1;
            List<T> result = inList.Clone();

            while (end < start)
            {
                T tmp = result[start];
                result[start] = result[end];
                result[end] = tmp;

                start++;
                end--;
            }

            return result;
        }

        public static List<T> Shuffle<T>(this IList<T> inList)
        {
            int n = inList.Count;
            List<T> result = inList.Clone();

            while (n > 1)
            {
                int k = UnityEngine.Random.Range(0, n);
                T val = result[k];
                result[k] = result[n - 1];
                result[n - 1] = val;

                --n;
            }

            return result;
        }

        /// <summary>
        /// 聯集。囊括a-list與b-list中所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aList"></param>
        /// <param name="bList"></param>
        /// <returns></returns>
        public static List<T> Union<T>(this IList<T> aList, IList<T> bList)
        {
            List<T> result = new List<T>();

            foreach (var a in aList)
                result.Add(a);

            foreach (var b in bList)
                result.Add(b);

            return result;
        }

        /// <summary>
        /// 交集。a-list中有，且b-list中也有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aList"></param>
        /// <param name="bList"></param>
        /// <returns></returns>
        public static List<T> Intersect<T>(this IList<T> aList, IList<T> bList)
        {
            List<T> result = new List<T>();

            foreach(var a in aList)
            {
                if(bList.Contains(a))
                {
                    result.Add(a);
                }
            }

            return result;
        }

        /// <summary>
        /// 差集。a-list中有，而b-list中沒有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aList"></param>
        /// <param name="bList"></param>
        /// <returns></returns>
        public static List<T> Except<T>(this IList<T> aList, IList<T> bList)
        {
            List<T> result = new List<T>();

            foreach (var a in aList)
            {
                if (!bList.Contains(a))
                {
                    result.Add(a);
                }
            }

            return result;
        }
    }
}
