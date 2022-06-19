using System.Drawing;
using System.Text;

public class CaptchaBuilder
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    int fontSize;
    StringBuilder text;
    Font font;
    Random random;
    Bitmap bmp;
    Graphics g;
    Brush fontBrush;

    public CaptchaBuilder(int width, int height)
    {
        text = new StringBuilder();
        random = new Random();
        bmp = new Bitmap(width, height);
        g = Graphics.FromImage(bmp);
        fontSize = height / 2;
        font = new Font("Consolas", fontSize, FontStyle.Bold);
        fontBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
    }

    public Captcha GetCaptcha(int charCount = 4)
    {
        text.Clear();
        g.Clear(Color.LightGray);

        for (int i = 0; i < charCount; i++)
            text.Append(chars[random.Next(0, chars.Length)]);

        for (int i = 0; i < charCount; i++)
        {
            int x1 = i * (bmp.Width / charCount);
            int x2 = bmp.Width - bmp.Width * (charCount - i - 1) / charCount - fontSize/2;

            int x = random.Next(x1, x2) - bmp.Width/20;
            int y = random.Next(bmp.Height / 5, bmp.Height - bmp.Height / 5 - fontSize);

            g.RotateTransform(random.Next(-10, 10));

            g.DrawString(text[i].ToString(), font, fontBrush, x, y);
            
            g.ResetTransform();
        }

        Pen pen = new Pen(Color.FromArgb(64, Color.Black), fontSize/10);

        Point[] curvePoints = new Point[3];
        int curvesCount = 3;

        for (int i = 0; i < curvesCount; i++)
        {
            for (int j = 0; j < curvePoints.Length; j++)
                curvePoints[j] = new Point(random.Next(-40, bmp.Width+40), random.Next(-40, bmp.Height+40));

            g.DrawCurve(pen, curvePoints);
        }

        return new Captcha() { Image = bmp, Text = text.ToString()};
    }
}

public class Captcha
{
    public Bitmap Image { get; set; }
    public string Text { get; set; }
}