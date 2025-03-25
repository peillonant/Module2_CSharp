using Raylib_cs;
using static Raylib_cs.Raylib; // Charger toutes les méthodes statics de Raylib
using static Raylib_cs.ConfigFlags; // 
using System.Numerics;

//namespace Program;

// Classe for testing primitves
public class Circle
{
    public Vector2 v2_Position { get; private set; }
    public float f_Radius { get; private set; }
    public Color c_Couleur { get; private set; }
    public Vector2 v2_Vitesse { get; private set; }

    // Code to create the constructor
    public Circle(Vector2 v2_LocalPosition, float f_LocalRadius, Color c_LocalCouleur, Vector2 v2_LocalVitesse)
    {
        v2_Position = v2_LocalPosition;
        f_Radius = f_LocalRadius;
        c_Couleur = c_LocalCouleur;
        v2_Vitesse = v2_LocalVitesse;
    }

    public void Update()
    {
        v2_Position += v2_Vitesse;
    }

    public void Draw()
    {
        DrawCircleV(v2_Position, f_Radius, c_Couleur);
    }


}

public class Program
{
    public static void Main()
    {
        SetConfigFlags(ConfigFlags.VSyncHint);
        InitWindow(800, 600, "Hello World");
        SetTargetFPS(60);

        //SetWindowState(BorderlessWindowMode);         // Boderless windows - Resize the Window to be Full screen
        //SetWindowState(ConfigFlags.ResizableWindow);  // Windows can be modify in terme of Size
        //SetWindowState(UndecoratedWindow);              // Remove everything around the window (Can not be moved or resize or close (no X))

        // Il faut charger les images APRES le InitWindow
        Texture2D tex_Player = LoadTexture("Asset/images/player_1.png");

        // Gestion d'une animation
        Texture2D tex_Char = LoadTexture("Asset/images/Character_1.png");
        int i_FrameTimer = 0;
        int i_CurrentFrame = 0;
        int i_FrameVitesse = 8;
        float f_FrameWidth = tex_Char.Width / 8;
        Rectangle rec_FrameRec = new Rectangle(0, 0, f_FrameWidth, tex_Char.Height);
        float angle = 0;

        Vector2 v2_posPlay = new((GetScreenWidth() - tex_Player.Width) / 2, (GetScreenHeight() - tex_Player.Height) / 2);
        Vector2 v2_posEne = new(250, 250);
        Color col_Play = Color.White;

        // Linked to Frame Rate
        //SetConfigFlags(ConfigFlags.VSyncHint);
        //SetTargetFPS(60);

        Circle circle3 = new Circle(new Vector2(0, 450), 50, Color.Black, new Vector2(0.5f, 0));
        //circle3.Construct_Cercle(new Vector2(0, 450), 50, Color.Black, new Vector2(0.35f, 0));

        // Update font
        Font font_Kenvector = LoadFontEx("Asset/Font/kenvector_future.ttf", 45, null, 250);

        // Create Camera 2D
        Camera2D camera = new();
        camera.Offset = new(v2_posPlay.X - tex_Player.Width / 2, v2_posPlay.Y - tex_Player.Height / 2);
        camera.Rotation = 0f;
        camera.Zoom = 1f;

        while (!WindowShouldClose())                    // GameLoop
        {
            BeginDrawing();
            ClearBackground(Color.Blue);

            BeginMode2D(camera);

            //DrawText("Hello, world!", 12, 12, 20, Color.Black);

            // Code to manage the Pause of the game when the focus is lost
            if (IsWindowState(UnfocusedWindow))
            {
                Console.WriteLine("Pause !");
            }

            // Draw a Circle thanks to the class
            // circle3.Update();
            // circle3.Draw();

            DrawLine(GetScreenWidth() / 2, 0, GetScreenWidth() / 2, GetScreenHeight(), Color.White);
            DrawLine(0, GetScreenHeight() / 2, GetScreenWidth(), GetScreenHeight() / 2, Color.White);

            // Draw an image
            angle += 0.25f;

            // DrawTexture(tex_Player, 10, 10, Color.White);

            // DrawTextureEx(tex_Player, new Vector2(30, 30), angle, 1, Color.White);

            // DrawCentre(tex_Player, new Vector2(150, 150), angle);

            // Draw the animation
            i_FrameTimer++;

            if (i_FrameTimer >= i_FrameVitesse)
            {
                i_FrameTimer = 0;
                i_CurrentFrame++;
            }

            if (i_CurrentFrame > 7)
                i_CurrentFrame = 0;

            rec_FrameRec.X = i_CurrentFrame * f_FrameWidth;

            DrawTextureRec(tex_Char, rec_FrameRec, v2_posEne, Color.White);


            // Draw a Text
            DrawText("BONJOUR", 500, 500, 50, Color.White);  // Text with the Default Font of Raylib

            // Centrer un text
            Vector2 v2_Size = MeasureTextEx(font_Kenvector, "Test de la font", font_Kenvector.BaseSize, 3);

            float f_PX = (GetScreenWidth() - v2_Size.X) / 2;
            float f_PY = (GetScreenHeight() - v2_Size.Y) / 2;

            DrawTextEx(font_Kenvector, "Test de la font", new Vector2(f_PX, f_PY), font_Kenvector.BaseSize, 3, Color.White);


            // Delta Time and Frame rate
            // Draw the number of FPS on screen
            DrawFPS(700, 0);

            // The way to retrieve a delta time as LUA
            float dt = GetFrameTime();

            //Retrieve the number of FPS to optimize bad computer to create condition to avoid animation when the FPS of the computer is not enough
            int i_FPS = GetFPS();


            // Controler
            DrawTextureV(tex_Player, v2_posPlay, col_Play);

            if (IsKeyDown(KeyboardKey.A) || IsKeyDown(KeyboardKey.Left))
            {
                v2_posPlay.X -= 5;
            }
            if (IsKeyDown(KeyboardKey.D) || IsKeyDown(KeyboardKey.Right))
            {
                v2_posPlay.X += 5;
            }
            if (IsKeyDown(KeyboardKey.W) || IsKeyDown(KeyboardKey.Up))
            {
                v2_posPlay.Y -= 5;
            }
            if (IsKeyDown(KeyboardKey.S) || IsKeyDown(KeyboardKey.Down))
            {
                v2_posPlay.Y += 5;
            }

            // Deplacement souris
            if (IsMouseButtonDown(MouseButton.Left))
            {
                v2_posPlay.X = GetMouseX() - tex_Player.Width / 2;
                v2_posPlay.Y = GetMouseY() - tex_Player.Height / 2;
            }

            if (IsMouseButtonPressed(MouseButton.Right))
            {
                v2_posPlay.X = GetMouseX() - tex_Player.Width / 2;
                v2_posPlay.Y = GetMouseY() - tex_Player.Height / 2;
            }



            // Method Clamp permet de bloquer le mouvement jusqu'a un certaine position (Ici on va de 0 au bord de l'écran)
            v2_posPlay.X = Math.Clamp(v2_posPlay.X, 0, GetScreenWidth() - tex_Player.Width);
            v2_posPlay.Y = Math.Clamp(v2_posPlay.Y, 0, GetScreenHeight() - tex_Player.Height);



            // Collision
            Rectangle rec_Player = new Rectangle(v2_posPlay.X, v2_posPlay.Y, tex_Player.Width, tex_Player.Height);
            Rectangle rec_Enemy = new Rectangle(v2_posEne.X, v2_posEne.Y, f_FrameWidth, tex_Char.Height);

            if (CheckCollisionRecs(rec_Player, rec_Enemy))
            {
                col_Play = Color.Red;
            }
            else
            {
                col_Play = Color.White;
            }

            // Gestion de la Camera
            camera.Target = new(v2_posPlay.X + tex_Player.Width / 2, v2_posPlay.Y + tex_Player.Height / 2);
            // Gestion Zoom
            camera.Zoom += GetMouseWheelMove() * 0.1f;

            EndDrawing();
        }

        UnloadFont(font_Kenvector);

        EndMode2D();

        CloseWindow();
    }

    // Function qui permet de faire la rotation de l'image par rapport à une origine différente de son 0x0
    public static void DrawCentre(Texture2D tex_Local, Vector2 v2_LocalPos, float f_LocalAngle)
    {
        DrawTexturePro(tex_Local, new Rectangle(0, 0, tex_Local.Width, tex_Local.Height), new Rectangle(v2_LocalPos.X, v2_LocalPos.Y, tex_Local.Width, tex_Local.Height), new Vector2(tex_Local.Width / 2, tex_Local.Height / 2), f_LocalAngle, Color.White);
    }
}


