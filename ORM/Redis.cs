using Newtonsoft.Json;

namespace ORM
{
    internal class Redis
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        internal static void PublishAsync(string channel, object msg)
        {
            var sub = Stores.RedisLog.GetSubscriber();
            sub.PublishAsync(channel, JsonConvert.SerializeObject(msg));
        }
    }
}
