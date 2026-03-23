using Avalonia;
using Avalonia.Markup.Xaml;

namespace HomelabPulse.Desktop;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted() =>
        base.OnFrameworkInitializationCompleted();
}
