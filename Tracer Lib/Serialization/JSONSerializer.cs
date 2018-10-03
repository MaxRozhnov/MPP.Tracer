using Newtonsoft.Json;

namespace Tracer_Lib.Serialization
{
    public class JSONSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult.TracedThreadsLists, Formatting.Indented) + "\n" + JsonConvert.SerializeObject(traceResult.ThreadExecutionTimes, Formatting.Indented);
        }
    }
}