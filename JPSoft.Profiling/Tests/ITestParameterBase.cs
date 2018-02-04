namespace JPSoft.Profiling
{
    public interface ITestParameterBase<TReturn, TParam>
    {
        TReturn WithParameter(TParam parameter);
    }
}