namespace Ordina.FileReading
{
    internal class NullClaimsRepository : IClaimsRepository
    {
        public bool CurrentUserHas(string readContentClaim)
        {
            return true;
        }
    }
}