using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
    private RenderWindow window;
    private static float deltaTime;
    private static Vector2f size = new Vector2f(185, 185);
    private const float padding = 0;
    private int turnCount = 0;
    private bool mPressed = false;

    public void Run()
    {
        window = new RenderWindow(new VideoMode(600, 600), "");
        window.SetFramerateLimit(60);

        Clock deltaTimeClock = new Clock();
        List<RectangleShape> renderList = new List<RectangleShape>();
        List<RectangleShape> clickedList = new List<RectangleShape>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                RectangleShape defaultRect = new RectangleShape
                {
                    Size = size,
                    FillColor = new Color(Color.Black),
                    Position = new Vector2f(i * (1.125f * size.X) + padding, j * (1.125f * size.Y) + padding),
                };
                renderList.Add(defaultRect);
            }
        }

        while (window.IsOpen)
        {
            window.DispatchEvents();

            float deltaTime = deltaTimeClock.Restart().AsSeconds();
            Game.deltaTime = deltaTime;

            window.Closed += (sender, e) => window.Close();
            window.Clear(Color.White);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!mPressed)
                {
                    Vector2f mPos = (Vector2f)Mouse.GetPosition(window);
                    for (int i = 0; i < renderList.Count; i++)
                    {
                        if (renderList[i].GetGlobalBounds().Contains(mPos.X, mPos.Y) && !clickedList.Contains(renderList[i]))
                        {
                            if (turnCount % 2 == 0)
                            {
                                clickedList.Add(renderList[i]);
                                turnCount += 1;
                                renderList[i].FillColor = Color.Red;
                            }
                            else
                            {
                                clickedList.Add(renderList[i]);
                                turnCount += 1;
                                renderList[i].FillColor = Color.Green;
                            }
                        }
                    }
                    mPressed = true;
                }
            }
            else
            {
                mPressed = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                for (int i = 0; i < renderList.Count; i++)
                {
                    renderList[i].FillColor = Color.Black;
                    clickedList.Remove(renderList[i]);
                    turnCount = 0;
                }
            }

            for (int i = 0; i < renderList.Count; i++)
            {
                window.Draw(renderList[i]);
            }
            
            window.Display();
        }
    }
}