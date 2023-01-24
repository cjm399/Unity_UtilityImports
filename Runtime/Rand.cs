using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpacePigs.Math
{
#if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
    public class RandomEditor
    {
        static RandomEditor()
        {
            Rand.InitializeCachedRandomValues();
        }
    }
#endif


        public static class Rand
    {
        const int RAND_INT_COUNT = 1000;
        const int MAX_RAND_INT_VAL = 10000;

        private static int[] CachedRandomIntegers = new int[RAND_INT_COUNT];
        private static int CachedRandomIntegersIndex = 0;

        //TODO(chris):Store this as a list of already random things.

        [RuntimeInitializeOnLoadMethod]
        public static void InitializeCachedRandomValues()
        {
            for (int i = 0; i < RAND_INT_COUNT; ++i)
            {
                CachedRandomIntegers[i] = Random.Range(0, MAX_RAND_INT_VAL);
            }
            InitializeStartIndex();
        }

        public static void InitializeStartIndex()
        {
            CachedRandomIntegersIndex = Random.Range(0, RAND_INT_COUNT);
        }

        public static int GetRandomInt(int _maxVal)
        {
            CachedRandomIntegersIndex += 1;
            if (CachedRandomIntegersIndex >= RAND_INT_COUNT)
            {
                CachedRandomIntegersIndex = 0;
            }
            return CachedRandomIntegers[CachedRandomIntegersIndex] % (_maxVal + 1);
        }

        public static float GetRandomFloat(int _maxVal)
        {
            CachedRandomIntegersIndex += 1;
            if (CachedRandomIntegersIndex >= RAND_INT_COUNT)
            {
                CachedRandomIntegersIndex = 0;
            }
            float result = CachedRandomIntegers[CachedRandomIntegersIndex] % (_maxVal + 1);
            return result;
        }

        public static float GetRandomPercentage()
        {
            CachedRandomIntegersIndex += 1;
            if (CachedRandomIntegersIndex >= RAND_INT_COUNT)
            {
                CachedRandomIntegersIndex = 0;
            }
            float result = CachedRandomIntegers[CachedRandomIntegersIndex] / (float)MAX_RAND_INT_VAL;
            return result;
        }

        //TODO(chris): Use cached numbers
        public static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
        {
            List<int> possibleNumbers = new List<int>();
            List<int> chosenNumbers = new List<int>();

            for (int index = minValue; index < maxValue; index++)
                possibleNumbers.Add(index);

            while (chosenNumbers.Count < count)
            {
                int position = UnityEngine.Random.Range(0, possibleNumbers.Count);
                chosenNumbers.Add(possibleNumbers[position]);
                possibleNumbers.RemoveAt(position);
            }
            return chosenNumbers;
        }
    }
}