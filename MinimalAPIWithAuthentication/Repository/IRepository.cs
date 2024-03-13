using MinimalAPIWithAuthentication.Entities;

namespace MinimalAPIWithAuthentication.Repository
{
    public interface IRepository
    {
        User? Find(string name, string password);
    }
}