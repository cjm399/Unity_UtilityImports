using System.Collections;
using UnityEngine;
using SpacePigs.Utilities;

namespace Utilities.Samples
{
    public class SampleCode : MonoBehaviour
    {
        public CanvasGroup loadingGroup;
        public CanvasGroup movingGroup;
        public RectTransform image;
        private const string logSpace = "SampleCode";

        private IEnumerator Start()
        {
            UIHelpers.ToggleCanvasGroup(ref loadingGroup, true);
            Log.Trace(logSpace, "Loading");
            yield return Static.WaitFor1Second;
            UIHelpers.ToggleCanvasGroup(ref loadingGroup, false);
            UIHelpers.LerpCanvasGroup(ref movingGroup, 0, 1, 1);
            yield return Static.WaitFor1Second;
            UIHelpers.MoveTo(ref image, Vector2.zero, Vector2.one / 2f, 100f, AnimationCurve.EaseInOut(0, 0, 1, 1));

            Log.Display(logSpace, "Test Display");
            Log.Trace(logSpace, "Test Trace");
            Log.Warning(logSpace, "Test Warning");
            Log.Error(logSpace, "Test Error");
            Log.Fatal(logSpace, "Test Fatal");

            Log.minLogDisplayLevel = LogVerbosity.Warning;
            Log.Display(logSpace, "Test Display 2");
            Log.Trace(logSpace, "Test Trace 2");
            Log.Warning(logSpace, "Test Warning 2");
            Log.Error(logSpace, "Test Error 2");

            Log.minLogDisplayLevel = LogVerbosity.Display;
            Stopwatch sw = new Stopwatch();

            yield return Static.WaitFor1Second;

            Log.Display(logSpace, $"Elapsed ms: {sw.ElapsedMilliseconds().ToString()}");
            Log.Display(logSpace, $"I have {UIHelpers.FormatAsMoney(5)}");
            Log.Display(logSpace, $"This is PI as a number {UIHelpers.FormatAsNumber(Mathf.PI)}");
        }
    }
}