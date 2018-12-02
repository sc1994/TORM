using Newtonsoft.Json;

namespace ORM
{
    internal class Redis
    {
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        internal static void Publish(string channel, object msg)
        {
            var sub = Stores.RedisLog.GetSubscriber();
            sub.PublishAsync(channel, JsonConvert.SerializeObject(msg));
        }
    }
}
