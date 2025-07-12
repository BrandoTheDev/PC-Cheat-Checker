🕵️‍♂️ PC Integrity Self-Check Tool

    A fully transparent, offline anti-cheat scanning tool for personal use and tournament self-checks.
    ✅ No internet access
    ✅ No installation
    ✅ No hidden tracking

🔍 What It Does

This tool scans your system for signs of known game cheats, mod menus, suspicious processes, and cheat-related domains stored in browser history (like Firefox's IndexedDB).

It performs:

    🔎 Running process inspection

    🔎 Registry scan (HKCU\Software)

    🔎 File system scan (AppData, Downloads, Firefox storage)

    ✅ Saves a readable report locally (e.g., PC_Integrity_Report_20250712_1430.txt)

⚠️ Transparency & Disclaimer

    This tool is 100% offline — nothing is sent, uploaded, or logged externally.

    No software is installed; it's a simple .exe file you can delete after use.

    Suspicious results are not proof of cheating.
    Some false positives may occur, especially with tools used in programming, reverse engineering, modding, or game development (e.g., dnSpy, IDA, x64dbg, etc.).

    ⚠️ Always review flagged items manually.
    This tool does not take action — it only reports.

✅ Usage Instructions

    Download the latest release

    Run the .exe (Windows only, no installation needed)

    Wait for the scan to complete

    A text report will be created in the same folder (e.g., PC_Integrity_Report_*.txt)

    Open the file and review any flagged entries

💡 Intended Use Cases

    eSports tournament check-in

    Self-check before playing competitive games

    LAN party verification

    Modding communities verifying tool usage transparency

📁 Example Output

    PC Integrity Report - 2025-07-12
    ------------------------------------------------------------
    [Running Processes]
    ✔ Suspicious Process: cheatengine64
    
    [Registry: HKCU\Software]
    ✔ Suspicious Registry Key: Cheat Engine
    
    [File Scan: AppData + Downloads + Firefox Storage]
    ✔ Suspicious File: C:\Users\Name\AppData\Roaming\CheatTool\loader.dll
    ✔ Suspicious File: C:\Users\Name\AppData\Roaming\Mozilla\Firefox\Profiles\...\storage\default\https+++gamehacking.org\ls\data.sqlite

🔒 Trust & Source Code

    This tool is 100% open-source and designed for full transparency.
