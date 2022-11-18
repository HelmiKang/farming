using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(720, 800, "farm");
Raylib.SetTargetFPS(60);

Texture2D dirtImage = Raylib.LoadTexture("dirt.png");
Texture2D seeds = Raylib.LoadTexture("seeds.png");
Texture2D sprout = Raylib.LoadTexture("sprout.png");
Texture2D carrot1 = Raylib.LoadTexture("carrot1.png");
Texture2D carrot2 = Raylib.LoadTexture("carrot2.png");
Texture2D carrot3 = Raylib.LoadTexture("carrot3.png");
Texture2D carrot4 = Raylib.LoadTexture("carrot4.png");

Rectangle farmRect = new Rectangle(0, 0, 720, 540);




Bags bag = new Bags();
Dirt[,] dirts = new Dirt[8, 6];
{
    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            Dirt d = new Dirt(dirtImage);
            d.rect.x = 90 * x;
            d.rect.y = 90 * y;

            dirts[x, y] = d;
        }
    }
}


while (Raylib.WindowShouldClose() == false)
{
    // logik
    Vector2 mousePos = Raylib.GetMousePosition();

    // Bags and equipment:
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, bag.rect))
    {
        bag.carrotEquipped = true;
    }

    // Placing seeds:
    if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) &&
        Raylib.CheckCollisionPointRec(mousePos, farmRect))
    {
        {
            int x = (int)mousePos.X / 90;
            int y = (int)mousePos.Y / 90;

            if (dirts[x, y].state == 0)
            {
                dirts[x, y].state++;
                dirts[x, y].image = seeds;
            }
        }
    }
    //Update seeds:
    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            dirts[x, y].timer += Raylib.GetFrameTime();
            if (dirts[x, y].timer > 5 && dirts[x, y].state != 0)
            {
                if (dirts[x, y].state < 5)
                {
                    dirts[x, y].state++;
                }
                dirts[x, y].timer = 0;
            }

            if (dirts[x, y].state == 2)
            {
                dirts[x, y].image = sprout;
            }
            if (dirts[x, y].state == 3)
            {
                dirts[x, y].image = carrot1;
            }
            if (dirts[x, y].state == 3)
            {
                dirts[x, y].image = carrot2;
            }
            if (dirts[x, y].state == 4)
            {
                dirts[x, y].image = carrot3;
            }
            if (dirts[x, y].state == 5)
            {
                dirts[x, y].image = carrot4;
            }
        }
    }
    // grafik
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.GRAY);
    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            dirts[x, y].Draw();
        }
    }
    Raylib.EndDrawing();
}

Raylib.CloseWindow();
