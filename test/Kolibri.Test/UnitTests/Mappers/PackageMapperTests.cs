using Kolibri.Api.Mappers;
using Kolibri.Api.Models;
using Kolibri.Test.Attributes;

namespace Kolibri.Test.UnitTests.Mappers;

public class PackageMapperTests
{
    [Theory, AutoFake]
    public void Tests_Map_Package_PackageResponse_MapsCorrect(
        Package package,
        PackageMapper sut)
    {
        // Act
        var actual = sut.Map(package);

        // Assert
        Assert.Equal(package.KolliId, actual.KolliId);
        Assert.Equal(package.Weight, actual.Weight);
        Assert.Equal(package.Length, actual.Length);
        Assert.Equal(package.Height, actual.Height);
        Assert.Equal(package.Width, actual.Width);
    }

    [Theory]
    [InlineAutoFake(1, 1, 1, 1, true)]
    [InlineAutoFake(20000, 60, 60, 60, true)]
    [InlineAutoFake(20001, 60, 60, 60, false)]
    [InlineAutoFake(20000, 61, 60, 60, false)]
    [InlineAutoFake(20000, 60, 61, 60, false)]
    [InlineAutoFake(20000, 60, 60, 61, false)]
    public void Test_Map_PackageResponse_IsValid_Is_Set_Correct(
        int weight,
        int length,
        int height,
        int width,
        bool expected,
        string kolliId,
        PackageMapper sut)
    {
        // Arrange
        var package = new Package(
            kolliId,
            weight,
            length,
            height,
            width);

        // Act
        var actual = sut.Map(package);

        // Assert
        Assert.Equal(expected, actual.IsValid);
    }

    [Theory, AutoFake]
    public void Test_Map_EnumerablePackage_EnumerablePackageResponse_MapsCorrect(
        List<Package> packages,
        PackageMapper sut)
    {
        // Act
        var actual = sut.Map(packages).ToList();

        // Assert
        Assert.Equal(packages.Count, actual.Count());

        foreach (var package in packages)
        {
            Assert.Contains(
                actual,
                x =>
                    x.KolliId == package.KolliId &&
                    x.Weight == package.Weight &&
                    x.Length == package.Length &&
                    x.Height == package.Height &&
                    x.Width == package.Width);
        }
    }
}
