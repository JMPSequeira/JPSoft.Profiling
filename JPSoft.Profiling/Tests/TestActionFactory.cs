using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace JPSoft.Profiling
{
    static class TestActionFactory
    {
        static Dictionary<int, MethodInfo> _actionCreators;

        static TestActionFactory()
        {
            _actionCreators = typeof(TestActionFactory)
                .GetMethods(BindingFlags.NonPublic)
                .Where(m => m.Name == "CreateAction")
                .ToDictionary(m => m.GetGenericArguments().Length);
        }

        public static Action Create(ITestInternal test) => CreateActionDinamically(test);

        static Action CreateActionDinamically(ITestInternal test)
        {
            if (TryGetTypedActionAndParameters(test, out var typedActionAndParameters))
            {
                if (_actionCreators.TryGetValue(typedActionAndParameters.Count(), out var method))
                {
                    var genericMethod = method.MakeGenericMethod(typedActionAndParameters.Keys.ToArray());

                    return (Action) genericMethod.Invoke(null, typedActionAndParameters.Values.ToArray());
                }
            }

            throw new NotImplementedException($"TestFactory cannot create action for {test.GetType().Name}");
        }

        static bool TryGetTypedActionAndParameters(ITestInternal test, out Dictionary<Type, object> typedParameters)
        {
            typedParameters = new Dictionary<Type, object>();

            if (test.TryGetParameters(out var parameters))
            {
                var code = test.GetAction();

                typedParameters.Add(code.GetType(), code);

                foreach (var parameter in parameters)
                {
                    var type = parameter.GetType();

                    typedParameters.Add(type, parameter);
                }
            }

            return typedParameters.Any();
        }

        static Action CreateAction(AbstractTest<Action> test)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action();
                }
            };
        }

        static Action CreateAction<T1>(AbstractTest<Action<T1>> test, T1 parameter1)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action(parameter1);
                }
            };
        }

        static Action CreateAction<T1, T2>(AbstractTest<Action<T1, T2>> test, T1 parameter1, T2 parameter2)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action(parameter1, parameter2);
                }
            };
        }

        static Action CreateAction<T1, T2, T3>(AbstractTest<Action<T1, T2, T3>> test, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action(parameter1, parameter2, parameter3);
                }
            };
        }

        static Action CreateAction<T1, T2, T3, T4>(AbstractTest<Action<T1, T2, T3, T4>> test, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action(parameter1, parameter2, parameter3, parameter4);
                }
            };
        }

        static Action CreateAction<T1, T2, T3, T4, T5>(AbstractTest<Action<T1, T2, T3, T4, T5>> test, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            var action = test.Code;

            var iterations = test.Iterations;

            return () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    action(parameter1, parameter2, parameter3, parameter4, parameter5);
                }
            };
        }

    }
}