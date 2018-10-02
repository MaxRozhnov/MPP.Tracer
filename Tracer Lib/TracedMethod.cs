using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer_Lib
{
    public class TracedMethod
    {
        public readonly string ClassName;
        public readonly string MethodName;
        public long ElapsedTime;
        private readonly Stopwatch _stopwatch;
        
        public List<TracedMethod> ChildrenMethods;


        internal TracedMethod(MethodBase method)
        {
            MethodName = method.Name;
            ClassName = method.DeclaringType?.Name;
            ElapsedTime = 0;
            _stopwatch = new Stopwatch();
            ChildrenMethods = new List<TracedMethod>();
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
            ChildrenMethods.Add(child);
        }
        
    }
}