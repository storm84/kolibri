using Kolibri.Api.Constants;
using Kolibri.Api.Contracts.v1.Requests;
using Kolibri.Api.Contracts.v1.Responses;
using Kolibri.Api.Models;
using Kolibri.Api.Utils;

namespace Kolibri.Api.Mappers;

public interface IPackageMapper
{
    PackageResponse Map(Package package);
    IEnumerable<PackageResponse> Map(IEnumerable<Package> packages);
    Package Map(CreatePackageRequest packageRequest);
}

public class PackageMapper : IPackageMapper
{
    public PackageResponse Map(Package package)
    {
        return new PackageResponse(
            package.KolliId,
            package.Weight,
            package.Length,
            package.Height,
            package.Width,
            IsPackageValid(package));
    }

    public IEnumerable<PackageResponse> Map(IEnumerable<Package> packages)
    {
        var packageList = packages.ThrowIfNull().ToList();
        return packageList.Select(Map);
    }

    private bool IsPackageValid(Package package)
        => package.Weight <= PackageConstants.MaxWeight &&
           package.Length <= PackageConstants.MaxSideLength &&
           package.Height <= PackageConstants.MaxSideLength &&
           package.Width <= PackageConstants.MaxSideLength;

    public Package Map(CreatePackageRequest packageRequest)
    {
        return new Package(
            packageRequest.KolliId,
            packageRequest.Weight,
            packageRequest.Length,
            packageRequest.Height,
            packageRequest.Width);
    }
}
