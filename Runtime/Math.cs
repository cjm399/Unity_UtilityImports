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

        #endregion
    }
}
