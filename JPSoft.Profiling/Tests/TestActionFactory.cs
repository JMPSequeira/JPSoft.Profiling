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
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(m => m.Name == "CreateAction")
                .ToDictionary(m => m.GetGenericArguments().Length);
        }

        public static Action Create(ITestInternal test) => CreateActionDinamically(test);

        static Action CreateActionDinamically(ITestInternal test)
        {
            var typedParameters = GetTypedParameters(test);

            if (_actionCreators.TryGetValue(typedParameters.Count(), out var method))
            {
                if (method.IsGenericMethod)
                    method = method.MakeGenericMethod(typedParameters.Select(a => a.Type).ToArray());

                var arguments = new List<object>(typedParameters.Select(a => a.Object));

                arguments.Insert(0, test);

                return (Action) method.Invoke(null, arguments.ToArray());
            }

            throw new NotImplementedException($"TestFactory cannot create action for {test.GetType().Name}");
        }

        static List<(Type Type, object Object)> GetTypedParameters(ITestInternal test)
        {
            var typedParameters = new List<(Type, object)>();

            if (test.TryGetParameters(out var parameters))
            {
                foreach (var parameter in parameters)
                {
                    var type = parameter.GetType();

                    typedParameters.Add((type, parameter));
                }
            }

            return typedParameters;
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