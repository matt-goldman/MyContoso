using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace MyContoso.App;

public class TabBarIconShellHandler: ShellRenderer
{
    protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
    {
        return new TabBarIconBottomNavViewAppearanceTracker(this, shellItem.CurrentItem);
    }
}