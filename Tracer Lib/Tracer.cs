using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Tracer_Lib
{

    //C# naming convention
    
    public class Tracer : ITracer
    {
        private long _currentlyTracing;
        private TraceResult _traceResult;
        object obj;

        public Tracer()
        {
            _traceResult = new TraceResult();
            _currentlyTracing = 0;
        }
        
        public void StartTrace()
        {    
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            lock (obj)
            {
                _currentlyTracing++;    
            }
           
            _traceResult.StartTracing(method);
        }

        public void StopTrace()
        {    
            
            _traceResult.StopTracing();
            lock (obj)
            {
                _currentlyTracing--;    
            }
            
        }

        public TraceResult GetTraceResult()
        {
            return _currentlyTracing == 0 ? _traceResult : null;
        }
       
    }
}