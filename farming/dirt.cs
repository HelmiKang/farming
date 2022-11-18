using Raylib_cs;

public class Dirt
{
    public Rectangle rect;
    public Texture2D image;

    public int state = 0;
    
    public float timer = 0;


    public Dirt(Texture2D i)
    {
        image = i;
        rect = new Rectangle(0, 0, 90, 90);

    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)rect.x, (int)rect.y, Color.WHITE);
    }

}
