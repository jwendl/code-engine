using CodeEngine.Interfaces;
using CodeEngine.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CodeEngine.Services
{
    public class FileService
        : IFileService
    {
        public async Task<FileServiceResult> FetchFileContentsAsync<TInput>(TInput location)
        {
            if (location is FileInfo)
            {
                return await FetchFileContentsAsync(location as FileInfo);
            }
            else if (location is Uri)
            {
                return await FetchFileContentsAsync(location as Uri);
            }

            throw new InvalidOperationException($"FetchFileContentAsync did not implement for location of type {typeof(TInput)}");
        }

        private async Task<FileServiceResult> FetchFileContentsAsync(FileInfo location) => new FileServiceResult()
        {
            FileExtension = location.Extension,
            FileContents = await Task.FromResult(File.ReadAllText(location.FullName)),
        };

        private async Task<FileServiceResult> FetchFileContentsAsync(Uri location)
        {
            var webClient = new WebClient();
            var fileContents = await webClient.DownloadStringTaskAsync(location);

            string escapedUriString = $"{location.Scheme}{Uri.SchemeDelimiter}{location.Authority}{location.AbsolutePath}";
            string fileExtension = Path.GetExtension(escapedUriString);

            return new FileServiceResult()
            {
                FileContents = fileContents,
                FileExtension = fileExtension,
            };
        }
    }
}
