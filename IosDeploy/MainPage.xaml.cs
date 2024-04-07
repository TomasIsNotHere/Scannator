
namespace IosDeploy;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        var cameraPage = new Scan(carouselView);
        var listPage = new List();
        var addPage = new Add(carouselView);
        var connectPage = new Connect();
        var pages = new List<ContentView> { cameraPage, listPage, addPage, connectPage };


        //List<string> pages = new List<string>() { "camera", "list", "add", "connect" };
        // Set the collection as the ItemsSource of the CarouselView
        carouselView.ItemsSource = pages;
    }

    private void CameraButton_Clicked(object sender, EventArgs e)
    {
        carouselView.Position = 0; // Navigate to the first page
    }

    private void ListButton_Clicked(object sender, EventArgs e)
    {
        carouselView.Position = 1; // Navigate to the second page
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        carouselView.Position = 2; // Navigate to the third page
    }

    private void ConnectButton_Clicked(object sender, EventArgs e)
    {
        carouselView.Position = 3; // Navigate to the fourth page
    }
}


