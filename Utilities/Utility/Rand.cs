using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Rand
    {
        const int randIntSize = 1000;
        static int[] cachedRandomIntegers = new int[randIntSize];
        static int cachedRandomIntegersIndex = 0;

        public static void InitializeCachedRandomValues(int minVal = 0, int maxVal = 10000)
        {
            for (int i = 0; i < randIntSize; ++i)
            {
                cachedRandomIntegers[i] = Random.Range(minVal, maxVal);
            }
        }

        public static void InitializeStartIndex()
        {
            cachedRandomIntegersIndex = Random.Range(0, randIntSize);
        }

        public static int GetRandomInt(int _maxVal)
        {
            cachedRandomIntegersIndex += 1;
            if (cachedRandomIntegersIndex >= randIntSize)
            {
                cachedRandomIntegersIndex = 0;
            }
            return cachedRandomIntegers[cachedRandomIntegersIndex] % (_maxVal + 1);
        }
    }
}