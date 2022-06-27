using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{   
    public enum LogVerbosity
    {
        NoLogs = 0,
        Fatal = 1,
        Error = 2,
        Assert = 3,
        Warning = 4,
        Display = 5,
        Trace = 6,
        Verbose = 7,
        VeryVerbose = 8,
        All = 999
    }

    public static class Log
    {
        public static LogVerbosity minLogDisplayLevel = LogVerbosity.Display;

#if !SUS_NO_LOGS || UNITY_EDITOR
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void DebugMessage(LogVerbosity _verbosity, string _source, string _text)
        {

            if (_verbosity >= minLogDisplayLevel)
            {
                switch (_verbosity)
                {
                    case LogVerbosity.VeryVerbose:
                        VeryVerbose(_source, _text);
                        break;
                    case LogVerbosity.Verbose:
                        Verbose(_source, _text);
                        break;
                    case LogVerbosity.Trace:
                        Trace(_source, _text);
                        break;
                    case LogVerbosity.Display:
                        Display(_source, _text);
                        break;
                    case LogVerbosity.Warning:
                        Warning(_source, _text);
                        break;
                    case LogVerbosity.Error:
                        Error(_source, _text);
                        break;
                    case LogVerbosity.Fatal:
                        Fatal(_source, _text);
                        break;
                }
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Fatal(string _source, string _text)
        {
            if (LogVerbosity.Fatal <= minLogDisplayLevel)
            {
                Debug.LogError($"<b>Fatal</b> : {_source} : {_text}");
            }
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Error(string _source, string _text)
        {
            if (LogVerbosity.Error <= minLogDisplayLevel)
            {
                Debug.LogError($"<b>Error</b> : {_source} : {_text}");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Assert(bool _check, string _text = "")
        {
            if (!_check && LogVerbosity.Assert <= minLogDisplayLevel)
            {
                Debug.LogError($"<b>Assertion Failed</b> : {_text}");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Warning(string _source, string _text)
        {
            if (LogVerbosity.Warning <= minLogDisplayLevel)
            {
                Debug.LogWarning($"<b>Warning</b> : {_source} : {_text}");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Trace(string _source, string _text)
        {
            if (LogVerbosity.Trace <= minLogDisplayLevel)
            {
                Debug.Log($"<color=#436FB6><b>Trace</b> : {_source} : {_text}</color>");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Display(string _source, string _text)
        {
            if (LogVerbosity.Display <= minLogDisplayLevel)
            {
                Debug.Log($"<color=#000000><b>Display</b> : {_source} : {_text}</color>");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Verbose(string _source, string _text = "")
        {
            if (LogVerbosity.Verbose <= minLogDisplayLevel)
            {
                Debug.Log($"<color=#3C3837><b>Verbose</b> : {_text}</color>");
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void VeryVerbose(string _source, string _text)
        {
            if (LogVerbosity.VeryVerbose <= minLogDisplayLevel)
            {
                Debug.Log($"<color=#484340><b>VeryVerbose</b> : {_source} : {_text}</color>");
            }
        }

#else
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void DebugMessage(LogVerbosity _verbosity, string _source, string _text){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Fatal(string _source, string _text){}
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Error(string _source, string _text){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Assert(bool _check, string _text = ""){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Warning(string _source, string _text){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Trace(string _source, string _text){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Display(string _source, string _text){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void Verbose(string _source, string _text = ""){}

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void VeryVerbose(string _source, string _text){}
#endif
    }
}