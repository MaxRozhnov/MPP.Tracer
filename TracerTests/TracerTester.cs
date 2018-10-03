using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer_Lib;
using System.Linq;
using System.Collections.Generic;

namespace TracerTests
{
    [TestClass]
    public class TracerTester
    {
        private ITracer _tracer;
        private TraceResult _result;

        [TestMethod]
        public void ThreadTimeTest()
        {
            int minimalExpectedThreadTime = 120;
            long mainThreadTime = _result.ThreadExecutionTimes.Values.FirstOrDefault();
            if (mainThreadTime < minimalExpectedThreadTime)
            {
                Assert.Fail("Wrong Time");
            }  
        }

        [TestMethod]
        public void MethodNameTest()
        {
            String expectedMethodName = "BasicMethod";
            List<TracedMethod> firstThreadMethods = _result.TracedThreadsLists.Values.FirstOrDefault();
            string firstMethodName = firstThreadMethods.FirstOrDefault().MethodName;
            Assert.AreEqual(expectedMethodName, firstMethodName);
        }

        [TestMethod]
        public void DeclaringClassNameTest()
        {
            String expectedDeclaringClassName = "TracerTester";
            List<TracedMethod> firstThreadMethods = _result.TracedThreadsLists.Values.FirstOrDefault();
            string declaringClassName = firstThreadMethods.FirstOrDefault().ClassName;
            Assert.AreEqual(expectedDeclaringClassName, declaringClassName);
        }

        [TestMethod]
        public void ThreadCountTest()
        {
            int threadCount = 0;
            int expectedThreadCount = 2;

            var threadKeys = _result.TracedThreadsLists.Keys;
            foreach(var key in threadKeys)
            {
                threadCount++;
            }
            Assert.AreEqual(expectedThreadCount, threadCount);
        }



        private void BasicMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Thread thread = new Thread(InnerMethod) { IsBackground = true };
            thread.Start();
            InnerMethod();
            _tracer.StopTrace();

            while (_result == null)
            {
                Thread.Sleep(20);
                _result = _tracer.GetTraceResult();
            }
        }

        private void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(20);
            _tracer.StopTrace();
        }

        [TestInitialize]
        public void Setup()
        {
            _tracer = new Tracer();
            BasicMethod();
        }
    }
}
