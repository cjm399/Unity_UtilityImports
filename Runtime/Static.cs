using UnityEngine;

namespace SpacePigs.Utilities
{
    public static class Static
    {
        public const System.Runtime.CompilerServices.MethodImplOptions INLINE = System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining;

        public delegate void Void_Delegate();
        public delegate void Int_Delegate(int _val);
        public delegate void Bool_Delegate(bool _val);
        public delegate void Float_Delegate(float _val);
        public delegate void String_Delegate(string _val);
        public delegate void Double_Delegate(double _val);
        public delegate void Vector2_Delegate(Vector2 _val);
        public delegate void Vector3_Delegate(Vector3 _val);

        public static WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static WaitForSeconds WaitFor0Point15Second = new WaitForSeconds(.15f);
        public static WaitForSeconds WaitFor0Point1Second = new WaitForSeconds(.1f);
        public static WaitForSeconds WaitFor0Point25Second = new WaitForSeconds(.25f);
        public static WaitForSeconds WaitFor0Point33Second = new WaitForSeconds(.33f);
        public static WaitForSeconds WaitFor0Point5Second = new WaitForSeconds(.5f);
        public static WaitForSeconds WaitFor1Second = new WaitForSeconds(1f);

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool PercentAsBool(float _percent)
        {
            return _percent >= 1;
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static float BoolAsPercent(bool _bool)
        {
            return (_bool ? 1 : 0);
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static char KeyCodeToLower(KeyCode kc)
        {
            return (char)kc;
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static char KeyCodeToUpper(KeyCode kc)
        {
            return (char)((int)kc - 32);
        }

        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(ref System.Collections.Generic.List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}