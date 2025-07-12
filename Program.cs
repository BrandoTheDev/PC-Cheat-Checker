using System.Diagnostics;
using Microsoft.Win32;
using System.Text;

namespace PCIntegrityCheckTool
{
    class Program
    {
        static readonly string[] SuspiciousKeywords = {
            "cheatengine", "aimbot", "wallhack", "esp", "unlockall",
            "hwidspoof", "cheatloader", "dllinjector", "reclass", "modmenu",
            "spoofer", "autohotkey", "processhacker", "dnspy", "x64dbg", "ida"
        };

        static readonly string[] KnownCheatDomains = {
            "gamehacking.org", "gamehacking.academy", "unknowncheats.me",
            "cheatengine.org", "guidedhacking.com", "mpgh.net", "cracked.to",
            "wearedevs.net", "interwebz.cc", "aimware.net", "iwantcheats.net",
            "elitepvpers.com", "x22cheats.com", "r6cheats.com", "cheatgg.net",
            "cshacks.net", "ph33r.net", "cheat-master.ru", "systemcheats.net"
        };

        static readonly string[] SafeDomains = {
            "youtube.com", "microsoft.com", "google.com", "facebook.com",
            "twitter.com", "reddit.com", "duckduckgo.com", "instagram.com",
            "bing.com", "tiktok.com", "linkedin.com"
        };

        static readonly string[] WhitelistedPaths = {
            @"\AppData\Roaming\npm\",
            @"\AppData\Roaming\.minecraft\",
            @"\node_modules\",
            @"\pnpm\",
            @"\npm\",
            @"\npm-registry-fetch\",
            @"\postcss-selector-parser\"
        };

        static readonly string[] Whitelist = {
            "dllhost", "easyanticheat", "cheatcheckertool", "svchost", "explorer",
            "chrome", "steam", "discord", "teams", "runtimebroker", "msedgewebview2"
        };

        static readonly string ReportPath = $"PC_Integrity_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        static void Main()
        {
            Console.Title = "PC Integrity Self-Check Tool";
            Console.WriteLine("=== PC Integrity Self-Check Tool ===");
            Console.WriteLine("This tool scans your PC for known cheat tools (files, processes, registry keys, browser traces).");
            Console.WriteLine("No data is uploaded or shared. Results are saved as a local text file.");
            Console.WriteLine("------------------------------------------------------------\n");

            var report = new StringBuilder();
            report.AppendLine($"PC Integrity Report - {DateTime.Now}");
            report.AppendLine("------------------------------------------------------------");

            ScanProcesses(report);
            ScanRegistry(report);
            ScanFiles(report);

            File.WriteAllText(ReportPath, report.ToString());
            Console.WriteLine($"\nScan complete. Report saved as: {ReportPath}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void ScanProcesses(StringBuilder report)
        {
            report.AppendLine("\n[Running Processes]");
            try
            {
                foreach (var proc in Process.GetProcesses())
                {
                    string name = proc.ProcessName.ToLower();
                    if (IsSuspicious(name))
                    {
                        report.AppendLine($"Suspicious Process: {proc.ProcessName}");
                    }
                }
            }
            catch (Exception ex)
            {
                report.AppendLine($"Error scanning processes: {ex.Message}");
            }
        }

        static void ScanRegistry(StringBuilder report)
        {
            report.AppendLine("\n[Registry: HKCU\\Software]");
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software"))
                {
                    foreach (var subKey in key.GetSubKeyNames())
                    {
                        string sub = subKey.ToLower();
                        if (IsSuspicious(sub))
                        {
                            report.AppendLine($"Suspicious Registry Key: HKCU\\Software\\{subKey}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                report.AppendLine($"Error scanning registry: {ex.Message}");
            }
        }

        static void ScanFiles(StringBuilder report)
        {
            report.AppendLine("\n[File Scan: AppData + Downloads + Firefox Storage]");
            string[] paths = {
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
            };

            foreach (var basePath in paths)
            {
                try
                {
                    var files = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories)
                                         .Where(f => IsSuspicious(f))
                                         .Take(300);

                    foreach (var file in files)
                    {
                        report.AppendLine($"✔ Suspicious File: {file}");
                    }
                }
                catch (Exception ex)
                {
                    report.AppendLine($"⚠ Error scanning {basePath}: {ex.Message}");
                }
            }
        }

        static bool IsSuspicious(string input)
        {
            string lower = input.ToLower();

            bool matchesKeyword = SuspiciousKeywords.Any(kw => lower.Contains(kw));
            bool matchesCheatDomain = KnownCheatDomains.Any(domain => lower.Contains(domain));
            bool matchesSafeDomain = SafeDomains.Any(safe => lower.Contains(safe));
            bool whitelisted = WhitelistedPaths.Any(wl => lower.Contains(wl)) || Whitelist.Any(wl => lower.Contains(wl));

            return (matchesKeyword || matchesCheatDomain) && !matchesSafeDomain && !whitelisted;
        }
    }
}
