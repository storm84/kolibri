using Kolibri.Api.Utils;

namespace Kolibri.Api.Contracts.v1.Responses;

public class PackageResponse(
    string kolliId,
    int weight,
    int length,
    int height,
    int width,
    bool isValid)
{
    public string KolliId { get; } = kolliId.ThrowIfNullOrWhiteSpace();
    public int Weight { get; } = weight.ThrowIfLessThan(0);
    public int Length { get; } = length.ThrowIfLessThan(0);
    public int Height { get; } = height.ThrowIfLessThan(0);
    public int Width { get; } = width.ThrowIfLessThan(0);
    public bool IsValid { get; } = isValid;
}
