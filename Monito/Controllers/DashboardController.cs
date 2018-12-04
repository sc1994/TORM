using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Monito.Models;
using ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monito.Controllers
{
    public class DashboardController : Controller
    {
        private int _total = 1000;
        /// <summary>
        /// color
        /// </summary>
        private readonly List<string> _color = new List<string>
                                      {
                                          "#f44336",
                                          "#e91e63",
                                          "#9c27b0",
                                          "#673ab7",
                                          "#3f51b5",
                                          "#2196f3",
                                          "#00bcd4",
                                          "#009688",
                                          "#8bc34a",
                                          "#cddc39",
                                          "#ff9800"
                                      };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        [HttpPost]
        public JsonResult GetAllData(DateTime start, DateTime end)
        {
            var particle = (end - start).TotalSeconds / _total; // 确定粒度
            var date = new List<string>
                       {
                           $"{start:M-d H:m}/粒度:{particle:0}s"
                       };
            var sql = new StringBuilder();
            for (var i = 0; i < _total; i++)
            {
                var e = start.AddSeconds(particle * (i + 1));
                if (i != 0)
                {
                    sql.Append("\r\n");
                }

                sql.Append($"SELECT COUNT(1) as Value FROM SqlLog WHERE EndTime > '{start.AddSeconds(particle * i):yyyy-MM-dd HH:mm:ss}' AND EndTime < '{e:yyyy-MM-dd HH:mm:ss}'");
                if (i != _total - 1)
                {
                    sql.Append(" UNION ALL");
                }

                date.Add($"{e:M-d H:m}/粒度:{particle:0}s");
            }
            var data = TORM.Query<Data>(sql.ToString()).Select(x => x.Value);
            return Json((data, date));
        }

        [HttpPost]
        public JsonResult GetDbData(DateTime start, DateTime end)
        {
            var result = Get(start, end, "SELECT COUNT(1) as Value FROM SqlLog WHERE DbName = '{item}' AND EndTime > '{s:yyyy-MM-dd HH:mm:ss}' AND EndTime < '{ }'");
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetErrorData(DateTime start, DateTime end)
        {
            var particle = (end - start).TotalSeconds / _total; // 确定粒度
            return null;
        }

        private dynamic Get(DateTime start, DateTime end, string sqlStr)
        {
            var particle = (end - start).TotalSeconds / _total; // 确定粒度
            var date = new List<string>
                       {
                           $"{start:M-d H:m}/粒度:{particle:0}s"
                       };

            var dataType = TORM.Query<SqlLog>()
                               .Select(x => x.DbName)
                               .Group(x => x.DbName)
                               .Find<string>();
            var data = new ArrayList();

            foreach (var item in dataType)
            {
                var sql = new StringBuilder();
                for (var i = 0; i < _total; i++)
                {
                    var s = start.AddSeconds(particle * i);
                    var e = start.AddSeconds(particle * (i + 1));
                    if (i != 0)
                    {
                        sql.Append("\r\n");
                    }
                    sql.AppendFormat(sqlStr, item, $"{s:yyyy-MM-dd HH:mm:ss}", $"{e:yyyy-MM-dd HH:mm:ss}");
                    if (i != _total - 1)
                    {
                        sql.Append(" UNION ALL");
                    }
                    if (date.Count < _total)
                        date.Add($"{e:M-d H:m}/粒度:{particle:0}s");
                }
                data.Add(new
                {
                    name = item,
                    type = "line",
                    smooth = true,
                    symbol = "none",
                    sampling = "average",
                    itemStyle = new
                    {
                        color = _color[dataType.IndexOf(item)]
                    },
                    data = TORM.Query<Data>(sql.ToString()).Select(x => x.Value)
                });
            }

            var t = end.Ticks - start.Ticks;
            var t2 = DateTime.Now.Ticks - start.Ticks;
            var t3 = t2 / (t / 100);
            var scope = new[] { t3 - 16, t3 };
            return new
            {
                dataType,
                data,
                date,
                scope
            };
        }
    }

    [Table("Log", DBTypeEnum.MySQL)]
    class Data
    {
        public long Value { get; set; }
    }
}