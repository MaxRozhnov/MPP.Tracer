using System.Collections.Generic;
using System.Xml.Linq;

namespace Tracer_Lib.Serialization
{
    public class XMLSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("root");
            foreach (var key in traceResult.TracedThreadsLists.Keys)
            {
                List<TracedMethod> threadMethodList =
                    traceResult.TracedThreadsLists.GetOrAdd(key, new List<TracedMethod>());
                
                var threadElement = new XElement("thread");
                threadElement.Add(new XAttribute("id", key));
                threadElement.Add(new XAttribute("time", traceResult.ThreadExecutionTimes.GetOrAdd(key, 0)));
                foreach (var method in threadMethodList)
                {
                    threadElement.Add(TracedMethodToXElement(method));
                }
                root.Add(threadElement);
            }
            document.Add(root);
            return document.ToString();

        }

        private XElement TracedMethodToXElement(TracedMethod tracedMethod)
        {    
            XElement result = new XElement("method");
            result.Add(new XAttribute("class", tracedMethod.ClassName));
            result.Add(new XAttribute("name", tracedMethod.MethodName));
            result.Add(new XAttribute("time", tracedMethod.ElapsedTime));
            foreach (var method in tracedMethod.ChildrenMethods)
                {
                    result.Add(TracedMethodToXElement(method));   
                }
            return result;
        }
    }
}