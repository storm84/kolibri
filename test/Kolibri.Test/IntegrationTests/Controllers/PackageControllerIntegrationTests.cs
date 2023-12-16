using System.Net;
using Kolibri.Api.Constants;
using Kolibri.Api.Contracts.v1.Responses;
using Kolibri.Test.Attributes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kolibri.Test.IntegrationTests.Controllers;

[Collection(BootstrappedTestCollection.CollectionName)]
public class PackageControllerIntegrationTests(BootstrappedTestFixture bootstrappedTestFixture)
{
    private BootstrappedTestFixture BootstrappedTestFixture { get; } = bootstrappedTestFixture;
    private const string packageUrlBasePath = "api/v1/Package";

    [Fact]
    public async Task Test_GetAllPackages_Returns_Successfully()
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.GetAsync(packageUrlBasePath);
        var responseString = await response.Content.ReadAsStringAsync();
        var packages = JsonConvert.DeserializeObject<List<PackageResponse>>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(packages);
        // check if the seeded packages exist
        Assert.True(packages.Exists(x => x.KolliId == "999111111111111111"));
        Assert.True(packages.Exists(x => x.KolliId == "999222222222222222"));
    }

    [Fact]
    public async Task Test_GetPackage_KolliId_Valid_Returns_Ok()
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();
        var kolliId = "999111111111111111";

        // Act
        var response = await client.GetAsync($"{packageUrlBasePath}/{kolliId}");
        var responseString = await response.Content.ReadAsStringAsync();
        var package = JsonConvert.DeserializeObject<PackageResponse>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(package);
        Assert.Equal(kolliId, package.KolliId);
    }

    [Fact]
    public async Task Test_GetPackage_KolliId_NotExist_Returns_NotFound()
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();
        var kolliId = "999111111111111112";

        // Act
        var response = await client.GetAsync($"{packageUrlBasePath}/{kolliId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineAutoFake("99911111111111111")]
    [InlineAutoFake("9991111111111111122")]
    public async Task Test_GetPackage_KolliId_InvalidLength_Returns_BadRequest(string kolliId)
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.GetAsync($"{packageUrlBasePath}/{kolliId}");
        var responseString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(PackageConstants.ErrorMessageKolliIdInvalidLength, problemDetails?.Errors.First().Value.First());
    }

    [Theory]
    [InlineAutoFake("929111111111111111")]
    [InlineAutoFake("777111111111111111")]
    [InlineAutoFake("999ABC111111111111")]
    public async Task Test_GetPackage_KolliId_InvalidFormat_Returns_BadRequest(string kolliId)
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.GetAsync($"{packageUrlBasePath}/{kolliId}");
        var responseString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(PackageConstants.ErrorMessageKolliIdInvalidFormat, problemDetails?.Errors.First().Value.First());
    }
}
