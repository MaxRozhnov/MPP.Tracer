using System;
using System.Threading;
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
            Console.ReadLine();
        }


    }

    class MethodContainer
    {
        private Tracer _tracer = new Tracer();

        public void TestMethod()
        {
            _tracer.StartTrace();
            Thread thread = new Thread(InnerMethod) { IsBackground = true };
            Thread thread2 = new Thread(InnerInnerMethod) { IsBackground = true };
            
            thread.Start();
            thread2.Start();
            System.Threading.Thread.Sleep(100);
            InnerMethod();
            InnerMethod();
            InnerMethod();
            _tracer.StopTrace();
            ISerializer toXML = new JSONSerializer();
            Console.WriteLine(toXML.Serialize(_tracer.GetTraceResult()));
        }

        public void InnerMethod()
        {
            
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            InnerInnerMethod();
            _tracer.StopTrace();
        }

        public void InnerInnerMethod()
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(100);
            _tracer.StopTrace();
            
            
            
            
        }

    }
}