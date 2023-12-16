namespace Kolibri.Api.Constants;

public static class PackageConstants
{
    public const int MaxWeight = 20 * 1000;
    public const int MaxSideLength = 60;

    public const int KolliIdLength = 18;
    public const string KolliIdRegExPattern = @"^999\d+";

    public const string ErrorMessageKolliIdInvalidLength = "KolliId must be exactly 18 characters long";
    public const string ErrorMessageKolliIdInvalidFormat = "KolliId must start with 999 and contain only digits";
}
