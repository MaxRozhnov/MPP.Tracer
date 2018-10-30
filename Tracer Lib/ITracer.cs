namespace Tracer_Lib
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}