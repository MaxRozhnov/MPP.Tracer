using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Tracer_Lib
{
    public class TraceResult
    {

        private ConcurrentDictionary<int, Stack<TracedMethod>> tracedThreadsStacks;
        private ConcurrentDictionary<int, List<TracedMethod>> tracedThreadsLists;

        public TraceResult()
        {
            tracedThreadsStacks = new ConcurrentDictionary<int, Stack<TracedMethod>>();
            tracedThreadsLists = new ConcurrentDictionary<int, List<TracedMethod>>();
        }
        
        public void StartTracing(MethodBase method)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TracedMethod tracedMethod;
            tracedMethod = new TracedMethod(method);

            if (tracedThreadsStacks.ContainsKey(threadId))
            {
                tracedMethod.StartTracing();
                tracedThreadsStacks[threadId].Push(tracedMethod);
            }
            else
            {
                Stack<TracedMethod> methodStack = new Stack<TracedMethod>();
                tracedThreadsStacks.GetOrAdd(threadId, methodStack);
                tracedMethod.StartTracing();
                tracedThreadsStacks[threadId].Push(tracedMethod);   
            }
            
            //FIXME
            if (tracedThreadsLists.ContainsKey(threadId))
            {
                tracedThreadsLists[threadId].Add(tracedMethod);
            }
            else
            {
                List<TracedMethod> methodList = new List<TracedMethod>();
                tracedThreadsLists.GetOrAdd(threadId, methodList);
                tracedThreadsLists[threadId].Add(tracedMethod);
            }
            //
            
        }

        public void StopTracing()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TracedMethod tracedMethod = tracedThreadsStacks[threadId].Pop();
            tracedMethod.StopTracing();
            Console.Write(tracedMethod.className + " " + tracedMethod.methodName + " " + tracedMethod.ElapsedTime);

        }
    }
}