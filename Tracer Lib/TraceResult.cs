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
        public readonly ConcurrentDictionary<int, List<TracedMethod>> TracedThreadsLists;

        public TraceResult()
        {
            tracedThreadsStacks = new ConcurrentDictionary<int, Stack<TracedMethod>>();
            TracedThreadsLists = new ConcurrentDictionary<int, List<TracedMethod>>();
        }
        
        public void StartTracing(MethodBase method)
        {
            int threadId = 0;
            threadId = Thread.CurrentThread.ManagedThreadId;
            TracedMethod tracedMethod = new TracedMethod(method);
            
            if (TracedThreadsLists.ContainsKey(threadId))
            {
                if (tracedThreadsStacks.ContainsKey(threadId))
                {
                    if (tracedThreadsStacks[threadId].Count == 0)
                    {
                        TracedThreadsLists[threadId].Add(tracedMethod);    
                    }
                    else
                    {
                        tracedThreadsStacks[threadId].Peek().AddChild(tracedMethod);    
                    }   
                }
                else
                {
                    List<TracedMethod> methodList = new List<TracedMethod>();
                    TracedThreadsLists.GetOrAdd(threadId, methodList);
                    TracedThreadsLists[threadId].Add(tracedMethod);
                }
            }
            else
            {
                List<TracedMethod> methodList = new List<TracedMethod>();
                TracedThreadsLists.GetOrAdd(threadId, methodList);
                TracedThreadsLists[threadId].Add(tracedMethod);
            }
            

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
   
            
            
            
        }

        public void StopTracing()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TracedMethod tracedMethod = tracedThreadsStacks[threadId].Pop();
            tracedMethod.StopTracing();
            Console.WriteLine(tracedMethod.className + " " + tracedMethod.methodName + " " + tracedMethod.ElapsedTime);

        }
    }
}