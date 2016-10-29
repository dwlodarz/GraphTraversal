using System;
using System.Threading.Tasks;
namespace GraphTraversal.DataLoader.Http
{
    /// <summary>
    /// File service uploader.
    /// </summary>
    public interface IFileServiceUploader
    {
        /// <summary>
        /// Uplaod the graph data.
        /// </summary>
        Task UploadGraphData<T>(T content) where T : class;
    }
}
