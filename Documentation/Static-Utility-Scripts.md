# Static Utility Scripts Documentation

## Overview
This is supposed to be a simple package of some Static scripts that you can import into any Unity Project and use as Utility Tools. The use of these packages is meant to be simple and speed up you game-dev workflow. Nothing is dependent upon Monobehaviour, so you can access anything from anywhere. I will be adding to this over time as I develop more and more tools that I personally find useful to my development.

You can see the github page for this project [here](https://github.com/cjm399/Unity_UtilityImports) as well. If you choose to not go through Unity's Asset importer, want to fork the project, etc. I have this hosted on Unity as well for ease of access and discovery, as well as to bolster my portfolio.

## Tools
Here I'll just go through the tools that I have developed and pushed into the project thus far.

### Log
A simple logger, I have some pound defines that you can use to easily remove logging from projects such as for shipped builds. I include Verbosity through the LogVerbosity enum, which includes Display (lowest), Trace, Warning, Error, Assert (highest), and NoLogs (Actual Highest).

### Rand
A simple class to pregenerate (on game startup, not at build/compile time) cached random integers. I frequently use this to cut down on runtime random number generation. I generate numbers and then manipulate the cached values however I see fit.

### Static
This is just very useful Static functions that are basic. I include common delegates here for C# events, such as:
`public delegate void Void_Delegate();`
or
`public delegate void Int_Delegate(int _val);`

I also include simple converters here, currently I only support PercentAsBool and BoolAsPercent

### Stopwatch
A simple stopwatch class to track time. I can start, stop, or reset timers. I can also get timers elapsed milliseconds.

### UIHelpers
Just as it sounds, very common UI helpers noteably the following:
* isHoveringUIElement
* ToggleCanvasGroup
* MoveTo
* LerpImageColor
* FormatAsNumber
* FormatAsMoney
* PositionRadially
* WorldSpaceToScreenSpace

And a couple others that are similar to some of the above listed. This is by far the largest and most complex of the scripts.
