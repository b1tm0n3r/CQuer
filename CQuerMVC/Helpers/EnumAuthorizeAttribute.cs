using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CQuerMVC.Helpers
{
    public class EnumAuthorizeRoleAttribute : AuthorizeAttribute
    {
        public EnumAuthorizeRoleAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType == typeof(Enum)))
                Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }
    }
        
}