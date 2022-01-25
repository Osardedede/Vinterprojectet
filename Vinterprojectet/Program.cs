using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;


Random generator = new Random();
Raylib.InitWindow(1200, 750, "2D-topdown game");

Raylib.SetTargetFPS(60);

Texture2D playerImage = Raylib.LoadTexture("gubbe(2).png");
Texture2D background = Raylib.LoadTexture("Stage_Basement_room2.png");

float speed = 20f;
bool death = false;
bool lvl1 = true;
bool lvl2 = false;
bool toutch = true;
int score = 0;

// kollar senaste directionen och gör default direction till höger. DÅ den är x= 1 y = 0
Vector2 lastPlayerDirection = new Vector2(1, 0);



Rectangle playerRect = new Rectangle(400, 300, playerImage.width, playerImage.height);

Rectangle exit = new Rectangle(200, 300, 80, 100);
Rectangle r2 = new Rectangle(200, 200, 80, 100);


List<Bullet> bullets = new List<Bullet>();

while (!Raylib.WindowShouldClose())
{

    if (lvl1 == true)
    {

        Raylib.BeginDrawing();
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.ClearBackground(Color.BLUE);

        Color exitColor = Color.BLACK;


        Raylib.DrawRectangleRec(exit, Color.BLACK);


        Raylib.DrawRectangleRec(r2, Color.MAGENTA);
        // Raylib.DrawRectangleRec(playerRect,Color.WHITE);
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
        Raylib.DrawText($"{score}", 75, 10, 100, Color.BLACK);

        if (toutch == true && Raylib.CheckCollisionRecs(exit, playerRect))
        {
            death = true;
            lvl1 = false;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
        {
            Bullet b1 = new Bullet();
            b1.rect = new Rectangle(playerRect.x, playerRect.y, 50, 30);
            b1.isAlive = true;
            b1.direction = lastPlayerDirection;
            bullets.Add(b1);

        }
// En for loop om att kolla listan och rita skott och kolla vilken riktning dom är/får dom att röra sig
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Update();
            bullets[i].Draw();
        }

        bullets.RemoveAll(x => x.isAlive == false);

        if (Raylib.CheckCollisionRecs(r2, playerRect))
        {
            int cord = generator.Next(800);
            int cord2 = generator.Next(600);
            r2 = new Rectangle(cord, cord2, 80, 100);
            score++;

        }
        if (lvl1 == true || lvl2 == true)
        {
            Vector2 movement = ReadMovement(speed);
            playerRect.x += movement.X;
            playerRect.y += movement.Y;
            // Den under uppdaterar för att se om jag har rtört mig
            if (movement.Length() > 0)
            {
                // Den här gör vectorn till 1 alltså normalizar
                lastPlayerDirection = Vector2.Normalize(movement);
            }
        }

        if (playerRect.x >= 1200)
        {
            lvl1 = false;
            lvl2 = true;
            playerRect.y = 300;
            playerRect.x = 300;
        }
        if (playerRect.x <= 0)
        {
            playerRect.x = 0;
        }
        if (playerRect.y <= 0)
        {
            playerRect.y = 0;
        }
        if (playerRect.y + playerRect.height >= 750)
        {
            playerRect.y = 750 - playerRect.height;
        }
        Raylib.EndDrawing();
    }
    else if (lvl2 == true)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BEIGE);
        Raylib.DrawText($"{score}", 75, 10, 100, Color.BLACK);
        Raylib.DrawRectangleRec(r2, Color.MAGENTA);


        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

        if (Raylib.CheckCollisionRecs(r2, playerRect))
        {
            int cord = generator.Next(800);
            int cord2 = generator.Next(600);
            r2 = new Rectangle(cord, cord2, 80, 100);
            score++;
        }

        if (lvl1 == true || lvl2 == true)
        {
            Vector2 movement = ReadMovement(speed);
            playerRect.x += movement.X;
            playerRect.y += movement.Y;
        }




        if (playerRect.x <= 0)
        {
            lvl1 = true;
            lvl2 = false;
            playerRect.x = 600 - playerRect.width;
        }


        if (playerRect.y <= 0)
        {
            playerRect.y = 0;
        }

        if (playerRect.y + playerRect.height >= 600)
        {
            playerRect.y = 600 - playerRect.height;
        }

        if (playerRect.x + playerRect.width >= 800)
        {
            playerRect.x = 800 - playerRect.width;
        }

        Raylib.EndDrawing();

    }

    else if (death == true)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText($"U DIED", 100, 100, 100, Color.WHITE);
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            death = false;
            lvl1 = true;
            score = 0;
            playerRect = new Rectangle(400, 300, playerImage.width, playerImage.height);

        }



        Raylib.EndDrawing();

    }
}

// Allt under är för movmenten
static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        movement.Y += speed;

    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X += speed;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y -= speed;

    return movement;
}

