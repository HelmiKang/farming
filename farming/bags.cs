using Raylib_cs;

public class Bags
{
    public Rectangle rect;
    public Rectangle hitBox;
    public Texture2D image;
    public bool equipped;
    public int seeds;
    public string typeName;
    public int crops;
    public Rectangle text;
    public Texture2D iconImage;

    public Bags(string imgPath, Rectangle newRect, int numSeeds, string type, int numcrops, Rectangle textrec, string iconImgPath)
    {
        image = Raylib.LoadTexture(imgPath);
        rect = new Rectangle(0, 0, 90, 90);
        hitBox = newRect;
        seeds = numSeeds;
        typeName = type;
        crops = numcrops;
        text = textrec;
        iconImage = Raylib.LoadTexture(iconImgPath);


    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)hitBox.x, (int)hitBox.y, Color.WHITE);
        Raylib.DrawText($"{seeds}x", (int)hitBox.x, (int)hitBox.y, 25, Color.WHITE);
        Raylib.DrawText($"{crops}x", (int)text.x, (int)text.y, 25, Color.WHITE);
        Raylib.DrawTexture(iconImage, (int)text.x, (int)text.y, Color.WHITE);
    }
    public void DrawEquipped()
    {
        Raylib.DrawRectangleLinesEx(hitBox, 2, Color.GRAY);
    }
}