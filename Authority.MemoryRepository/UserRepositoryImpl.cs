using Authority.Domain;
using Authority.Domain.Repositories;
using Domain;

namespace Authority.MemoryRepository
{
    public class UserRepositoryImpl : MemoryRepositoryImpl<User>, IUserRepository
    {
    }
}
