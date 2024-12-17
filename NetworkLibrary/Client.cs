using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Client
{
    private readonly string _serverUrl;
    private readonly HttpClient _httpClient;

    public Client(string ipAddress, int port)
    {
        _serverUrl = $"http://{ipAddress}:{port}/";
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Отправка JSON-запроса на сервер и получение ответа.
    /// </summary>
    public async Task<string> SendRequestAsync(string jsonData)
    {
        try
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_serverUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                return $"Ошибка от сервера: {response.StatusCode} - {errorContent}";
            }
        }
        catch (Exception ex)
        {
            return $"Ошибка при отправке запроса: {ex.Message}";
        }
    }

    /// <summary>
    /// Ожидание сигнала от сервера о начале игры.
    /// </summary>
    public async Task WaitForGameStartAsync()
    {
        Console.WriteLine("Ожидание запуска игры от сервера...");
        while (true)
        {
            // Запрос на сервер для проверки статуса игры
            string response = await SendRequestAsync("{\"action\": \"check_start\"}");
            Console.WriteLine($"Ответ сервера: {response}");

            // Проверяем, содержит ли ответ сигнал о начале игры
            if (response.Contains("start_game"))
            {
                Console.WriteLine("Сервер готов начать игру!");
                break;
            }

            // Задержка между запросами (1 секунда)
            await Task.Delay(1000);
        }
    }

    /// <summary>
    /// Завершение работы клиента и освобождение ресурсов.
    /// </summary>
    public void Stop()
    {
        _httpClient.Dispose();
        Console.WriteLine("Клиент завершил работу.");
    }
}
