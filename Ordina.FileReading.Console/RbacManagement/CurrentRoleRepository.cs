using System.Collections.Generic;
using System.Linq;

namespace Ordina.FileReading.Console
{
    internal class CurrentRoleRepository : IClaimsRepository
    {
        private readonly string _wantedRole;
        private readonly List<ClaimRole> _currentClaimRoles;

        private class ClaimRole
        {
            public string Claim { get; set; }
            public string Role { get; set; }

        }
        public CurrentRoleRepository(string _wantedRole)
        {
            this._wantedRole = _wantedRole;
            _currentClaimRoles = new List<ClaimRole>();
            _currentClaimRoles.Add(new ClaimRole() { Claim = ClaimTypes.ReadContentClaim, Role = "Admin" });
            _currentClaimRoles.Add(new ClaimRole() { Claim = ClaimTypes.ReadContentClaim, Role = "Read" });
            _currentClaimRoles.Add(new ClaimRole() { Claim = "", Role = "Write" });

        }

        public bool CurrentUserHas(string readContentClaim)
        {
            var values = _currentClaimRoles.Where(x => x.Claim == readContentClaim && _wantedRole == x.Role).ToList();
            return values.Any();

        }
    }
}