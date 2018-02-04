using System.Collections.Generic;

namespace JPSoft.Profiling
{
    abstract class AbstractTestWithParams<TAction> : AbstractTest<TAction>
    {
        readonly List<object> _parameters;
        public override byte ParameterCount =>(byte) _parameters.Count;

        public AbstractTestWithParams(TAction code) : base(code) { }

        public override bool TryGetParameters(out IEnumerable<object> parameters)
        {
            parameters = _parameters;

            return ParameterCount > 0;
        }

        public override void AddParameter(object parameter) => _parameters.Insert(0, parameter);
    }
}