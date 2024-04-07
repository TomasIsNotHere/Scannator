using QRCoder;

namespace IosDeploy;

public partial class Add : ContentView
{
    private CarouselView _carouselView;

    private byte[] _QRcode;

    public Add(CarouselView carouselView)
    {
        InitializeComponent();
        _carouselView = carouselView;

    }

    private async void Add_Button_Clicked(System.Object sender, System.EventArgs e)
    {

        if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCategory.Text)
            || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtCount.Text))
        {
            await App.Current.MainPage.DisplayAlert("error", "can be empty or below 0", "OK");
            
            // || int.TryParse(txtCount.Text, out int result) || int.Parse(txtCount.Text) < 0)
        } else
        {

            string count = txtCount.Text;

            if (int.TryParse(count, out _))
            {
                string barcode = GenerateID(txtName.Text + txtCategory.Text);

                Item item = new Item
                {
                    name = txtName.Text,
                    category = txtCategory.Text,
                    description = txtDescription.Text,
                    count = int.Parse(txtCount.Text),
                    barcode = barcode
                };

                App.ItemHandler.Add(item);
                GenerateQR(barcode);
            } else
            {
                await App.Current.MainPage.DisplayAlert("error", "can be empty or below 0", "OK");
            }
        }
        
    }

    public string GenerateID(string sourceUrl)
    {
        return string.Format("{0}_{1:N}", sourceUrl, Guid.NewGuid());
    }


    public void GenerateQR(string barcode)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(barcode, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        _QRcode = qrCodeBytes;
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

        Button button = new Button
        {
            Text = "Share",
            WidthRequest = 150,
            HeightRequest = 60,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand,

        };

        layout.Children.Add(button);

        button.Clicked += (sender, args) => Share();
    }

    public async void Share()
    {
        var shareHandler = new ShareQrCode();

        await shareHandler.ShareQR(_QRcode);

        _carouselView.Position += -1;

    }
}
