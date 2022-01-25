using System;
using System.Numerics;
using Raylib_cs;


public class Bullet
{

    public Vector2 direction;

    public Rectangle rect;
    public bool isAlive;

    public void Update()
    {
        float speed = 30;

        // Tar rictningen som är 1 om jag har rört och gångrar den med speed. 
        rect.x += direction.X * speed;
        rect.y += direction.Y * speed;



        if (rect.x > Raylib.GetScreenWidth() - 100)
        {
            isAlive = false;
        }
    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(rect, Color.RED);
    }

}
