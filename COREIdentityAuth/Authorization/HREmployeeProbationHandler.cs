using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace COREIdentityAuth.Authorization
{
    public class HREmployeeProbationHandler : AuthorizationHandler<HREmployeeProbation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HREmployeeProbation requirement)
        {
            if (context.User.HasClaim(x => x.Type == "EmploymentDate"))
            {
                var date = DateTime.Parse(context.User.FindFirst("EmploymentDate").Value);
                var period = DateTime.Now - date;
                if (period.Days > requirement.probationDays)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
