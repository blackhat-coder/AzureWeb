using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COREIdentityAuth.Authorization
{
    public class HREmployeeProbation : IAuthorizationRequirement
    {
        public int probationDays { get; set; }

        public HREmployeeProbation(int probationDays)
        {
            this.probationDays = probationDays;
        }
    }
}
