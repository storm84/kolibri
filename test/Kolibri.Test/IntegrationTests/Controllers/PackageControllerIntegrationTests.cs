using System.Net;
using Kolibri.Api.Contracts.v1.Responses;
using Newtonsoft.Json;

namespace Kolibri.Test.IntegrationTests.Controllers;

[Collection(BootstrappedTestCollection.CollectionName)]
public class PackageControllerIntegrationTests(BootstrappedTestFixture bootstrappedTestFixture)
{
    private BootstrappedTestFixture BootstrappedTestFixture { get; } = bootstrappedTestFixture;

    [Fact]
    public async Task Test_GetAllPackages_Returns_Successfully()
    {
        // Arrange
        var client = BootstrappedTestFixture.CreateClient();

        // Act
        var response = await client.GetAsync("api/v1/Package");
        var responseString = await response.Content.ReadAsStringAsync();
        var packages = JsonConvert.DeserializeObject<List<PackageResponse>>(responseString);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(packages);
        // check if the seeded packages exist
        Assert.True(packages.Exists(x => x.KolliId == "999111111111111111"));
        Assert.True(packages.Exists(x => x.KolliId == "999222222222222222"));
    }
}
