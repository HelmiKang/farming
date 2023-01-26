using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1160, 720, "farm");
Raylib.SetTargetFPS(60);

Color Purple = new Color(104, 56, 108, 255);
Color Magenta = new Color(159, 73, 128, 255);

int dabloons = 0;


Texture2D dirtImage = Raylib.LoadTexture("dirt.png");



Bags carrots = new Bags("bagcarrot.png", new Rectangle(24, 140, 90, 90), 10);
Bags cabbage = new Bags("bagcabbage.png", new Rectangle(116, 140, 90, 90), 0);
Bags pumpkin = new Bags("bagpumpkin.png", new Rectangle(24, 241, 90, 90), 0);

Icons carrotscrop = new Icons("iconcarrot.png", new Rectangle(24, 416, 90, 90), 0);
Icons cabbagecrop = new Icons("iconcabbage.png", new Rectangle(116, 416, 90, 90), 0);
Icons pumpkincrop = new Icons("iconpumpkin.png", new Rectangle(24, 517, 90, 90), 0);

Shop carrotsbuy = new Shop("smallcarroticon.png", new Rectangle(960, 140, 150, 40));
Shop cabbagebuy = new Shop("smallcabbageicon.png", new Rectangle(960, 210, 150, 40));
Shop pumpkinbuy = new Shop("smallpumpkinicon.png", new Rectangle(960, 270, 150, 40));

Shop carrotssell = new Shop("smallcarroticon.png", new Rectangle(960, 416, 150, 40));
Shop cabbagesell = new Shop("smallcabbageicon.png", new Rectangle(960, 486, 150, 40));
Shop pumpkinsell = new Shop("smallpumpkinicon.png", new Rectangle(960, 546, 150, 40));





Vector2 offset = new Vector2(220, 90);

// Skapa farmen
Dirt[,] dirts = new Dirt[8, 6];
{
    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            Dirt d = new Dirt(dirtImage);
            d.rect.x = 90 * x;
            d.rect.y = 90 * y;
            d.rect.x += offset.X;
            d.rect.y += offset.Y;

            dirts[x, y] = d;
        }
    }
}

Rectangle farmRect = new Rectangle(offset.X, offset.Y, 90 * dirts.GetLength(0), 90 * dirts.GetLength(1));

while (Raylib.WindowShouldClose() == false)
{
    // logik

    Vector2 mousePos = Raylib.GetMousePosition();

    // Bags
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, carrots.hitBox))
    {
        carrots.equipped = true;
        cabbage.equipped = false;
        pumpkin.equipped = false;

    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, cabbage.hitBox))
    {
        cabbage.equipped = true;
        carrots.equipped = false;
        pumpkin.equipped = false;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, pumpkin.hitBox))
    {
        pumpkin.equipped = true;
        cabbage.equipped = false;
        carrots.equipped = false;
    }

    // buy seeds
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, carrotsbuy.hitBox) && (dabloons >= 5))
    {
        carrots.seeds++;
        dabloons -= 5;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, cabbagebuy.hitBox) && (dabloons >= 10))
    {
        cabbage.seeds++;
        dabloons -= 10;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, pumpkinbuy.hitBox) && (dabloons >= 20))
    {
        pumpkin.seeds++;
        dabloons -= 20;
    }

    // Sell seeds
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, carrotssell.hitBox) && (carrotscrop.crops >= 1))
    {
        carrotscrop.crops -= 1;
        dabloons += 7;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, cabbagesell.hitBox) && (cabbagecrop.crops >= 1))
    {
        cabbagecrop.crops -= 1;
        dabloons += 14;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, pumpkinsell.hitBox) && (pumpkincrop.crops >= 1))
    {
        pumpkincrop.crops -= 1;
        dabloons += 28;
    }


    // Placing seeds:
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
        Raylib.CheckCollisionPointRec(mousePos, farmRect))
    {


        int x = (int)(mousePos.X - offset.X) / 90;
        int y = (int)(mousePos.Y - offset.Y) / 90;

        if (dirts[x, y].state == 0)
        {
           dirts[x, y].timer = 0; 
            if (carrots.equipped && carrots.seeds > 0)
            {
                dirts[x, y].type = "carrot";
                carrots.seeds = carrots.seeds - 1;
                dirts[x, y].state = 1;

            }
            else if (cabbage.equipped && cabbage.seeds > 0)
            {
                dirts[x, y].type = "cabbage";
                cabbage.seeds = cabbage.seeds - 1;
                dirts[x, y].state = 1;

            }
            else if (pumpkin.equipped && pumpkin.seeds > 0)
            {
                dirts[x, y].type = "pumpkin";
                pumpkin.seeds = pumpkin.seeds - 1;
                dirts[x, y].state = 1;

            }
        }
        // harvest
        else if (dirts[x, y].state == 5)
        {
            if (dirts[x, y].type == "carrot")
            {
                carrotscrop.crops++;
                dirts[x, y].state = 0;
            }
            if (dirts[x, y].type == "cabbage")
            {
                cabbagecrop.crops++;
                dirts[x, y].state = 0;
            }
            if (dirts[x, y].type == "pumpkin")
            {
                pumpkincrop.crops++;
                dirts[x, y].state = 0;
            }



        }
    }
    //Update seeds:
    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            dirts[x, y].Update();

        }
    }
    // grafik
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Purple);


    Raylib.DrawRectangle(15, 90, 190, 263, Magenta);
    Raylib.DrawText("Seeds", 65, 91, 25, Color.WHITE);

    Raylib.DrawRectangle(15, 366, 190, 263, Magenta);
    Raylib.DrawText("Crops", 65, 367, 25, Color.WHITE);

    Raylib.DrawRectangle(955, 90, 190, 263, Magenta);
    Raylib.DrawText("Shop", 1015, 91, 25, Color.WHITE);

    Raylib.DrawRectangle(955, 366, 190, 263, Magenta);
    Raylib.DrawText("Sell", 1020, 367, 25, Color.WHITE);

    for (int y = 0; y < dirts.GetLength(1); y++)
    {
        for (int x = 0; x < dirts.GetLength(0); x++)
        {
            dirts[x, y].Draw();
        }
    }

    carrots.Draw();
    cabbage.Draw();
    pumpkin.Draw();

    carrotscrop.Draw();
    cabbagecrop.Draw();
    pumpkincrop.Draw();

    Raylib.DrawText($"dabloons: {dabloons}", 10, 670, 25, Color.WHITE);

    carrotsbuy.Draw();
    Raylib.DrawText("cost: 5 dabloons", 1000, 155, 17, Color.WHITE);
    cabbagebuy.Draw();
    Raylib.DrawText("cost: 10 dabloons", 1000, 215, 17, Color.WHITE);
    pumpkinbuy.Draw();
    Raylib.DrawText("cost: 20 dabloons", 1000, 280, 17, Color.WHITE);

    carrotssell.Draw();
    Raylib.DrawText("earn: 7 dabloons", 1000, 431, 17, Color.WHITE);
    cabbagesell.Draw();
    Raylib.DrawText("earn: 14 dabloons", 1000, 491, 17, Color.WHITE);
    pumpkinsell.Draw();
    Raylib.DrawText("earn: 28 dabloons", 1000, 556, 17, Color.WHITE);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
