using System;
using System.Threading.Tasks;
using System.Windows;

namespace Maze
{
    public partial class ServerConnectWindow : Window
    {
        private bool _isServer;  // Флаг: true = сервер, false = клиент
        private Server _server;  // Сервер
        private Client _client;  // Клиент

        public ServerConnectWindow(bool isServer)
        {
            InitializeComponent();
            _isServer = isServer;

            if (_isServer)
            {
                Title = "Создание сервера";
                ipTextBox.Text = "127.0.0.1"; // Локальный IP
                connectButton.Content = "Создать сервер"; // Текст кнопки
            }
            else
            {
                Title = "Подключение к серверу";
                connectButton.Content = "Подключиться"; // Текст кнопки
            }
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipTextBox.Text;
            string portText = portTextBox.Text;

            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(portText))
            {
                MessageBox.Show("Введите IP и порт!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(portText, out int port))
            {
                MessageBox.Show("Некорректный формат порта!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_isServer)
                {
                    _server = new Server(ip, port);
                    _server.Start();
                    MessageBox.Show($"Сервер запущен на {ip}:{port}! Ожидание клиента...", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Ожидание подключения клиента
                    await Task.Run(() =>
                    {
                        while (!_server.HasClientConnected)
                        {
                            Task.Delay(100).Wait();
                        }
                    });

                    MessageBox.Show("Клиент подключился! Начинаем игру.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    _server.SendStartGameSignal();
                }
                else
                {
                    _client = new Client(ip, port);
                    string response = await _client.SendRequestAsync("{\"action\": \"connect\"}");

                    if (response.Contains("connected"))
                    {
                        MessageBox.Show("Успешно подключено к серверу!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Ожидание запуска игры
                        await _client.WaitForGameStartAsync();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось подключиться к серверу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                // Запуск игрового окна
                MainWindow gameWindow = new MainWindow();
                gameWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки Cancel (если он есть в XAML)
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
