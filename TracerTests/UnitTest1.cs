using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer_Lib;

namespace TracerTests
{
    [TestClass]
    public class TracerTester
    {
        private ITracer _tracer;

        [TestMethod]
        public void TimeTest()
        {
          
           
        }

        private void BasicMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            _tracer.StopTrace();
        }

        [TestInitialize]
        private void Setup()
        {
            _tracer = new Tracer();
            BasicMethod();
        }
    }


}
