namespace Repositories
{
    public interface IVirtualMachineConsoleDataRepository
    {
        public void AddDataToConsole(Guid uuid, byte[] data);
        public List<byte> GetData(Guid uuid);
    }
}
