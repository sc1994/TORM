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
        internal static long Publish(string channel, object msg)
        {
            var sub = Stores.RedisLog.GetSubscriber();
            return sub.Publish(channel, JsonConvert.SerializeObject(msg));
        }
    }
}
