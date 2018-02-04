using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    abstract class AbstractTest<TAction> : ITestInternal
    {
        string name;
        public TAction Code { get; }
        public Guid Guid { get; }
        public string Name
        {
            get => string.IsNullOrWhiteSpace(name) ?
                $"Test {Guid}" :
                name;
            set => name = value;
        }
        public virtual byte ParameterCount { get; } = 0;
        public long Iterations { get; set; } = 1000000;
        public TimeSpan Timeout { get; set; }

        public AbstractTest(TAction code) { Guid = Guid.NewGuid(); Code = code; }

        public virtual bool TryGetParameters(out IEnumerable<object> parameters)
        {
            parameters = null;

            return false;
        }

        public bool IsOfTypeTest<T>()
        where T : ITest => this.GetType() == typeof(T);

        public virtual void InsertParameter(object parameter) =>
            throw new InvalidOperationException($"Type {this.GetType()} cannot hold parameters.");

        public object GetAction() => Code;
    }
}