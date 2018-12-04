﻿using Monito.Models;
using Newtonsoft.Json;
using ORM;
using StackExchange.Redis;
using System;

namespace Monito.Methods
{
    public class Redis
    {
        private static string _config = "118.24.27.231:6379";
        private static readonly ConnectionMultiplexer _conn = ConnectionMultiplexer.Connect(_config);

        public static void Subscriber()
        {
            var sub = _conn.GetSubscriber();
            sub.Subscribe("LogSql",
                          (channel, message) =>
                          {
                              try
                              {

                                  var info = JsonConvert.DeserializeObject<SqlLog>(message);
                                  TORM.Insert(info);
                              }
                              catch (Exception e)
                              {
                                  _conn.GetDatabase().ListRightPush("Error:Subscriber:" + DateTime.Today.ToShortDateString(), e.ToString());
                              }
                          });
        }
    }
}