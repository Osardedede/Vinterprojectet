using System;
using Raylib_cs;
public class Bullet
{
    public Rectangle rect;
    public bool isAlive;

    public void Update()
    {
        rect.x += 30;

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
