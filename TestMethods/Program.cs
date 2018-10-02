using System;
using Tracer_Lib;

namespace TestMethods
{
    class Program
    {

        static void Main(string[] args)
        {
            MethodContainer methodContainer = new MethodContainer();

            //Console.WriteLine("Hello World!");
            methodContainer.TestMethod();
        }


    }

    class MethodContainer
    {
        private Tracer _tracer = new Tracer();

        public void TestMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            
            InnerMethod();
            InnerMethod();
            InnerMethod();
            Console.WriteLine("Parent method done");
            _tracer.StopTrace();
            

//            var result = _tracer.GetTraceResult();
//            Console.WriteLine("Method " + result.methodName + " in class " +result.className + " took " + result.exactTime + " ms");

        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            InnerInnerMethod();
            Console.WriteLine("Inner method done");
            _tracer.StopTrace();
        }

        public void InnerInnerMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("Inner Inner method done");
            _tracer.StopTrace();
            
        }

    }
}