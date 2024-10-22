using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;

namespace OpenTKMouseControl
{
    public class SimpleWindow : GameWindow
    {
        private float pyramidX = 0.0f; // Pozitia initiala a piramidei pe axa X
        private float pyramidY = 0.0f; // Pozitia initiala a piramidei pe axa Y
        private float pyramidZ = -5.0f; // Pozitia initiala a piramidei pe axa Z
        private float rotationY = 0.0f; // Unghiul de rotatie initial pe axa Y

        public SimpleWindow() : base(800, 600)
        {
            VSync = VSyncMode.On; // Activam VSync
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e); // Apelam implementarea din clasa de baza

            GL.ClearColor(Color.CornflowerBlue); // Culoarea fundalului
            GL.Enable(EnableCap.DepthTest); // Activam testul de adancime
            GL.Disable(EnableCap.Blend); // Dezactivam blendingul pentru culori solide
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            float aspectRatio = (float)Width / Height;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            MouseState mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            // Mapam pozitia mouse-ului la pozitia piramidei 
            pyramidX = (mouseX / (float)Width) * 2 - 1;
            pyramidY = 1 - (mouseY / (float)Height) * 2; // Inversam axa Y

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Key.Escape)) // Verificam daca tasta Escape este apasata
            {
                Exit();
            }

            // Rotim piramida in jurul axei Y cu tastele sus si jos
            if (keyboardState.IsKeyDown(Key.Up))
            {
                rotationY += 1.0f; // Rotim in sus
            }
            if (keyboardState.IsKeyDown(Key.Down))
            {
                rotationY -= 1.0f; // Rotim in jos
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(pyramidX, pyramidY, pyramidZ);

            GL.Rotate(rotationY, 0.0f, 1.0f, 0.0f); // Rotatie in jurul axei Y

            DrawPyramid(); 

            SwapBuffers();
        }

        private void DrawPyramid()
        {
            // Desenam baza ca un quad
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Black); // Culoarea bazei
            GL.Vertex3(-1.0f, -1.0f, -1.0f); // Vertex-ul din stanga-jos
            GL.Vertex3(1.0f, -1.0f, -1.0f); // Vertex-ul din dreapta-jos
            GL.Vertex3(1.0f, -1.0f, 1.0f); // Vertex-ul din dreapta-sus
            GL.Vertex3(-1.0f, -1.0f, 1.0f); // Vertex-ul din stanga-sus
            GL.End();

            // Desenam fetele piramidei
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.White); // Culoarea fetei din fata
            GL.Vertex3(0.0f, 1.0f, 0.0f); // Vertex-ul de sus
            GL.Vertex3(-1.0f, -1.0f, 1.0f); // Vertex-ul din stanga-jos
            GL.Vertex3(1.0f, -1.0f, 1.0f); // Vertex-ul din dreapta-jos
            GL.End();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.White); // Culoarea fetei din dreapta
            GL.Vertex3(0.0f, 1.0f, 0.0f); // Vertex-ul de sus
            GL.Vertex3(1.0f, -1.0f, 1.0f); // Vertex-ul din dreapta-jos
            GL.Vertex3(1.0f, -1.0f, -1.0f); // Vertex-ul din spate-dreapta
            GL.End();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.White); // Culoarea fetei din spate
            GL.Vertex3(0.0f, 1.0f, 0.0f); // Vertex-ul de sus
            GL.Vertex3(1.0f, -1.0f, -1.0f); // Vertex-ul din spate-dreapta
            GL.Vertex3(-1.0f, -1.0f, -1.0f); // Vertex-ul din spate-stanga
            GL.End();

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.White); // Culoarea fetei din stanga
            GL.Vertex3(0.0f, 1.0f, 0.0f); // Vertex-ul de sus
            GL.Vertex3(-1.0f, -1.0f, -1.0f); // Vertex-ul din spate-stanga
            GL.Vertex3(-1.0f, -1.0f, 1.0f); // Vertex-ul din fata-stanga
            GL.End();

            // Desenam linii negre de la varful piramidei catre fiecare colt al bazei pentru evidentierea fetelor
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black); // Culoarea liniilor

            GL.Vertex3(0.0f, 1.0f, 0.0f); // Linia catre coltul din fata-stanga
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Vertex3(0.0f, 1.0f, 0.0f); // Linia catre coltul din fata-dreapta
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Vertex3(0.0f, 1.0f, 0.0f); // Linia catre coltul din spate-dreapta
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Vertex3(0.0f, 1.0f, 0.0f); // Linia catre coltul din spate-stanga
            GL.Vertex3(-1.0f, -1.0f, -1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(144.0, 0.0);
            }
        }
    }
}
