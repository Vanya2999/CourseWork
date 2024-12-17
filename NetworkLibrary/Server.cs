using System;
using System.IO;
using System.Net;
using System.Text;

public class Server
{
    private HttpListener _listener;
    private bool _isRunning;
    private bool _hasClientConnected; // Флаг подключения клиента

    public bool HasClientConnected => _hasClientConnected; // Свойство для проверки подключения

    public Server(string ipAddress, int port)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://{ipAddress}:{port}/");
        _hasClientConnected = false;
    }

    public void Start()
    {
        _isRunning = true;
        _listener.Start();
        Console.WriteLine("HTTP-сервер запущен...");
        ListenForRequests();
    }

    private async void ListenForRequests()
    {
        while (_isRunning)
        {
            try
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                HandleRequest(context);
            }
            catch (Exception ex)
            {
                if (_isRunning)
                    Console.WriteLine($"Ошибка сервера: {ex.Message}");
            }
        }
    }

    private void HandleRequest(HttpListenerContext context)
    {
        try
        {
            string requestBody;
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }

            Console.WriteLine($"Получен запрос: {requestBody}");

            if (requestBody.Contains("connect"))
            {
                Console.WriteLine("Игрок подключился!");
                _hasClientConnected = true;

                string responseJson = "{\"status\": \"connected\", \"message\": \"Игрок подключён.\"}";
                SendResponse(context, 200, responseJson);
            }
            else if (requestBody.Contains("check_start"))
            {
                if (_hasClientConnected)
                {
                    string responseJson = "{\"status\": \"ok\", \"action\": \"start_game\"}";
                    SendResponse(context, 200, responseJson);
                    Console.WriteLine("Сигнал о начале игры отправлен клиенту.");
                }
                else
                {
                    string responseJson = "{\"status\": \"waiting\", \"message\": \"Ожидание подключения клиента...\"}";
                    SendResponse(context, 200, responseJson);
                }
            }
            else
            {
                string errorResponse = "{\"status\": \"error\", \"message\": \"Неверный запрос.\"}";
                SendResponse(context, 400, errorResponse);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке запроса: {ex.Message}");
        }
    }

    private void SendResponse(HttpListenerContext context, int statusCode, string responseJson)
    {
        byte[] responseData = Encoding.UTF8.GetBytes(responseJson);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        context.Response.OutputStream.Write(responseData, 0, responseData.Length);
        context.Response.OutputStream.Close();
    }

    public void SendStartGameSignal()
    {
        if (_hasClientConnected)
        {
            Console.WriteLine("Отправка сигнала о начале игры клиенту.");
            // Здесь можно реализовать дополнительную логику отправки
        }
        else
        {
            Console.WriteLine("Клиент ещё не подключился.");
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _listener.Stop();
        Console.WriteLine("Сервер завершил работу.");
    }
}
