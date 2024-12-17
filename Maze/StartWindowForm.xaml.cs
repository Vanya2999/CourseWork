using System;
using System.Windows;


namespace Maze
{
    public partial class StartWindowForm : Window
    {
        public StartWindowForm()
        {
            InitializeComponent();
        }

        // Обработчик кнопки "Создать сервер"
        private void CreateServerButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на окно ввода параметров для создания сервера
            var serverWindow = new ServerConnectWindow(true); // true - флаг, что создаётся сервер
            serverWindow.Show();
            this.Close();
        }

        // Обработчик кнопки "Присоединиться к серверу"
        private void JoinServerButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на окно ввода параметров для подключения клиента
            var clientWindow = new ServerConnectWindow(false); // false - флаг, что клиент подключается к серверу
            clientWindow.Show();
            this.Close();
        }

        // Обработчик кнопки "Выход"
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
