using System;
using Tracer_Lib;

namespace TestMethods
{
    class Progran
    {
        
        static void Main(string[] args)
        {
            MethodContainer _methodContainer = new MethodContainer();
           
            //Console.WriteLine("Hello World!");
            _methodContainer.TestMethod();
        }


    }

    class MethodContainer
    {
        private Tracer _tracer = new Tracer();
        public void TestMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Done");
            _tracer.StopTrace();
            Console.WriteLine("Method took " + _tracer.GetTraceResult().exactTime + " ms");
        }
        
    }
}