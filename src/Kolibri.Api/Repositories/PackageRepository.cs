using System.Collections.Concurrent;
using Kolibri.Api.Exceptions;
using Kolibri.Api.Models;
using Kolibri.Api.Utils;

namespace Kolibri.Api.Repositories;

public interface IPackageRepository
{
    bool AddPackage(Package package);
    IEnumerable<Package> GetAllPackages();
    Package GetPackage(string kolliId);
}

public class PackageRepository : IPackageRepository
{
    public ConcurrentDictionary<string, Package> Packages { get; } = new();

    public bool AddPackage(Package package)
    {
        package.ThrowIfNull();
        return Packages.TryAdd(package.KolliId, package);
    }

    public IEnumerable<Package> GetAllPackages()
    {
        return Packages.Values;
    }

    public Package GetPackage(string kolliId)
    {
        kolliId.ThrowIfNullOrWhiteSpace();

        if (Packages.TryGetValue(kolliId, out var result))
        {
            return result;
        }

        throw new PackageNotFoundException(kolliId);
    }
}
