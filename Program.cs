using System.IO;
using System.Web;
using static System.Console;

namespace FilenameURLDecode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WriteLine("即将进行URL解码...");
                WriteLine();
                if (args.Length > 0)
                {
                    DoUrlDecode(args);
                }
                else
                {
                    WriteLine("没有拖入文件或文件夹，请直接将需要转码的文件或文件夹拖动到执行程序上运行");
                }

            }
            catch (System.Exception e)
            {
                WriteLine(e.Message);
                WriteLine("发生错误");
            }
            finally
            {
                WriteLine("点击任意键退出...");
                ReadKey();
            }
        }

        static void DoUrlDecode(string[] args)
        {
            foreach (var path in args)
            {
                if (File.Exists(path))
                {
                    //当输入的路径是文件时
                    FileInfo fileInfo = new FileInfo(path);
                    var newName = HttpUtility.UrlDecode(path);
                    if (newName != path)
                    {
                        File.Move(path, newName);
                        WriteLine($"重命名文件\"{path}\"\n为\"{newName}\"");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine($"跳过\"{path}\"文件");
                        WriteLine();
                    }

                }
                else if (Directory.Exists(path))
                {
                    //当输入的路径是文件夹时
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    var newName = HttpUtility.UrlDecode(path);
                    if (newName != path)
                    {
                        Directory.Move(path, newName);
                        WriteLine($"重命名文件夹\"{path}\"\n为\"{newName}\"");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine($"跳过\"{path}\"文件夹");
                        WriteLine();
                    }

                    foreach (var file in directoryInfo.GetFiles())
                    {
                        string[] myStr = new string[1];
                        myStr[0] = file.FullName;
                        DoUrlDecode(myStr);
                    }

                    foreach (var directory in directoryInfo.GetDirectories())
                    {
                        string[] myStr = new string[1];
                        myStr[0] = directory.FullName;
                        DoUrlDecode(myStr);
                    }

                }
            }
        }
    }
}
