using Raylib_cs;

public class Dirt
{
    public Rectangle rect;
    public Texture2D image;

    static Texture2D[] carrotStates = {
        Raylib.LoadTexture("carrot1.png"),
        Raylib.LoadTexture("carrot2.png"),
        Raylib.LoadTexture("carrot3.png"),
    };

    static Texture2D[] cabbageStates = {
        Raylib.LoadTexture("cabbage1.png"),
        Raylib.LoadTexture("cabbage2.png"),
        Raylib.LoadTexture("cabbage3.png"),
    };

    static Texture2D[] pumpkinStates = {
        Raylib.LoadTexture("pumpkin1.png"),
        Raylib.LoadTexture("pumpkin2.png"),
        Raylib.LoadTexture("pumpkin3.png"),
    };

    static Texture2D seeds = Raylib.LoadTexture("seeds.png");
    static Texture2D sprout = Raylib.LoadTexture("sprout.png");

    static Texture2D dirtimage = Raylib.LoadTexture("dirt.png");


    public int state = 0;

    public float timer = 0;

    public string type;

    // sort
    // speed

    public void Update()
    {
        timer += Raylib.GetFrameTime();
        if (timer > 5 && state != 0)
        {
            if (state < 5)
            {
                state++;
            }
            timer = 0;
        }

         if (state == 0)
        {
            image = dirtimage;
        }
        if (state == 1)
        {
            image = seeds;
        }
        if (state == 2)
        {
            image = sprout;
        }
        else if (state > 2)
        {
            if (type == "carrot")
            {
                image = carrotStates[state - 3];
            }
            if (type == "cabbage")
            {
                image = cabbageStates[state - 3];
            }
            if (type == "pumpkin")
            {
                image = pumpkinStates[state - 3];
            }
        }

    }


    public Dirt(Texture2D i)
    {
        image = i;
        rect = new Rectangle(0, 0, 90, 90);
        dirtimage = i;
    }

    public void Draw()
    {
        Raylib.DrawTexture(image, (int)rect.x, (int)rect.y, Color.WHITE);
    }

}
