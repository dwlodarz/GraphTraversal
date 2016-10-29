using System;
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
        void UploadGraphData<T>(T content) where T : class;
    }
}
