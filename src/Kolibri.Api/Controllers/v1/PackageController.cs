using Kolibri.Api.Contracts.v1.Responses;
using Kolibri.Api.Mappers;
using Kolibri.Api.Repositories;
using Kolibri.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Kolibri.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class PackageController : ControllerBase
{
    public IPackageRepository PackageRepository { get; }
    public IPackageMapper PackageMapper { get; }

    public PackageController(IPackageRepository packageRepository, IPackageMapper packageMapper)
    {
        PackageMapper = packageMapper.ThrowIfNull();
        PackageRepository = packageRepository.ThrowIfNull();
    }

    [HttpGet(Name = nameof(GetAllPackages))]
    [ProducesResponseType<IEnumerable<PackageResponse>>(StatusCodes.Status200OK)]
    public IActionResult GetAllPackages()
    {
        var packages = PackageRepository.GetAllPackages();
        var response = PackageMapper.Map(packages);
        return Ok(response);
    }
}
