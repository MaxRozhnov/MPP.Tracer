using System;
using System.Threading;
using TestMethods.Writer;
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
        private Tracer _tracer;
        private TraceResult _result;

        public MethodContainer()
        {
            _tracer = new Tracer();
        }

        public void TestMethod()
        {
            _tracer.StartTrace();
            Thread thread = new Thread(InnerMethod) { IsBackground = true };
            Thread thread2 = new Thread(InnerInnerMethod) { IsBackground = true };
            
            thread.Start();
            thread2.Start();
            Thread.Sleep(100);
            InnerMethod();
            InnerMethod();
            InnerMethod();
            _tracer.StopTrace();

            IWriter fileWriter = new FileWriter("output.txt");
            IWriter consoleWriter = new ConsoleWriter();
            ISerializer xmlSerializer = new XMLSerializer();

            while (_result == null)
            {
                Thread.Sleep(50);
                _result = _tracer.GetTraceResult();
            }

            consoleWriter.Write(xmlSerializer.Serialize(_result));
            fileWriter.Write(xmlSerializer.Serialize(_result));

        }

        public void InnerMethod()
        {
            
            _tracer.StartTrace();
            Thread.Sleep(100);
            InnerInnerMethod();
            _tracer.StopTrace();
        }

        public void InnerInnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();

        }
    }
}