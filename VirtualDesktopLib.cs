// Common interface across all versions

using System;
using Microsoft.Win32;
using Windows11.VirtualDesktop;

namespace VirtualDesktop.VirtualDesktop {
    public static class VirtualDesktopManager {
        public static VirtualDesktopWrapper Create() {
            string releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                "CurrentBuildNumber", "").ToString();

            if (!int.TryParse(releaseId, out int buildNumber)) {
                throw new Exception("Unrecognized Windows version");
            }

            // Work out the proper desktop wrapper
            if (buildNumber >= 22621) return new VirtualDesktopWrapperWin11();
            if (buildNumber == 22000) return new VirtualDesktopWrapperWin11_21H2();
            if (buildNumber >= 17763 && buildNumber <= 19044)
                return new VirtualDesktopWrapperWin10_1809_21H2();
            if (buildNumber >= 14393 && buildNumber <= 16299)
                return new VirtualDesktopWrapperWin10_1607_1709();
            if (buildNumber == 17134) return new VirtualDesktopWrapperWin10_1803();

            throw new Exception("Unsupported Windows version");
        }
    }

    public interface VirtualDesktopWrapper {
        void JumpTo(int index);
    }

    internal class VirtualDesktopWrapperWin11 : VirtualDesktopWrapper {
        public void JumpTo(int index) {
            if (index >= Desktop.Count) return;

            Desktop.FromIndex(index).MakeVisible();
        }
    }

    internal class VirtualDesktopWrapperWin11_21H2 : VirtualDesktopWrapper {
        public void JumpTo(int index) {
            if (index >= Windows11_21H2.VirtualDesktop.Desktop.Count) return;

            Windows11_21H2.VirtualDesktop.Desktop.FromIndex(index).MakeVisible();
        }
    }

    internal class VirtualDesktopWrapperWin10_1809_21H2 : VirtualDesktopWrapper {
        public void JumpTo(int index) {
            if (index >= Windows10_1809_21H2.VirtualDesktop.Desktop.Count) return;

            Windows10_1809_21H2.VirtualDesktop.Desktop.FromIndex(index).MakeVisible();
        }
    }

    internal class VirtualDesktopWrapperWin10_1803 : VirtualDesktopWrapper {
        public void JumpTo(int index) {
            if (index >= Windows10_1803.VirtualDesktop.Desktop.Count) return;

            Windows10_1803.VirtualDesktop.Desktop.FromIndex(index).MakeVisible();
        }
    }

    internal class VirtualDesktopWrapperWin10_1607_1709 : VirtualDesktopWrapper {
        public void JumpTo(int index) {
            if (index >= Windows10_1607_1709.VirtualDesktop.Desktop.Count) return;
            
            Windows10_1607_1709.VirtualDesktop.Desktop.FromIndex(index).MakeVisible();
        }
    }
}