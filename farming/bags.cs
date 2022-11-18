using Raylib_cs;

public class Bags
{
    public Rectangle rect;
    public Texture2D image;

    public bool carrotEquipped;

    public Bags()
    {
        image = Raylib.LoadTexture("bagcarrot.png");
        rect = new Rectangle(0, 0, 70, 70);
    }

    public void Draw()
    {
        Raylib.DrawTexture(image, 100, 700, Color.WHITE);
    }
}
