

namespace IosDeploy;

public partial class Scan : ContentView
{
    private CarouselView _carouselView;

	public Scan(CarouselView carouselView)
	{
		InitializeComponent();
        _carouselView = carouselView;
        barcodeReader.CameraLocation = ZXing.Net.Maui.CameraLocation.Rear;
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions()
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
        };
	}

    private void BarcodeReader_BarcodesDetected(System.Object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var first = e.Results[0];
        Dispatcher.Dispatch(() =>
        {
            
            Item item = App.ItemHandler.GetItemByCode(first.Value);
            if(item.name == "IsNotThere")
            {
                Barcodetxt.Text = $"code: {first.Value} / format : {first.Format} / not in database";
            }
            else
            {
                barcodeReader.IsDetecting = false;
                ScanBtn.Text = "Start scanning";
                Redirect(item);
                Barcodetxt.Text = "";
            }
        });

    }

    public async void Redirect(Item item)
    {
        await Navigation.PushAsync(new Edit(item));
    }

    void ScanBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        if(barcodeReader.IsDetecting == true)
        {
            barcodeReader.IsDetecting = false;
            ScanBtn.Text = "Start scanning";
        } else
        {
            barcodeReader.IsDetecting = true;
            ScanBtn.Text = "Stop scanning";
        }
    }
}
