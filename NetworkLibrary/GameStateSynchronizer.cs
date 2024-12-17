using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace NetworkLibrary
{
    public class GameStateSynchronizer
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl;

        public GameStateSynchronizer(string serverUrl)
        {
            _serverUrl = serverUrl;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Отправляет текущее состояние игры на сервер.
        /// </summary>
        public async Task<bool> SendGameStateAsync(GameState state)
        {
            try
            {
                string json = JsonSerializer.Serialize(state);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_serverUrl + "/update", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки состояния: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Получает состояние игры с сервера.
        /// </summary>
        public async Task<GameState> FetchGameStateAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_serverUrl + "/state");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<GameState>(json);
                }
                else
                {
                    Console.WriteLine($"Ошибка получения состояния: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения состояния: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Освобождение ресурсов.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }

    /// <summary>
    /// Представляет состояние игры.
    /// </summary>
    public class GameState
    {
        public List<PlayerState> Players { get; set; } = new List<PlayerState>();
    }

    /// <summary>
    /// Представляет состояние игрока.
    /// </summary>
    public class PlayerState
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Health { get; set; }
        public int Ammo { get; set; }
        public bool IsAlive { get; set; }
    }
}
