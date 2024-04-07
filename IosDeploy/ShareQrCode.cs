using System;
namespace IosDeploy
{
    public class ShareQrCode
    {
        public ShareQrCode()
        {
        }


        public async Task ShareQR(byte[] arr)
        {
            var path = FileSystem.Current.CacheDirectory;
            string fileName = "QRcode.png";
            var fullPath = Path.Combine(path, fileName);

            await File.WriteAllBytesAsync(fullPath, arr);

            string file = Path.Combine(FileSystem.CacheDirectory, fileName);

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Generated QR Code",
                File = new ShareFile(file)
            });
        }


    }
}

