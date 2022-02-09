using System;
using System.Numerics;
using Raylib_cs;


public class ENEMY
{

    public Rectangle erect;


    Texture2D enemyImage = Raylib.LoadTexture("zombe.png");
    Random generator = new Random();

    public bool Edead;


    Vector2 direction;
    public ENEMY()
    {
        int dir = generator.Next(8);


        direction = new Vector2();
        while (direction.X == 0 && direction.Y == 0)
        {
            direction.X = generator.Next(-1, 2);
            direction.Y = generator.Next(-1, 2);
        }
        // direction = Vector2.Normalize(direction);

    }

    public void draw()
    {

        Raylib.DrawTexture(enemyImage, (int)erect.x, (int)erect.y, Color.WHITE);

    }

    float speed = 3;
    public void emovement()
    {

        erect.x += direction.X * speed;
        erect.y += direction.Y * speed;

        // så att dom stuttsar
        if (erect.x < 0 || erect.x > Raylib.GetScreenWidth() - erect.width)
        {
            direction.X = -direction.X;
        }
        if (erect.y < 0 || erect.y > Raylib.GetScreenHeight() - erect.height)
        {
            direction.Y = -direction.Y;
        }
    }
}
