using UserManagementDomain;

namespace UserManagementService.authentication
{
    public interface TokenService
    {
        String generateToken(User user);
    }
}
