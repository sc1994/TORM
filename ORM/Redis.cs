using Newtonsoft.Json;
using ORM;
using StackExchange.Redis;

namespace Explain
{
    internal class Redis
    {
        private static readonly ISubscriber _sub = Stores.RedisLog.GetSubscriber();
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        internal static void Publish(string channel, object msg)
        {
            _sub.PublishAsync(channel, JsonConvert.SerializeObject(msg));
        }
    }
}
