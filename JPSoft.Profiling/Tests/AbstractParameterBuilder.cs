using System.Collections.Generic;

namespace JPSoft.Profiling
{
    abstract class AbstractParameterBuilder<TReturn, TInternal, TParam> : ITestParameterBase<TReturn, TParam>, ITestHolder
    where TInternal : TReturn, ITestHolder
    {
        readonly TInternal _returnee;

        public AbstractParameterBuilder(TInternal returnee)
        {
            _returnee = returnee;

            Test = returnee.Test;
        }
        public ITestInternal Test { get; }

        public TReturn WithParameter(TParam parameter)
        {
            Test.InsertParameter(parameter);

            return _returnee;
        }
    }
}