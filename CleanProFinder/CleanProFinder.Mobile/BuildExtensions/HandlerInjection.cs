namespace CleanProFinder.Mobile.BuildExtensions;

internal static class HandlerInjection
{
    internal static void ConfigureHandlers(this MauiAppBuilder builder)
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });

        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
    }
}