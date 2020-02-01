using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DotNetGitLabWebHookToMatterMost.Business.Check
{
    /// <summary>
    /// 格式化文件
    /// </summary>
    public class FileFormatChecker
    {
        public FileFormatChecker(IReadOnlyList<FileInfo> fileList, FileInfo csprojFile)
        {
            FileList = fileList;
            CsprojFile = csprojFile;
        }

        public FileInfo CsprojFile { get; }

        public IReadOnlyList<FileInfo> FileList { get; }

        /// <summary>
        /// 尝试格式化文件
        /// </summary>
        public void FormatFile()
        {
            // dotnet format xx
            var fileList = string.Join(",", FileList.Select(file => file.FullName));

            var processStartInfo = new ProcessStartInfo("dotnet")
            {
                ArgumentList =
                {
                    "format",
                    "--workspace",
                    CsprojFile.FullName,
                    "--files",
                    fileList
                }
            };

            Process.Start(processStartInfo)?.WaitForExit();
        }
    }
}