using System.Drawing.Imaging;

var cb = new CaptchaBuilder(200, 100);
var dir = "captcha-test";

foreach (var file in Directory.EnumerateFiles(dir))
{
    File.Delete(file);
}

for (int i = 0; i < 50; i++)
{
    var captcha = cb.GetCaptcha(5);
    captcha.Image.Save($"{dir}/{captcha.Text}.jpg", ImageFormat.Jpeg);
}
