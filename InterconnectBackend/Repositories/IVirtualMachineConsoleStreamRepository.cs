using Models;

namespace Repositories
{
    /// <summary>
    /// Repository managing virtual machine console streams in memory.
    /// </summary>
    public interface IVirtualMachineConsoleStreamRepository
    {
        /// <summary>
        /// Adds a console stream to the repository.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        void Add(StreamInfo stream);
        
        /// <summary>
        /// Removes a console stream from the repository.
        /// </summary>
        /// <param name="stream">Stream information.</param>
        void Remove(StreamInfo stream);
        
        /// <summary>
        /// Retrieves a stream by virtual machine UUID.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Stream information or null.</returns>
        StreamInfo? GetByUuid(Guid uuid);
        
        /// <summary>
        /// Retrieves all opened streams.
        /// </summary>
        /// <returns>List of streams.</returns>
        List<StreamInfo> GetAllStreams();
    }
}
