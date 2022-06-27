using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SampleCode : MonoBehaviour
{
    public CanvasGroup loadingGroup;
    public CanvasGroup movingGroup;
    public RectTransform image;
    private const string logSpace = "SampleCode";
    
    private IEnumerator Start()
    {

        UIHelpers.Initialize();
        UIHelpers.ToggleCanvasGroup(ref loadingGroup, true);
        Log.Trace(logSpace, "Loading");
        yield return Static.WaitFor1Second;
        UIHelpers.ToggleCanvasGroup(ref loadingGroup, false);
        UIHelpers.LerpCanvasGroup(ref movingGroup, 0, 1, 1);
        yield return Static.WaitFor1Second;
        UIHelpers.MoveTo(ref image, Vector2.zero, Vector2.one / 2f, 100f, AnimationCurve.EaseInOut(0, 0, 1, 1));

        Log.Display(logSpace, "Display");
        Log.Trace(logSpace, "Trace");
        Log.Warning(logSpace, "Warning");
        Log.Error(logSpace, "Error");
        Log.Fatal(logSpace, "Fatal");

        Log.minLogDisplayLevel = LogVerbosity.Warning;
        Log.Display(logSpace, "Display 2");
        Log.Trace(logSpace, "Trace 2");
        Log.Warning(logSpace, "Warning 2");
        Log.Error(logSpace, "Error 2");

        Log.minLogDisplayLevel = LogVerbosity.Display;
        Stopwatch sw = new Stopwatch();

        yield return Static.WaitFor1Second;

        Log.Verbose(logSpace, $"Elapsed ms: {sw.ElapsedMilliseconds().ToString()}");
        Log.Display(logSpace, $"I have {UIHelpers.FormatAsMoney(5)}");
        Log.Display(logSpace, $"This is PI as a number {UIHelpers.FormatAsNumber(Mathf.PI)}");
    }
}
