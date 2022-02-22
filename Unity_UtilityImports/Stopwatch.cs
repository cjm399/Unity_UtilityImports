using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch
{
    public enum StopwatchPlayState
    {
        None    =   0 << 0,
        Started =   1 << 0,
        Stoped  =   1 << 1,
    }

    public long startTime { get; private set;}
    public long endTime { get; private set; }

    public StopwatchPlayState playState;

    public Stopwatch()
    {
        playState = StopwatchPlayState.Started;
        startTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        endTime = startTime;
    }

    public void Start()
    {
        startTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        playState = StopwatchPlayState.Started;
    }
    public void Restart()
    {
        startTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        playState = StopwatchPlayState.Started;
    }

    public void Stop()
    {
        endTime = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        playState = StopwatchPlayState.Stoped;
    }


    public long ElapsedMilliseconds()
    {
        long result = 0;
        long stopResult = (endTime - startTime) * 
            ((int)playState & (int)StopwatchPlayState.Stoped) / (int)StopwatchPlayState.Stoped;

        long playTime = ((System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond) - startTime) *
            ((int)playState & (int)StopwatchPlayState.Started) / (int)StopwatchPlayState.Started;
        
        result = stopResult + playTime;
        return result;
    }
}
