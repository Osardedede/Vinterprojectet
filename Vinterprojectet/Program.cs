using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;
using System.Threading;



Random generator = new Random();
Raylib.InitWindow(1200, 750, "2D-topdown game");

Raylib.SetTargetFPS(60);

Texture2D playerImage = Raylib.LoadTexture("gubbe(2).png");
Texture2D enemyImage = Raylib.LoadTexture("zombe.png");
Texture2D coin = Raylib.LoadTexture("coin.png");
Texture2D key = Raylib.LoadTexture("KEY.png");
Texture2D background = Raylib.LoadTexture("Stage_Basement_room1.png");
Texture2D background2 = Raylib.LoadTexture("Room2.png");

float speed = 10f;
bool death = false;
bool lvl1 = true;
bool lvl2 = false;
bool hasKey = false;
bool hasKey2 = false;
int score = 0;
int score2 = 0;

// kollar senaste directionen och gör default direction till höger. DÅ den är x= 1 y = 0
Vector2 lastPlayerDirection = new Vector2(1, 0);

int timer = 0;
int timerMax = 60;

Rectangle playerRect = new Rectangle(400, 300, playerImage.width, playerImage.height);

Rectangle r2 = new Rectangle(200, 200, 80, 100);
Rectangle keyrect = new Rectangle(600, 375, 200, 100);
List<int> espawn = new List<int>();
espawn.Add(169);
espawn.Add(825);
espawn.Add(475);
espawn.Add(475);

List<Bullet> bullets = new List<Bullet>();
List<ENEMY> enemies = new List<ENEMY>();

while (!Raylib.WindowShouldClose())
{

    if (lvl1 == true)
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            speed = 15f;
        }
        else
        {
            speed = 10f;
        }

        Raylib.BeginDrawing();
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.ClearBackground(Color.BLUE);





        Raylib.DrawTexture(coin, (int)r2.x, (int)r2.y, Color.WHITE);
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
        Raylib.DrawText($"{score}", 75, 10, 100, Color.WHITE);

        if (score >= 1 && hasKey == false)
        {
            Raylib.DrawTexture(key, 550, 400, Color.WHITE);

            if (Raylib.CheckCollisionRecs(keyrect, playerRect))
            {
                hasKey = true;
                background = Raylib.LoadTexture("Stage_Basement_room5.png");


            }
        }





        if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
        {
            Bullet b1 = new Bullet();
            b1.brect = new Rectangle(playerRect.x, playerRect.y, 50, 30);
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


        }
        if (playerRect.x <= 0)
        {
            playerRect.x = 0;
        }
        if (playerRect.y <= 0)
        {
            if (hasKey == true)
            {

                lvl1 = false;
                lvl2 = true;
                playerRect.y = 750 - playerRect.height;
                playerRect.x = 600;
            }
        }
        if (playerRect.y + playerRect.height >= 750)
        {
            playerRect.y = 750 - playerRect.height;
        }
        Raylib.EndDrawing();
    }




    // Rum 2




















    else if (lvl2 == true)
    {

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BEIGE);
        Raylib.DrawTexture(background2, 0, 0, Color.WHITE);
        Raylib.DrawText($"{score2}", 75, 10, 100, Color.WHITE);
        Raylib.DrawTexture(coin, (int)r2.x, (int)r2.y, Color.WHITE);
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);



        if (lvl1 == true || lvl2 == true)
        {
            Vector2 movement = ReadMovement(speed);
            playerRect.x += movement.X;
            playerRect.y += movement.Y;

            if (movement.Length() > 0)
            {
                // Den här gör vectorn till 1 alltså normalizar
                lastPlayerDirection = Vector2.Normalize(movement);
            }
        }

        if (Raylib.CheckCollisionRecs(r2, playerRect))
        {
            int cord = generator.Next(800);
            int cord2 = generator.Next(600);
            r2 = new Rectangle(cord, cord2, 80, 100);
            score2++;

        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_H))
        {
            Bullet b1 = new Bullet();
            b1.brect = new Rectangle(playerRect.x, playerRect.y, 50, 30);
            b1.isAlive = true;
            // så att skottet skuts där spelaren kollar
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
        // vilket gör så att den kommer sluta spwana fiender när jag har 15 score 
        if (score2 < 15)
        {

            // en timer som funkar att den gör +1 varje frame och när den har nått timerMax 
            // så spawnar den en fiende var i det här fallet var 60 frame
            timer++;
            if (timer > timerMax)
            {
                timer = 0;
                int spawn = generator.Next(700);

                ENEMY e1 = new ENEMY();
                e1.erect = new Rectangle(spawn, spawn, 100, 50);
                enemies.Add(e1);


            }
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            speed = 15f;
        }
        else
        {
            speed = 10f;
        }

        if (score2 >= 1 && hasKey2 == false)
        {
            Raylib.DrawTexture(key, 550, 400, Color.WHITE);

            if (Raylib.CheckCollisionRecs(keyrect, playerRect))
            {
                hasKey2 = true;
                background2 = Raylib.LoadTexture("Room3.png");

            }
        }

        // kollar igenom hela ENEMY listan om en något ifrån listan Bullet nuddar varandra, isf +1 poäng
        foreach (ENEMY enemy in enemies)
        {
            foreach (Bullet bullet in bullets)
            {
                if (Raylib.CheckCollisionRecs(enemy.erect, bullet.brect))
                {
                    enemy.Edead = true;
                    score2++;
                }
            }
        }

        foreach (ENEMY enemy in enemies)
        {
            if (Raylib.CheckCollisionRecs(playerRect, enemy.erect))
            {
                death = true;
                lvl2 = false;
            }
        }

        // den här kollar om det i listen har fått att Edead == true
        enemies.RemoveAll(e => e.Edead == true);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].draw();
            enemies[i].emovement();
        }







        if (playerRect.x <= 90)
        {

            playerRect.x = 100 + playerRect.height;
        }

        // väggar
        if (playerRect.x >= 1200) { playerRect.x = 1100 - playerRect.width; }
        if (playerRect.x <= 0) { playerRect.x = 0; }
        if (playerRect.y <= 0) { playerRect.y = 750 - playerRect.height; }
        if (playerRect.y + playerRect.height >= 750) { playerRect.y = 750 - playerRect.height; }



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
            score2 = 0;
            playerRect = new Rectangle(400, 300, playerImage.width, playerImage.height);
            enemies.RemoveAll(e => e.Edead == false);
            hasKey = false;
            background = Raylib.LoadTexture("Stage_Basement_room1.png");
            background2 = Raylib.LoadTexture("Room2.png");


        }



        Raylib.EndDrawing();

    }
}

// Allt under är för movmenten
static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) { movement.Y += speed; }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X += speed;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y -= speed;

    return movement;
}

