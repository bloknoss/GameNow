using GameNow.Domain.Entities;
using GameNow.Server.Dtos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameNow.Server.Helpers
{
	public class JwtService
	{
		private IConfiguration _configuration;
		private string? secureKey;

		public JwtService(IConfiguration configuration)
		{
			this._configuration = configuration;
			this.secureKey = _configuration.GetSection("Jwt:Key").Value;
		}

		public string Generate(User user)
		{
			IEnumerable<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

			var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), issuer: _configuration.GetSection("Jwt:Issuer").Value, audience: _configuration.GetSection("Jwt:Audience").Value, signingCredentials: signingCred);

			string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

			return tokenString;
		}

		public JwtSecurityToken Verify(string jwt)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secureKey);
			tokenHandler.ValidateToken(jwt, new TokenValidationParameters
			{
				ValidateActor = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				RequireExpirationTime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _configuration.GetSection("Jwt:Issuer").Value,
				ValidAudience = _configuration.GetSection("Jwt:Audience").Value,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value))
			}, out SecurityToken validatedToken);

			return (JwtSecurityToken)validatedToken;
		}

	}
}
