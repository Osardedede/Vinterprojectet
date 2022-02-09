using System.Numerics;
using Raylib_cs;


public class Bullet
{

    public Vector2 direction;

    public Rectangle brect;
    public bool isAlive;

    public void Update()
    {
        float speed = 30;

        // Tar rictningen som är 1 om jag har rört och gångrar den med speed. 
        brect.x += direction.X * speed;
        brect.y += direction.Y * speed;



        if (brect.x > Raylib.GetScreenWidth() - 100)
        {
            isAlive = false;
        }
    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(brect, Color.RED);
    }

}
