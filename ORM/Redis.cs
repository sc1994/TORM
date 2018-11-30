using Newtonsoft.Json;
using StackExchange.Redis;

namespace ORM
{
    internal class Redis
    {
        private static ConnectionMultiplexer _redis
        {
            get
            {
                var config = Tools.GetAppSetting("redis");
                var conn = ConnectionMultiplexer.Connect(config);
                // todo 连接错误
                return conn;
            }
        }

        internal static void PublishAsync(string channel, object msg)
        {
            var sub = _redis.GetSubscriber();
            sub.PublishAsync(channel, JsonConvert.SerializeObject(msg));
        }
    }
}
