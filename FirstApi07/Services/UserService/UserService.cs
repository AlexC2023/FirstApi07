using FirstApi07.Authentication;
using FirstApi07.DataContext;
using FirstApi07.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Text;


namespace FirstApi07.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly ProgrammingClubDataContext _context;
        private readonly IConfiguration _configuration;
        public UserService(ProgrammingClubDataContext context) 
        {
        _context = context;
        }

        public UserService(ProgrammingClubDataContext context, IConfiguration configuration)
        {
            _context = context; _configuration = configuration;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Members.SingleOrDefault(x => x.IdMember == model.username && x.Name == model.Password);
            //return null if user not found
            if (user == null) return null;
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
            //helper methods
        private string generateJwtToken(MemberAccessException user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AUTHSECRET_AUTHSECRET"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("https://localhost:7029", "https://localhost:7029", null, expires: DateTime.Now.AddDays(3), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
         
        }

        private string generateJwtToken(Member user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration.GetValue<string>("Authentication:Domain"),
                _configuration.GetValue<string>("Authentication:Audience"), null, expires: DateTime.Now.AddDays(3), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
