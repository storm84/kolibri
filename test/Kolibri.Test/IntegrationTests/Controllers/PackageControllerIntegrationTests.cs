using System.Net;
using System.Text;
using Kolibri.Api.Constants;
using Kolibri.Api.Contracts.v1.Requests;
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

    [Fact]
    public async Task Test_CreatePackage_Successfully()
    {
        // Arrange
        var request = new CreatePackageRequest(
            "999123456789012345",
            2000,
            20,
            30,
            40);
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.PostAsync(packageUrlBasePath, SerializeRequest(request));

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var packageResponse = await client.GetAsync($"{packageUrlBasePath}/{request.KolliId}");
        var responseString = await packageResponse.Content.ReadAsStringAsync();
        var package = JsonConvert.DeserializeObject<PackageResponse>(responseString);
        Assert.NotNull(package);
        Assert.Equal(request.KolliId, package.KolliId);
        Assert.Equal(request.Weight, package.Weight);
        Assert.Equal(request.Height, package.Height);
        Assert.Equal(request.Length, package.Length);
        Assert.Equal(request.Width, package.Width);
    }

    [Theory]
    [InlineAutoFake("929111111111111111")]
    [InlineAutoFake("777111111111111111")]
    [InlineAutoFake("999ABC111111111111")]
    public async Task Test_CreatePackage_KolliId_InvalidFormat_Returns_BadRequest(string kolliId)
    {
        // Arrange
        var request = new CreatePackageRequest(
            kolliId,
            2000,
            20,
            30,
            40);
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.PostAsync(packageUrlBasePath, SerializeRequest(request));
        var responseString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(PackageConstants.ErrorMessageKolliIdInvalidFormat, problemDetails?.Errors.First().Value.First());
    }

    [Theory]
    [InlineAutoFake("99911111111111111")]
    [InlineAutoFake("9991111111111111122")]
    public async Task Test_CreatePackage_KolliId_InvalidLength_Returns_BadRequest(string kolliId)
    {
        // Arrange
        var request = new CreatePackageRequest(
            kolliId,
            2000,
            20,
            30,
            40);
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.PostAsync(packageUrlBasePath, SerializeRequest(request));
        var responseString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(PackageConstants.ErrorMessageKolliIdInvalidLength, problemDetails?.Errors.First().Value.First());
    }

    [Theory]
    [InlineAutoFake(20001, 60, 60, 60, "The field Weight must be between 0 and 20000.")]
    [InlineAutoFake(20000, 61, 60, 60, "The field Length must be between 0 and 60.")]
    [InlineAutoFake(20000, 60, 61, 60, "The field Height must be between 0 and 60.")]
    [InlineAutoFake(20000, 60, 60, 61, "The field Width must be between 0 and 60.")]
    [InlineAutoFake(-1, 60, 60, 60, "The field Weight must be between 0 and 20000.")]
    [InlineAutoFake(20000, -1, 60, 60, "The field Length must be between 0 and 60.")]
    [InlineAutoFake(20000, 60, -1, 60, "The field Height must be between 0 and 60.")]
    [InlineAutoFake(20000, 60, 60, -1, "The field Width must be between 0 and 60.")]
    public async Task Test_CreatePackage_invalid_dimension_Returns_BadRequest(
        int weight,
        int length,
        int height,
        int width,
        string expectedErrorMessage)
    {
        // Arrange
        var request = new CreatePackageRequest(
            "999111111111111113",
            weight,
            length,
            height,
            width);
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.PostAsync(packageUrlBasePath, SerializeRequest(request));
        var responseString = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(expectedErrorMessage, problemDetails?.Errors.First().Value.First());
    }

    private static StringContent SerializeRequest<T>(T request)
        where T : class
    {
        var requestContent = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            "application/json");
        return requestContent;
    }
}
