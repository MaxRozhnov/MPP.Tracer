using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace Tracer_Lib
{

    //C# naming convention
    
    public class Tracer : ITracer
    {
        //private readonly Stopwatch _stopwatch = new Stopwatch();
        //private Stopwatch[] _stopwatchStack;
        private Stack _stopwatchStack = new Stack();
        private Stack _results = new Stack();
        private Stack _finalResults = new Stack();
        public void StartTrace()
        {
            Stopwatch tempStopWatch = new Stopwatch();
            StackTrace stackTrace = new StackTrace();
            ResultContainer tempresult = new ResultContainer{methodName = stackTrace.GetFrame(1).GetMethod().Name, 
                                                     className = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name};
            _results.Push(tempresult);
            tempStopWatch.Start();
            _stopwatchStack.Push(tempStopWatch);
        }

        public void StopTrace()
        {
            Stopwatch tempStopWatch = (Stopwatch)_stopwatchStack.Pop();
            tempStopWatch.Stop();
            ResultContainer tempResultContainer = (ResultContainer) _results.Pop();
            tempResultContainer.exactTime = tempStopWatch.ElapsedMilliseconds;
            _finalResults.Push(tempResultContainer);
            /*TODO: Record the time somewhere
             *Interesting idea - use stack 
             *structure for tracing results.             
             */
        }

        public TraceResult GetTraceResult()
        {

            var result = new TraceResult {ResultsStack = _finalResults};
            return result;
        }
       
    }
}