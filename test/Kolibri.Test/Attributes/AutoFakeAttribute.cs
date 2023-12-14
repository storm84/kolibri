using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using AutoFixture.Xunit2;

namespace Kolibri.Test.Attributes;

public class AutoFakeAttribute() : AutoDataAttribute(
    () =>
    {
        var fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        return fixture;
    });
