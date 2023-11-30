public class RunnableInDebugOnlyFactAttribute : FactAttribute
{
#if !DEBUG
    public override string Skip => "Debug";
#endif
}