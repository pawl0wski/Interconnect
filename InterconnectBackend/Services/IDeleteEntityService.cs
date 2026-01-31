namespace Services
{
    public interface IDeleteEntityService
    {
        public Task DeleteInternetEntity(int id);
        public Task DeleteVirtualMachineEntity(int id);
        public Task DeleteVirtualNetworkNodeEntity(int id);
    }
}
