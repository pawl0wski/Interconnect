namespace Repositories
{
    public interface IVirtualSwitchEntityRepository
    {
        public Task Create(string name, string bridge, Guid uuid);
        public Task CreateInvisible(string bridge, Guid uuid);
    }
}
