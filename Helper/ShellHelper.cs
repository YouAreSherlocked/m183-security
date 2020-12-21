using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace m183_shovel_knight_security.Helper
{
    public class ShellHelper
    {
        private readonly string[] _whitelistCommand = { "ls", "cat" };
        private readonly string[] _blacklistParameter = { "&", "<", ";", "'", "`", "|", "\""};
        public string Bash(string cmd)
        {
            try
            {
                if (!ValidateInput(cmd))
                {
                    return "Malicious command detected. Please give it up.";
                }

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

        private bool ValidateInput(string cmd)
        {
            try
            {
                string[] command = cmd.Split(" ");
                if (command.Length > 2) return false;
                if (!_whitelistCommand.Contains(command[0])) return false;
                if (command.Length == 2 && _blacklistParameter.Any(x => command[1].Contains(x))) return false;

                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
