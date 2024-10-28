using System;
using System.Drawing;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class ColorChangingTriangle : GameWindow
{
    private Vector3[] vertices = new Vector3[3];
    private Color triangleColor = Color.Red;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public ColorChangingTriangle() : base(800, 600, GraphicsMode.Default, "Color Changing Triangle")
    {
        LoadVerticesFromFile("triangle_vertices.txt");
        VSync = VSyncMode.On;
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

        // Schimba culoarea la apasarea tastelor R, G, B
        if (keyboard[Key.R]) triangleColor = Color.Red;
        if (keyboard[Key.G]) triangleColor = Color.Green;
        if (keyboard[Key.B]) triangleColor = Color.Blue;

        // Iesire program la apasarea tastei ESC
        if (keyboard[Key.Escape]) Exit();

        // Modifica rotatia camerei pe baza miscarii mouse-ului
        MouseState mouse = Mouse.GetState();
        rotationX += mouse.X * 0.01f;
        rotationY += mouse.Y * 0.01f;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.LoadIdentity();

        // Aplicarea rotatiei triunghiului
        GL.Rotate(rotationX, 0.0, 1.0, 0.0);
        GL.Rotate(rotationY, 1.0, 0.0, 0.0);

        GL.Begin(PrimitiveType.Triangles);
        GL.Color3(triangleColor);
        foreach (var vertex in vertices)
        {
            GL.Vertex3(vertex);
        }
        GL.End();

        SwapBuffers();
    }

    // Citire din fisier
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

    [STAThread]
    static void Main()
    {
        Console.WriteLine("HELP \nSchimbarea culorilor se realizeaza prin apasarea tastelor R sau G sau B conform initalei culorilor:");
        Console.WriteLine("R - Red\nG - Green\nB - Blue\n");
        Console.WriteLine("Mouse-ul este urmarit constant, nu e nevoie apasarea LMB-ului sau a RMB-ului doar miscarea cursorului.");
        Console.WriteLine(value: "Tasta ESC inchide programul.");
        using (var window = new ColorChangingTriangle())
        {
            window.Run(60.0);
        }
    }
}
