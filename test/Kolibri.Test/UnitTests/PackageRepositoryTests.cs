using Kolibri.Api.Exceptions;
using Kolibri.Api.Models;
using Kolibri.Api.Repositories;
using Kolibri.Test.Attributes;

namespace Kolibri.Test.UnitTests;

public class PackageRepositoryTests
{
    [Theory, AutoFake]
    public void Test_AddPackage_AddsSuccessfully(
        Package package,
        PackageRepository sut)
    {
        // Act
        var actual = sut.AddPackage(package);

        // Assert
        Assert.True(actual);
        var packageFromDictionary = sut.Packages[package.KolliId];
        Assert.Equal(package.KolliId, packageFromDictionary.KolliId);
        Assert.Equal(package.Weight, packageFromDictionary.Weight);
        Assert.Equal(package.Length, packageFromDictionary.Length);
        Assert.Equal(package.Height, packageFromDictionary.Height);
        Assert.Equal(package.Width, packageFromDictionary.Width);
    }

    [Theory, AutoFake]
    public void Test_AddPackage_AddExistingPackage_ReturnsFalse(
        Package package,
        PackageRepository sut)
    {
        // Arrange
        sut.AddPackage(package);

        // Act
        var actual = sut.AddPackage(package);

        // Assert
        Assert.False(actual);
    }

    [Theory, AutoFake]
    public void Test_GetPackage_Successfully(
        Package package,
        PackageRepository sut)
    {
        // Arrange
        sut.AddPackage(package);

        // Act
        var actual = sut.GetPackage(package.KolliId);

        // Assert
        Assert.Same(package, actual);
    }

    [Theory, AutoFake]
    public void Test_GetPackage_NotExistingKolliId_Throws(
        string kolliId,
        PackageRepository sut)
    {
        // Act
        var ex = Assert.Throws<PackageNotFoundException>(() => sut.GetPackage(kolliId));

        // Assert
        Assert.IsType<PackageNotFoundException>(ex);
    }

    [Theory, AutoFake]
    public void Test_GetAllPackages_ReturnsSuccessFully(
        List<Package> packages,
        PackageRepository sut)
    {
        // Arrange
        foreach (var package in packages)
        {
            sut.AddPackage(package);
        }

        // Act
        var actual = sut.GetAllPackages().ToList();

        // Arrange
        Assert.Equal(packages.Count, actual.Count);

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
