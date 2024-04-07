namespace IosDeploy;

public partial class App : Application
{
    public static ItemHandler ItemHandler { get; private set; }

    public App(ItemHandler itemHandler)
    {
        InitializeComponent();

        MainPage = new AppShell();

        ItemHandler = itemHandler;
    }
}

