using Newtonsoft.Json;

namespace Tracer_Lib.Serialization
{
    public class JSONSerializer: ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult.TracedThreadsLists, Formatting.Indented);
        }
    }
}