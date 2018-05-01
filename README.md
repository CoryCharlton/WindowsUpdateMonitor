# Windows Update Monitor

Windows Update Monitor is a simple application to monitor and delete the following registry keys: 

- HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\DisableDualScan
- HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU\NoAutoUpdate

If the keys are detected it will also trigger a Windows Update check for updates.

## Why would I need this?

I was working in an environment where a group policy was configured to deny Windows Updates but I needed them on my development machine. Thus I created this to make my life simpler.