using Raylib_cs;

public class Shop
{
    public Rectangle rect;
    public Rectangle hitBox;
    public Texture2D image;

    public Shop(string imgPath, Rectangle newRect)
    {
        image = Raylib.LoadTexture(imgPath);
        rect = new Rectangle(0, 0, 90, 90);
        hitBox = newRect;
    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)hitBox.x, (int)hitBox.y, Color.WHITE);
    }
}