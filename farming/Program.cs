using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1160, 720, "farm");
Raylib.SetTargetFPS(60);

//färger
Color Purple = new Color(104, 56, 108, 255);
Color Magenta = new Color(159, 73, 128, 255);

int dabloons = 0;

Texture2D dirtImage = Raylib.LoadTexture("dirt.png");

Bags carrots = new Bags("bagcarrot.png", new Rectangle(24, 140, 90, 90), 10, "carrots", 0, new Rectangle(24, 416, 90, 90), "iconcarrot.png");
Bags cabbage = new Bags("bagcabbage.png", new Rectangle(116, 140, 90, 90), 0, "cabbage", 0, new Rectangle(116, 416, 90, 90), "iconcabbage.png");
Bags pumpkin = new Bags("bagpumpkin.png", new Rectangle(24, 241, 90, 90), 0, "pumpkin", 0, new Rectangle(24, 517, 90, 90), "iconpumpkin.png");

Shop carrotsbuy = new Shop("smallcarroticon.png", new Rectangle(960, 140, 150, 40));
Shop cabbagebuy = new Shop("smallcabbageicon.png", new Rectangle(960, 210, 150, 40));
Shop pumpkinbuy = new Shop("smallpumpkinicon.png", new Rectangle(960, 270, 150, 40));

Shop carrotssell = new Shop("smallcarroticon.png", new Rectangle(960, 416, 150, 40));
Shop cabbagesell = new Shop("smallcabbageicon.png", new Rectangle(960, 486, 150, 40));
Shop pumpkinsell = new Shop("smallpumpkinicon.png", new Rectangle(960, 546, 150, 40));

Info infobox = new Info();

Vector2 offset = new Vector2(220, 90);

// Skapa farmen med en 2 dimensionel array
// detta låter mig skapa ett rutnät med kordinatsystem 
// array eftersom jag vet den exakta storleken jag vill ha på min jordyta
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

Bags currentBag = null;

while (Raylib.WindowShouldClose() == false)
{
    // logik

    Vector2 mousePos = Raylib.GetMousePosition();

    // Bags
    // När man trycker på en bag blir den equipped
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, carrots.hitBox))
    {
        currentBag = carrots;

    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, cabbage.hitBox))
    {
        currentBag = cabbage;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, pumpkin.hitBox))
    {
        currentBag = pumpkin;
    }

    // buy seeds
    // Kollar om man har råd, lägger till ett seed och tar bort dabloons 
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
    // kollar att man mer än en crop, tar bort en crop och ger dabloons 
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, carrotssell.hitBox) && (carrots.crops >= 1))
    {
        carrots.crops -= 1;
        dabloons += 7;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, cabbagesell.hitBox) && (cabbage.crops >= 1))
    {
        cabbage.crops -= 1;
        dabloons += 14;
    }
    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) &&
        Raylib.CheckCollisionPointRec(mousePos, pumpkinsell.hitBox) && (pumpkin.crops >= 1))
    {
        pumpkin.crops -= 1;
        dabloons += 28;
    }


    // Placing seeds
    // tar muspositionen, bestämmer type, sätter dess dirts state till 1, timer till 0, och tar bort ett frö
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
        Raylib.CheckCollisionPointRec(mousePos, farmRect))
    {
        int x = (int)(mousePos.X - offset.X) / 90;
        int y = (int)(mousePos.Y - offset.Y) / 90;

        if (dirts[x, y].state == 0)
        {
            dirts[x, y].timer = 0;
            if (currentBag != null && currentBag.seeds > 0)
            {
                dirts[x, y].type = currentBag.typeName;
                currentBag.seeds = currentBag.seeds - 1;
                dirts[x, y].state = 1;
            }
        }

        // harvest
        // När den är fullväxt lägger den till en crop och nollställer state
        else if (dirts[x, y].state == 5)
        {
            if (dirts[x, y].type == "carrots")
            {
                carrots.crops++;
                dirts[x, y].state = 0;
            }
            if (dirts[x, y].type == "cabbage")
            {
                cabbage.crops++;
                dirts[x, y].state = 0;
            }
            if (dirts[x, y].type == "pumpkin")
            {
                pumpkin.crops++;
                dirts[x, y].state = 0;
            }
        }
    }

    // Inforuta
    // medans muspekarentrycker på frågetecknet ska inforutan visas, sedan försvinna om man trycker igen
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) &&
        Raylib.CheckCollisionPointRec(mousePos, infobox.hitbox))

    {
        if (infobox.openInfo)
        {
            infobox.openInfo = false;
        }

        else
            infobox.openInfo = true;
    }

    //Update seeds
    // loopar genom alla dirts och kör update metoden
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

    // ritar dirts
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

    if(currentBag == carrots)
    {
    carrots.DrawEquipped();
    }
     if(currentBag == pumpkin)
    {
    pumpkin.DrawEquipped();
    }
     if(currentBag == cabbage)
    {
    cabbage.DrawEquipped();
    }

    if (infobox.openInfo)
    {
        infobox.InfoDraw();
    }
    infobox.Draw();


    Raylib.EndDrawing();
}

Raylib.CloseWindow();
