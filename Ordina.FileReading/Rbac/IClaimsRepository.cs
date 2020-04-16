namespace Ordina.FileReading
{
    public interface IClaimsRepository
    {
        bool CurrentUserHas(string readContentClaim);
    }
}