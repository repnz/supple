using System.Collections;

namespace Supple.Collections
{
    public static class EnumerableExtentions
    {
        public static bool IsItemsEqual(this IList listA, IList listB)
        {
            if (listA.Count != listB.Count)
            {
                return false;
            }

            for (int i = 0; i < listA.Count; ++i)
            {
                if (!listA[i].Equals(listB[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
