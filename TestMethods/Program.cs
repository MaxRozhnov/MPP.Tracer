﻿using System;
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
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Done");
            _tracer.StopTrace();
            Console.WriteLine("Method took " + _tracer.GetTraceResult().exactTime + " ms");
        }
        
    }
}