using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MDLoader
{
    class MFiles
    {
        /// <summary>
        /// 复制文件到指定目录
        /// </summary>
        /// <param name="srcFile"></param>  
        /// <param name="destFile"></param>  
        /// <returns></returns>
        public static void CopyFile(string srcFile, string destFile)
        {
            DirectoryInfo destDirectory = new DirectoryInfo(System.IO.Path.GetDirectoryName(destFile));
            string fileName = Path.GetFileName(srcFile);
            if (!File.Exists(srcFile))
            {
                return;
            }

            if (!destDirectory.Exists)
            {
                destDirectory.Create();
            }

            File.Copy(srcFile, destFile, true);

        }
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>  
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir, true); //删除已空文件夹                 
            }
        }
    }
}
