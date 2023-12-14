using AutoFixture.Xunit2;

namespace Kolibri.Test.Attributes;

public class InlineAutoFakeAttribute : CompositeDataAttribute
{
    public InlineAutoFakeAttribute(params object[] values)
        : base(new InlineDataAttribute(values), new AutoFakeAttribute())
    {
    }
}
