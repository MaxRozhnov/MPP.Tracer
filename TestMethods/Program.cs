using System;
using Tracer_Lib;
using Tracer_Lib.Serialization;

namespace TestMethods
{
    class Program
    {

        static void Main(string[] args)
        {
            MethodContainer methodContainer = new MethodContainer();
            methodContainer.TestMethod();
        }


    }

    class MethodContainer
    {
        private Tracer _tracer = new Tracer();

        public void TestMethod()
        {
            _tracer.StartTrace();
            Console.WriteLine("Parent method starts");
            System.Threading.Thread.Sleep(100);
            
            InnerMethod();
            InnerMethod();
            InnerMethod();
            Console.WriteLine("Parent method done");
            _tracer.StopTrace();
            JSONSerializer toJson = new JSONSerializer();
            Console.WriteLine(toJson.Serialize(_tracer.GetTraceResult()));
        }

        public void InnerMethod()
        {
            
            _tracer.StartTrace();
            Console.WriteLine("    Inner method starts");
            System.Threading.Thread.Sleep(100);
            InnerInnerMethod();
            Console.WriteLine("    Inner method done");
            _tracer.StopTrace();
        }

        public void InnerInnerMethod()
        {
            _tracer.StartTrace();
            Console.WriteLine("        Inner Inner method starts");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine("        Inner Inner method done");
            _tracer.StopTrace();
            
            
            
            
        }

    }
}