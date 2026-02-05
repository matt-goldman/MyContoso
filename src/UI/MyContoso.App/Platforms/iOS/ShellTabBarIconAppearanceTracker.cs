using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace MyContoso.App;

public class ShellTabBarIconAppearanceTracker : ShellTabBarAppearanceTracker
{
    public override void UpdateLayout(UITabBarController controller)
    {
        base.UpdateLayout(controller);
        ApplySelection(controller);
    }

    public override void SetAppearance(UITabBarController controller, ShellAppearance appearance)
    {
        base.SetAppearance(controller, appearance);
        ApplySelection(controller);
    }

    private static void ApplySelection(UITabBarController controller)
    {
        var tabBar = controller.TabBar;
        if (tabBar?.Items == null)
            return;

        var selectedIndex = controller.SelectedIndex;

        for (var i = 0; i < tabBar.Items.Length; i++)
        {
            var item = tabBar.Items[i];

            if (i == selectedIndex)
            {
                // Hide the title - we'll render it as part of the combined image
                item.SetTitleTextAttributes(new UIStringAttributes { ForegroundColor = UIColor.Clear }, UIControlState.Normal);
                item.SetTitleTextAttributes(new UIStringAttributes { ForegroundColor = UIColor.Clear }, UIControlState.Selected);

                if (item.Image != null)
                {
                    item.SelectedImage = CreateSelectedImageWithText(item.Image, item.Title);
                }
            }
            else
            {
                // Show normal title for unselected tabs
                item.SetTitleTextAttributes(new UIStringAttributes { ForegroundColor = UIColor.SecondaryLabel }, UIControlState.Normal);
                item.SetTitleTextAttributes(new UIStringAttributes { ForegroundColor = UIColor.SecondaryLabel }, UIControlState.Selected);

                if (item.Image != null)
                {
                    item.SelectedImage = item.Image;
                }
            }
        }
    }

    private static readonly Dictionary<string, UIImage> CombinedCache = new();

    private static UIImage CreateSelectedImageWithText(UIImage icon, string? title)
    {
        var cacheKey = $"{icon.Handle}_{title}";
        if (CombinedCache.TryGetValue(cacheKey, out var cached))
            return cached;

        var image = RenderSelectedImageWithText(icon, title);
        CombinedCache[cacheKey] = image;
        return image;
    }

    private static UIImage RenderSelectedImageWithText(UIImage icon, string? title)
    {
        var font = UIFont.SystemFontOfSize(10, UIFontWeight.Medium);
        var textAttributes = new UIStringAttributes
        {
            Font = font,
            ForegroundColor = UIColor.White
        };

        var textSize = string.IsNullOrEmpty(title)
            ? CGSize.Empty
            : new NSString(title).GetSizeUsingAttributes(textAttributes);

        var horizontalPadding = 12f;
        var iconTextSpacing = 2f;
        var topPadding = 6f;
        var bottomPadding = 4f;
        var topOffset = 16f; // Transparent space above the pill to push it down

        var pillWidth = (nfloat)Math.Max(icon.Size.Width + horizontalPadding * 2, textSize.Width + horizontalPadding * 2);
        var pillHeight = icon.Size.Height + iconTextSpacing + textSize.Height + topPadding + bottomPadding;
        var cornerRadius = 12f;

        var size = new CGSize(pillWidth, pillHeight + topOffset);
        var renderer = new UIGraphicsImageRenderer(size);

        var image = renderer.CreateImage(ctx =>
        {
            var pillRect = new CGRect(0, topOffset, pillWidth, pillHeight);

            // Draw pill background
            UIColor.SystemBlue.SetFill();
            UIBezierPath.FromRoundedRect(pillRect, cornerRadius).Fill();

            // Draw icon centered horizontally, tinted white
            var iconY = topOffset + topPadding;
            var iconRect = new CGRect(
                (size.Width - icon.Size.Width) / 2,
                iconY,
                icon.Size.Width,
                icon.Size.Height
            );

            // Properly tint the icon white using CGContext
            var cgContext = ctx.CGContext;
            cgContext.SaveState();
            cgContext.TranslateCTM(iconRect.X, iconRect.Y + iconRect.Height);
            cgContext.ScaleCTM(1, -1); // Flip for correct orientation
            cgContext.ClipToMask(new CGRect(0, 0, iconRect.Width, iconRect.Height), icon.CGImage);
            UIColor.White.SetFill();
            cgContext.FillRect(new CGRect(0, 0, iconRect.Width, iconRect.Height));
            cgContext.RestoreState();

            // Draw text centered horizontally, below the icon
            if (!string.IsNullOrEmpty(title))
            {
                var textPoint = new CGPoint(
                    (size.Width - textSize.Width) / 2,
                    iconY + icon.Size.Height + iconTextSpacing
                );

                var attributedString = new NSAttributedString(title, new UIStringAttributes
                {
                    Font = font,
                    ForegroundColor = UIColor.White,
                    StrokeWidth = 2
                });
                attributedString.DrawString(textPoint);
            }
        });

        // Return as AlwaysOriginal to prevent iOS from applying template tinting
        return image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
    }
}
