﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Helper
{
    public class ShellHelper
    {
        public string Bash(string cmd)
        {
            string filesFolder = "cd Files &&";
            string escapedArgs = $"{filesFolder} {cmd.Replace("\"", "\\\"")}";
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}
