using UnityEngine;

public static class Static
{
    public delegate void Void_Delegate();
    public delegate void Int_Delegate(int _val);
    public delegate void Bool_Delegate(bool _val);
    public delegate void Float_Delegate(float _val);
    public delegate void String_Delegate(string _val);
    public delegate void Double_Delegate(double _val);
    public delegate void Vector2_Delegate(Vector2 _val);
    public delegate void Vector3_Delegate(Vector3 _val);

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
}
