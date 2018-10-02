using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer_Lib
{
    public class TracedMethod
    {
        public string className;
        public string methodName;
        public long ElapsedTime;
        private readonly Stopwatch _stopwatch;
        
        public List<TracedMethod> childrenMethods;


        internal TracedMethod(MethodBase method)
        {
            methodName = method.Name;
            className = method.DeclaringType?.Name;
            ElapsedTime = 0;
            _stopwatch = new Stopwatch();
            childrenMethods = new List<TracedMethod>();
          
        }

        internal void StartTracing()
        {
            _stopwatch.Start();
        }

        internal void StopTracing()
        {
           _stopwatch.Stop();
            ElapsedTime = _stopwatch.ElapsedMilliseconds;
        }

        public void AddChild(TracedMethod child)
        {
            childrenMethods.Add(child);
        }
        
    }
}