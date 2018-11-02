
using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Probotix.Mpu
{
    public partial class Simulator : Form
    {
        public Simulator()
        {
            InitializeComponent();
        }

        private int _width, _height;
        private static int _frontTexture, _backTexture, _topTexture, _bottomTexture, _leftTexture, _rightTexture;
        private bool _isLoaded;
        public int Pitch, Yaw, Roll;
        private int size = 250;

        private void SetupViewport()
        {

            _width = glControl_visualizer.Width;
            _height = glControl_visualizer.Height;
            size = (int)(0.45 * _width);
            GL.MatrixMode(MatrixMode.Projection);

            GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.Texture2D);						    // Enable Texture Mapping            
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.DepthTest);						    // Enables Depth Testing
            GL.Enable(EnableCap.Blend);
            TexUtil.InitTexturing();

            const float diffuseLight = 0.6f;
            const float ambientLight = 1.0f;
            const float specularLight = 0.7f;

            float[] lightKa = { ambientLight, ambientLight, ambientLight, 1.0f };
            float[] lightKd = { diffuseLight, diffuseLight, diffuseLight, 1.0f };
            float[] lightKs = { specularLight, specularLight, specularLight, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Ambient, lightKa);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightKd);
            GL.Light(LightName.Light0, LightParameter.Specular, lightKs);

            float[] lightPos = { 0.0f, 10.0f, -10.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Lighting);


            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.LoadIdentity();
            GL.Ortho(-_width, _width, -_height, _height, _width, -_width); // define coordinate system 
            GL.Viewport(0, 0, _width, _height); // Use all of the glControl painting area

            _frontTexture = TexUtil.CreateTextureFromFile("pix/Front.png");
            _backTexture = TexUtil.CreateTextureFromFile("pix/Back.png");
            _topTexture = TexUtil.CreateTextureFromFile("pix/Top.png");
            _bottomTexture = TexUtil.CreateTextureFromFile("pix/Bottom.png");
            _leftTexture = TexUtil.CreateTextureFromFile("pix/Left.png");
            _rightTexture = TexUtil.CreateTextureFromFile("pix/Right.png");
        }

        private void glControl_visualizer_Load(object sender, EventArgs e)
        {

            _isLoaded = true;
            GL.ClearColor(Color.Black);
            SetupViewport();
        }

        private void glControl_visualizer_Paint(object sender, PaintEventArgs e)
        {
            if (!_isLoaded) return; // to ensure that main form is loaded properly

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


            GL.Rotate(Pitch, Vector3.UnitX);
            GL.Rotate(Roll, Vector3.UnitZ);
            GL.Rotate(Yaw, Vector3.UnitY);

            GL.BindTexture(TextureTarget.Texture2D, _frontTexture);
            GL.Begin(PrimitiveType.Quads); // Front Face
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1 * size, -1 * size, -1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1 * size, -1 * size, -1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1 * size, 1 * size, -1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1 * size, 1 * size, -1 * size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _backTexture);
            GL.Begin(PrimitiveType.Quads); // Back Face
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1 * size, 1 * size, 1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1 * size, 1 * size, 1 * size);
            GL.End();



            GL.BindTexture(TextureTarget.Texture2D, _topTexture);
            GL.Begin(PrimitiveType.Quads); // Top Face
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1 * size, 1 * size, 1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1 * size, 1 * size, 1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1 * size, 1 * size, -1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1 * size, 1 * size, -1 * size);
            GL.End();


            GL.BindTexture(TextureTarget.Texture2D, _bottomTexture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1 * size, -1 * size, -1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1 * size, -1 * size, -1 * size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _leftTexture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-1 * size, -1 * size, -1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-1 * size, 1 * size, -1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1 * size, 1 * size, 1 * size);
            GL.End();


            GL.BindTexture(TextureTarget.Texture2D, _rightTexture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1);
            GL.Vertex3(1 * size, -1 * size, 1 * size);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1 * size, -1 * size, -1 * size);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1 * size, 1 * size, -1 * size);
            GL.TexCoord2(0, 0);
            GL.Vertex3(1 * size, 1 * size, 1 * size);
            GL.End();

            glControl_visualizer.SwapBuffers();
        }

        private void timer_Updater_Tick(object sender, EventArgs e)
        {
            glControl_visualizer.Invalidate();
        }

        private void Simulator_Resize(object sender, EventArgs e)
        {
            SetupViewport();
            glControl_visualizer.Invalidate();
        }

    }
}
