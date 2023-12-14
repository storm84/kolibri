namespace Kolibri.Api.Exceptions;

public class PackageNotFoundException : Exception
{
    public PackageNotFoundException(string kolliId)
        : base($"Package with KolliId {kolliId} could not be found")
    {
    }
}
