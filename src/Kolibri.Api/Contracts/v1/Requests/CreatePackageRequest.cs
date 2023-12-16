using System.ComponentModel.DataAnnotations;
using Kolibri.Api.Constants;

namespace Kolibri.Api.Contracts.v1.Requests;

public class CreatePackageRequest(
    string kolliId,
    int weight,
    int length,
    int height,
    int width)
{
    [StringLength(
        PackageConstants.KolliIdLength,
        MinimumLength = PackageConstants.KolliIdLength,
        ErrorMessage = PackageConstants.ErrorMessageKolliIdInvalidLength)]
    [RegularExpression(
        PackageConstants.KolliIdRegExPattern,
        ErrorMessage = PackageConstants.ErrorMessageKolliIdInvalidFormat)]
    public string KolliId { get; } = kolliId;

    [Range(0, PackageConstants.MaxWeight)]
    public int Weight { get; } = weight;

    [Range(0, PackageConstants.MaxSideLength)]
    public int Length { get; } = length;

    [Range(0, PackageConstants.MaxSideLength)]
    public int Height { get; } = height;

    [Range(0, PackageConstants.MaxSideLength)]
    public int Width { get; } = width;
}
