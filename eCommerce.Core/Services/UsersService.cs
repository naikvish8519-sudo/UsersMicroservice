using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using Microsoft.Extensions.Logging;

namespace eCommerce.Core.Services;

internal class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersService> _logger;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, ILogger<UsersService> logger)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        try
        {
            ApplicationUser? user = await _usersRepository
                .GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

            if (user == null)
                return null;

            return _mapper.Map<AuthenticationResponse>(user) with
            {
                Success = true,
                Token = "token"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during Login for {Email}", loginRequest.Email);
            return null;
        }
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        try
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);
            ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

            if (registeredUser == null)
                return null;

            return _mapper.Map<AuthenticationResponse>(registeredUser) with
            {
                Success = true,
                Token = "token"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during Register for {Email}", registerRequest.Email);
            return null;
        }
    }

    public async Task<UserInfo?> GetUserByIdAsync(Guid userId)
    {
        try
        {
            if (userId == Guid.Empty)
                return null;

            ApplicationUser? user = await _usersRepository.GetUserById(userId);

            if (user == null)
                return null;

            return _mapper.Map<UserInfo>(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving User by ID: {UserId}", userId);
            return null;
        }
    }
}
