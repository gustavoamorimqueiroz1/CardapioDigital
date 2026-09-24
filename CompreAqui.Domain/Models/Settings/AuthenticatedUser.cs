using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CompreAqui.Domain.Models.Settings
{
	public class AuthenticatedUser
	{
		private readonly IHttpContextAccessor _accessor;
		

		public AuthenticatedUser(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public string UserName =>  
			_accessor.HttpContext.User.Identity.Name;

		public string Email => _accessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email)
				   .Select(c => c.Value).SingleOrDefault();
		public string Guid => _accessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.AuthenticationMethod)
				   .Select(c => c.Value).SingleOrDefault();

		public IEnumerable<Claim> GetClaimsIdentity()
		{
			return _accessor.HttpContext.User.Claims;
		}
	}
}
