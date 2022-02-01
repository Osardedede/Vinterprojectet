using System;
using System.Threading;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;


public class ENEMY
{

    public Rectangle erect;

    float speed = 3;

    Texture2D enemyImage = Raylib.LoadTexture("zombe.png");
    Random generator = new Random();

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

    public void emovement()
    {
        // int speed = generator.Next(30, 100);

        erect.x += direction.X * speed;
        erect.y += direction.Y * speed;


        if (erect.x < 0 || erect.x > Raylib.GetScreenWidth() - erect.width)
        {
            direction.X = -direction.X;
        }
    }
}
