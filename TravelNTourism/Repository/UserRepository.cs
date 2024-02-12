using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelNTourism.Data;
using TravelNTourism.Model;
using TravelNTourism.Model.Dto;
using TravelNTourism.Repository.IRepository;

namespace TravelNTourism.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper _mapper;
        private string SecretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration,
           UserManager<ApplicationUser> usermanager ,IMapper mapper, RoleManager<IdentityRole> rolemanager)
        {
            _db = db;
            _usermanager = usermanager;
            _mapper = mapper;
            _rolemanager = rolemanager;
            SecretKey = configuration.GetValue<string>("APISettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
            if (user==null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginrequestdto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginrequestdto.Username.ToLower());
            bool isvalid = await _usermanager.CheckPasswordAsync(user, loginrequestdto.Password);
            
            if (user== null|| isvalid==false)
            {
                return  new LoginResponseDTO()
                {
                    Token = "",
                    User = null

                };
            }
            var roles = await _usermanager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginresponsedto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(Token),
                User = _mapper.Map<UserDTO>(user),
                Role =roles.FirstOrDefault(),

            };
            return loginresponsedto;


        }

        public  async Task<UserDTO> Register(RegistrationRequestDTO registrationrequestdto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationrequestdto.UserName,
                Email = registrationrequestdto.UserName,
                NormalizedEmail = registrationrequestdto.UserName.ToUpper(),
                Name = registrationrequestdto.Name
               
            };
            try
            {
                var result = await _usermanager.CreateAsync(user, registrationrequestdto.Password);
                if (result.Succeeded)
                {
                    if (!_rolemanager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _rolemanager.CreateAsync(new IdentityRole("admin"));
                        await _rolemanager.CreateAsync(new IdentityRole("customer"));
                    }
                    await _usermanager.AddToRoleAsync(user, "admin");
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationrequestdto.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);
                }
            }
            catch (Exception)
            {

                 
            }

            return new UserDTO();
        }
    }
}
