namespace Kolibri.Test;

[CollectionDefinition(CollectionName)]
public class BootstrappedTestCollection : ICollectionFixture<BootstrappedTestFixture>
{
    public const string CollectionName = "Bootstrapped test collection";
}
