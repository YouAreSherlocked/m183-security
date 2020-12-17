using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Helper
{
    public class ShellHelper
    {
        private readonly string[] _whitelistCommand = new string[] { "ls", "cat" };
        private readonly string[] _blacklistParameter = new string[] { "&", "<", ";", "'", "`", "|" };
        public string Bash(string cmd)
        {
            //TODO prevent shell injection attacks
            try
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
            } catch
            {
                return "The backend needs to be executed in a docker container";
            }
        }

        private string ValidateInput(string cmd)
        {
            string[] command = cmd.Split(" ");
            return "";
        }
    }
}
