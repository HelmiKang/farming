using Raylib_cs;

public class Bags
{
    public Rectangle rect;
    public Rectangle hitBox;
    public Texture2D image;
    public bool equipped;
    public int seeds;

    public Bags(string imgPath, Rectangle newRect, int numSeeds)
    {
        image = Raylib.LoadTexture(imgPath);
        rect = new Rectangle(0, 0, 90, 90);
        hitBox = newRect;
        seeds = numSeeds;
    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)hitBox.x, (int)hitBox.y, Color.WHITE);
        Raylib.DrawText($"{seeds}x", (int)hitBox.x, (int)hitBox.y, 25, Color.WHITE);
        if (equipped)
        {
            DrawEquipped();
        }
    }
    public void DrawEquipped()
    {
        Raylib.DrawRectangleLinesEx(hitBox, 2, Color.GRAY);
    }
}