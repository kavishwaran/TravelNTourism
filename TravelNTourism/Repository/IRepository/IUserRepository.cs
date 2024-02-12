using TravelNTourism.Model.Dto;

namespace TravelNTourism.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginrequestdto);
        Task<UserDTO> Register(RegistrationRequestDTO registrationrequestdto); 
    }
}
