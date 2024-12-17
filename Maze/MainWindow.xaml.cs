using GameLibrary;
using GameLibrary.Maze;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Color = System.Windows.Media.Color;

namespace Maze
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The game
        /// </summary>
        private Game game = new Game();
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            GameEvents.ChangePoins += ChangePoins;
            GameEvents.ChangeHealth += ChangeHealth;
            GameEvents.ChangeCount += ChangeCount;
            GameEvents.EndGame += EndGame;

            PlayerOnePanel.Visibility = Visibility.Visible;
            PlayerTwoPanel.Visibility = Visibility.Visible;
            
            Background = new SolidColorBrush(Color.FromRgb(32, 56, 177));
        }

        /// <summary>
        /// Handles the Initialized event of the WindowsFormsHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            glControl.MakeCurrent();
        }

        /// <summary>
        /// Handles the Load event of the glControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void glControl_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            _loaded = true;
        }

        /// <summary>
        /// Handles the Paint event of the glControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void glControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Render();
            glControl.Invalidate();
        }

        /// <summary>
        /// Прогрузка
        /// </summary>
        private bool _loaded;

        /// <summary>
        /// Рендер интерфейста.
        /// </summary>
        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(System.Drawing.Color.FromArgb(32, 56, 177));
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, glControl.Width, glControl.Height, 0, 0, 1);

            game.Rendering();

            glControl.SwapBuffers();
        }

        /// <summary>
        /// Handles the Resize event of the GLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GLControl_Resize(object sender, EventArgs e)
        {
            if (!_loaded)
                return;
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            glControl.Invalidate();
        }

        /// <summary>
        /// Изменение количества очков.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="value">The value.</param>
        private void ChangePoins(string player, int value)
        {
            if (player == "PlayerOne")
            {
                value += Int32.Parse(PlayerOnePoins.Text);
                PlayerOnePoins.Text = value.ToString();
            }
            else
            {
                value += Int32.Parse(PlayerTwoPoins.Text);
                PlayerTwoPoins.Text = value.ToString();
            }
        }

        /// <summary>
        /// Изменение здоровья для игрока один.
        /// </summary>
        /// <param name="player">Игрок.</param>
        /// <param name="value">Значение.</param>
        private void ChangeHealth(string player, int value)
        {
            if (player == "PlayerOne")
                PlayerOneHealth.Text = value.ToString();
            else
                PlayerTwoHealth.Text = value.ToString();
        }

        /// <summary>
        /// Измнение количества для игрока один.
        /// </summary>
        /// <param name="player">Игрок.</param>
        /// <param name="count">Количество.</param>
        private void ChangeCount(string player, int count)
        {
            if (player == "PlayerOne")
                POCountBullets.Text = count.ToString();
            else
                PTCountBullets.Text = count.ToString();
        }

        /// Окончание игры.
        /// </summary>
        /// <param name="winPlayer">Победа одного из игроков.</param>
        private void EndGame(string winPlayer)
        {
            formHost.Visibility = Visibility.Hidden;
            var image = new ImageBrush();
            if (winPlayer == "PlayerOne")
            {
                image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\PlayerOneWin.png", UriKind.Relative));
                Background = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Resources\\PlayerSecondWin.png", UriKind.Relative));
                Background = image;
            }
            WinPanel.Visibility = Visibility.Visible;

            GameEvents.EndGame -= EndGame;
            GameEvents.ChangePoins -= ChangePoins;
            GameEvents.ChangeHealth -= ChangeHealth;
        }

        private void formHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void formHost_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}