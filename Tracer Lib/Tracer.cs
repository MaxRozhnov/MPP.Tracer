using System;
using System.Collections;
using System.Diagnostics;

namespace Tracer_Lib
{

    //C# naming convention
    
    public class Tracer : ITracer
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        //private Stopwatch[] _stopwatchStack;
        private Stack _stopwatchStack = new Stack();
        public void StartTrace()
        {
            Stopwatch tempStopWatch = new Stopwatch();
            tempStopWatch.Start();
            _stopwatchStack.Push(tempStopWatch);
        }

        public void StopTrace()
        {
            Stopwatch tempStopWatch = (Stopwatch)_stopwatchStack.Pop();
            tempStopWatch.Stop();
            /*TODO: Record the time somewhere
             *Interesting idea - use stack 
             *structure for tracing results.             
             */
        }

        public TraceResult GetTraceResult()
        {
            var result = new TraceResult {exactTime = _stopwatch.ElapsedMilliseconds};
            return result;
        }
    }
}