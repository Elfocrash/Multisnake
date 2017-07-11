using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Multisnake
{
    public class GameManager
    {
        private static GameManager instance;
        private static readonly object padLock = new object();
        public ConcurrentDictionary<string, Snake> Snakes { get; set; }
        public Timer Timer;

        public static GameManager Instance
        {
            get
            {
                lock (padLock)
                {
                    return instance ?? (instance = new GameManager());
                }
            }
        }

        public void Initilize()
        {
            Snakes = new ConcurrentDictionary<string, Snake>();
            Timer = new Timer(Callback, null,0,1000/15);
        }

        private void Callback(object state)
        {
            var listOfSnakes = JsonConvert.SerializeObject(Snakes.Values);
            Startup.ServiceProvider.GetRequiredService<SnakeHandler>()
                .InvokeClientMethodToAllAsync("pingSnakes", listOfSnakes)
                .Wait();
        }
    }
}