using System.Xml.Linq;
using QRCoder;

namespace IosDeploy;

public partial class Edit : ContentPage
{
    private Item _item;
    private byte[] _QRcode;

    public Edit(Item item)
    {
        InitializeComponent();
        _item = item;
        RenderText();


    }

    public void RenderText()
    {
        IdL.Text = _item.id.ToString();
        nameL.Text = _item.name;
        categoryL.Text = _item.category;
        descriptionL.Text = _item.description;
        countL.Text = _item.count.ToString();
        GenerateQR();
    }


    public void GenerateQR()
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(_item.barcode, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        _QRcode = qrCodeBytes;
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }


    private async void deleteB_Clicked(System.Object sender, System.EventArgs e)
    {
        App.ItemHandler.Delete(_item);
        await Navigation.PopAsync();
    }

    private void minusB_Clicked(System.Object sender, System.EventArgs e)
    {
        _item.count -= 1;
        RenderText();
    }

    private void plusB_Clicked(System.Object sender, System.EventArgs e)
    {
        _item.count += 1;
        RenderText();
    }

    private async void saveB_Clicked(System.Object sender, System.EventArgs e)
    {
        _item.name = nameL.Text;
        _item.category = categoryL.Text;
        _item.description = descriptionL.Text;

        if (string.IsNullOrEmpty(nameL.Text) || string.IsNullOrEmpty(categoryL.Text) || string.IsNullOrEmpty(descriptionL.Text) || _item.count < 0){
            await DisplayAlert("error", "can be empty or below 0", "OK");
        } else
        {
            _item.name = nameL.Text;
            _item.category = categoryL.Text;
            _item.description = descriptionL.Text;

            App.ItemHandler.Update(_item);
            await Navigation.PopAsync();
        }
        

    }

    private async void ShareBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        var shareHandler = new ShareQrCode();

        await shareHandler.ShareQR(_QRcode);
    }
}
