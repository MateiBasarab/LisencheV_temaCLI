using System;
using System.Drawing;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class RGBTriangle : GameWindow
{
    private Vector3[] vertices = new Vector3[3];
    private Color[] vertexColors = new Color[3] { Color.Red, Color.Green, Color.Blue };
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public RGBTriangle() : base(800, 600, GraphicsMode.Default, "RGB Triangle")
    {
        VSync = VSyncMode.On;
        LoadVerticesFromFile("triangle_vertices.txt");
    }
    
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(Color.Black);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);
        GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
    }
    
    
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        KeyboardState keyboard = Keyboard.GetState();

        // Ajusteaza valorile RGB pentru fiecare vertex
        if (keyboard[Key.R]) vertexColors[0] = AdjustColorComponent(vertexColors[0], 'R');
        if (keyboard[Key.G]) vertexColors[1] = AdjustColorComponent(vertexColors[1], 'G');
        if (keyboard[Key.B]) vertexColors[2] = AdjustColorComponent(vertexColors[2], 'B');

        // Iesire program la apasarea tastei ESC
        if (keyboard[Key.Escape]) Exit();

        // Afiseaza valorile RGB in consola la apasarea tastei SPACE
        if (keyboard[Key.Space])
        {
            Console.WriteLine("Valori RGB:");
            for (int i = 0; i < vertexColors.Length; i++)
            {
                Console.WriteLine($"Vertex {i + 1} RGB: {vertexColors[i].R}, {vertexColors[i].G}, {vertexColors[i].B}");
            }
            Console.WriteLine();
        }

        // Roteste triunghiul pe baza miscarii mouse-ului
        MouseState mouse = Mouse.GetState();
        rotationX += mouse.X * 0.01f;
        rotationY += mouse.Y * 0.01f;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // Aplicarea rotatiei triunghiului
        GL.LoadIdentity();
        GL.Rotate(rotationX, 0.0, 1.0, 0.0);
        GL.Rotate(rotationY, 1.0, 0.0, 0.0);

        GL.Begin(PrimitiveType.Triangles);
        for (int i = 0; i < vertices.Length; i++)
        {
            GL.Color3(vertexColors[i]);
            GL.Vertex3(vertices[i]);
        }
        GL.End();

        SwapBuffers();
    }

    // Citire fisier
    private void LoadVerticesFromFile(string fileName)
    {
        string filePath = "../../../" + fileName;
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < vertices.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                vertices[i] = new Vector3(
                    float.Parse(parts[0]),
                    float.Parse(parts[1]),
                    float.Parse(parts[2])
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare la incarcarea coordonatelor: " + ex.Message);
        }
    }

    private Color AdjustColorComponent(Color color, char component)
    {
        int increment = 10; // incrementarea se face cu 10 unitati la fiecare apasare
        int r = color.R, g = color.G, b = color.B;

        switch (component)
        {
            case 'R': r = (r + increment) % 256; break;
            case 'G': g = (g + increment) % 256; break;
            case 'B': b = (b + increment) % 256; break;
        }

        return Color.FromArgb(r, g, b);
    }

    [STAThread]
    static void Main()
    {
        Console.WriteLine("HELP \nSchimbarea culorilor pentru fiecare vertex se realizează prin apasarea tastelor R, G, B conform initialei culorilor:");
        Console.WriteLine("R - incrementare Red\nG - incrementare Green\nB - incrementare Blue\n");
        Console.WriteLine("Tasta SPACE afiseaza valorile RGB ale fiecarui vertex.");
        Console.WriteLine("Mouse-ul este urmarit constant, nu e nevoie apasarea LMB-ului sau a RMB-ului doar miscarea cursorului.");
        Console.WriteLine("Tasta ESC inchide programul.");

        using (var window = new RGBTriangle())
        {
            window.Run(60.0);
        }
    }
}
