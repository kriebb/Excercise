using System;
using System.Security;
using System.Security.Principal;

namespace Ordina.FileReading
{
    public class DefaultRbacService : IRbacService
    {
        private readonly IClaimsRepository _claimsRepository;

        public DefaultRbacService(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository ?? throw new ArgumentNullException(nameof(claimsRepository));
        }
        public void ThrowWhenCantReadContent(string path)
        {
            if (!_claimsRepository.CurrentUserHas(ClaimTypes.ReadContentClaim))
                throw new SecurityException($"Current user { WindowsIdentity.GetCurrent().Name } doesn't have the right to read the content of the path {path}");
        }
    }
}