using Raylib_cs;

public class Info
{
    public bool openInfo;
    public Rectangle hitbox = new Rectangle(1080, 15, 60, 60);
    public Color Magenta = new Color(159, 73, 128, 255);

    // Rita ruta med frågetecken
    public void Draw()
    {
        Raylib.DrawRectangle((int)hitbox.x, (int)hitbox.y, 60, 60, Color.BLACK);
        Raylib.DrawText("?", 1100, 30, 35, Color.WHITE);
    }
    // Ska ritas när man trycker på rutan med frågetecken
    public void InfoDraw()
    {
        Raylib.DrawRectangle(0, 0, 1160, 720, Magenta);
        Raylib.DrawText("> PLANTING: Equip a bag of seeds in the SEEDS window by pressing on a bag.", 20, 120, 25, Color.WHITE);
        Raylib.DrawText("Press on the dirt field to place a seed and wait for it to grow.", 20, 150, 25, Color.WHITE);

        Raylib.DrawText("> HARVESTING: Press on a fully grown crop to harvest it.", 20, 250, 25, Color.WHITE);
        Raylib.DrawText("View how many crops you have in the CROPS window.", 20, 280, 25, Color.WHITE);


        Raylib.DrawText("> BUYING: Press on an option in the BUY window to buy new seeds.", 20, 380, 25, Color.WHITE);
        Raylib.DrawText("Make sure you have enough dabloons for your purchase in the bottom left corner.", 20, 410, 25, Color.WHITE);

        Raylib.DrawText("> SELLING: Press on an option in the SELL window to sell your crops.", 20, 510, 25, Color.WHITE);
        Raylib.DrawText("Watch your dabloon amount grow and earn even more by buying more seeds!", 20, 540, 25, Color.WHITE);
    }
}