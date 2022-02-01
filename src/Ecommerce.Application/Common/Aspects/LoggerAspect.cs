using MethodBoundaryAspect.Fody.Attributes;

namespace Ecommence.Application.Common.Aspects
{
    public sealed class LoggerAspect : OnMethodBoundaryAspect 
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
           //Log info about method starting

        }

        public override void OnExit(MethodExecutionArgs args)
        {
            //Log info about method ending
        }

        public override void OnException(MethodExecutionArgs args)
        {
            //Catch unexcepted exception
            //Log info about method exception
        }

    }
}
