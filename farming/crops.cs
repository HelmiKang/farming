using Raylib_cs;

public class Icons
{
    public Rectangle rect;
    public Rectangle hitBox;
    public Texture2D image;
    public bool equipped;
    public int crops;

    public Icons(string imgPath, Rectangle newRect, int numCrops)
    {
        image = Raylib.LoadTexture(imgPath);
        rect = new Rectangle(0, 0, 90, 90);
        hitBox = newRect;
        crops = numCrops;
    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)hitBox.x, (int)hitBox.y, Color.WHITE);
        Raylib.DrawText($"{crops}x", (int)hitBox.x, (int)hitBox.y, 25, Color.WHITE);
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