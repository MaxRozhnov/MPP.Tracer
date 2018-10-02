using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer_Lib
{

    //C# naming convention
    
    public class Tracer : ITracer
    {
        private long currentlyTracing;
        private TraceResult _traceResult;
        //private readonly Stopwatch _stopwatch = new Stopwatch();
        //private Stopwatch[] _stopwatchStack;

        //private Dictionary<int, Stack> _threadStacks;

        public Tracer()
        {
            _traceResult = new TraceResult();
            currentlyTracing = 0;
        }
        
        public void StartTrace()
        {    
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            currentlyTracing++;
            _traceResult.StartTracing(method);
//            Stopwatch tempStopWatch = new Stopwatch();
//            StackTrace stackTrace = new StackTrace();
//            ResultContainer tempresult = new ResultContainer{methodName = stackTrace.GetFrame(1).GetMethod().Name, 
//                                                             className = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name};
//            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
//            _results.Push(tempresult);
//            tempStopWatch.Start();
//            _stopwatchStack.Push(tempStopWatch);
        }

        public void StopTrace()
        {    
            
            _traceResult.StopTracing();
            currentlyTracing--;
//            Stopwatch tempStopWatch = (Stopwatch)_stopwatchStack.Pop();
//            tempStopWatch.Stop();
//            ResultContainer tempResultContainer = (ResultContainer) _results.Pop();
//            tempResultContainer.exactTime = tempStopWatch.ElapsedMilliseconds;
//            _finalResults.Push(tempResultContainer);
        }

        public TraceResult GetTraceResult()
        {
            return currentlyTracing == 0 ? _traceResult : null;
        }
       
    }
}