using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpacePigs.Math
{
    public static class Math
    {
        #region Int/UInt/Short
        
        #region Max
        
        public static int MaxInt(params int[] _ints)
        {
            int result = _ints[0];
            
            for(int i = 1; i < _ints.Length; ++i)
            {
                result = (result > _ints[i] ? result : _ints[i]);
            }

            return result;
        }

        public static uint MaxUInt(params uint[] _uints)
        {
            uint result = _uints[0];

            for (uint i = 1; i < _uints.Length; ++i)
            {
                result = (result > _uints[i] ? result : _uints[i]);
            }

            return result;
        }

        public static short MinShort(params short[] _shorts)
        {
            short result = _shorts[0];

            for (uint i = 1; i < _shorts.Length; ++i)
            {
                result = (result > _shorts[i] ? result : _shorts[i]);
            }

            return result;
        }

        #endregion

        #region Min

        public static int MinInt(params int[] _ints)
        {
            int result = _ints[0];

            for (int i = 1; i < _ints.Length; ++i)
            {
                result = (result < _ints[i] ? result : _ints[i]);
            }

            return result;
        }

        public static uint MinUInt(params uint[] _uints)
        {
            uint result = _uints[0];

            for (uint i = 1; i < _uints.Length; ++i)
            {
                result = (result < _uints[i] ? result : _uints[i]);
            }

            return result;
        }

        public static short MaxShort(params short[] _shorts)
        {
            short result = _shorts[0];

            for (uint i = 1; i < _shorts.Length; ++i)
            {
                result = (result < _shorts[i] ? result : _shorts[i]);
            }

            return result;
        }

        #endregion

        //TODO(chris): Move to Random File?
        #region Random

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

        public static IEnumerator BasicLerp(float startVal, float endVal, float duration)
        {
            float timer = 0f;
            float returnVal = startVal;
            while (timer < duration)
            {
                timer += Time.smoothDeltaTime;
                returnVal = Mathf.Lerp(startVal, endVal, (timer / duration));
                yield return returnVal;
            }
            yield return endVal;
        }

        #endregion

        #endregion
    }
}
