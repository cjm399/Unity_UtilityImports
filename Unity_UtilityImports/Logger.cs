using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogVerbosity
{
    Display = 0,
    Trace = 1,
    Warning = 2,
    Error = 3,
    NoLogs = 999,
}

public static class Logger 
{
    public static LogVerbosity minLogDisplayLevel = LogVerbosity.Display;

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.Diagnostics.Contracts.Pure]
    public static void Log(LogVerbosity _verbosity, string _source, string _text)
    {
        if(_verbosity >= minLogDisplayLevel)
        {
            switch (_verbosity)
            {
                case LogVerbosity.Display:
                    Display(_source, _text);
                    break;
                case LogVerbosity.Trace:
                    Trace(_source, _text);
                    break;
                case LogVerbosity.Warning:
                    Warning(_source, _text);
                    break;
                case LogVerbosity.Error:
                    Error(_source, _text);
                    break;
            }
        }
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.Diagnostics.Contracts.Pure]
    public static void Display(string _source, string _text)
    {
        if (LogVerbosity.Display >= minLogDisplayLevel)
        {
            Debug.Log($"Display : {_source} : {_text}");
        }
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.Diagnostics.Contracts.Pure]
    public static void Trace(string _source, string _text)
    {
        if (LogVerbosity.Trace >= minLogDisplayLevel)
        {
            Debug.Log($"<color=cyan>Trace : {_source} : {_text}</color>");
        }
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.Diagnostics.Contracts.Pure]
    public static void Warning(string _source, string _text)
    {
        if (LogVerbosity.Warning >= minLogDisplayLevel)
        {
            Debug.LogWarning($"Warning : {_source} : {_text}");
        }
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    [System.Diagnostics.Contracts.Pure]
    public static void Error(string _source, string _text)
    {
        if (LogVerbosity.Error >= minLogDisplayLevel)
        {
            Debug.LogError($"Error : {_source} : {_text}");
        }
    }

}
