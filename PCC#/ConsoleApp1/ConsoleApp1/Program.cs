using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dllPath = @"D:\Anaconda\python311.dll";
            string pythonHomePath = @"D:\Anaconda";
            // 对应python内的重要路径
            string[] py_paths = {"DLLs", "lib", "lib/site-packages", "lib/site-packages/win32"
                , "lib/site-packages/win32/lib", "lib/site-packages/Pythonwin" };
            string pySearchPath = $"{pythonHomePath};";
            foreach (string p in py_paths)
            {
                pySearchPath += $"{pythonHomePath}/{p};";
            }

            // 此处解决BadPythonDllException报错
            Runtime.PythonDLL = dllPath;
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", dllPath);
            // 配置python环境搜索路径解决PythonEngine.Initialize() 崩溃
            PythonEngine.PythonHome = pythonHomePath;
            PythonEngine.PythonPath = pySearchPath;

            PythonEngine.Initialize();
        }
    }
}
