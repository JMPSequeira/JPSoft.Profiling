using System;
using JPSoft.Profiling;
using NUnit.Framework;

[TestFixture]
public partial class TestActionFactoryTest
{
    [Test]
    public void Create_ActionNoParams_ActionOfSameType()
    {
        var test = new Test(() => { });

        var action = TestActionFactory.Create(test);

        Assert.IsInstanceOf(typeof(Action), action);
    }

    [Test]
    public void Create_ActionWithParams_ActionOfSameType()
    {
        Action<int, string> testAction = (i, s) => s = s + i;

        var test = new Test<int, string>(testAction);

        test.InsertParameter("Gello");

        test.InsertParameter(2);

        var action = TestActionFactory.Create(test);

        Assert.IsInstanceOf(typeof(Action), action);
    }
}