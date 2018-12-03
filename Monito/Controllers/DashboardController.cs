using Microsoft.AspNetCore.Mvc;
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
                           start.ToString("yy-M-d HH:mm")
                       };
            var sql = new StringBuilder();
            for (var i = 0; i < _total; i++)
            {
                var s = start.AddSeconds(particle);
                if (i != 0)
                {
                    sql.Append("\r\n");
                }

                sql.Append($"SELECT COUNT(1) as Value FROM SqlLog WHERE EndTime > '{start:yyyy-MM-dd HH:mm:ss}' AND EndTime < '{s:yyyy-MM-dd HH:mm:ss}'");
                if (i != _total - 1)
                {
                    sql.Append(" UNION ALL");
                }

                start = s;
                date.Add(s.ToString("M-d HH:mm"));
            }
            var data = TORM.Query<Data>(sql.ToString()).Select(x => x.Value);
            return Json((data, date));
        }

        [HttpPost]
        public JsonResult GetDbData(DateTime start, DateTime end)
        {
            var total = _total / 10;
            var particle = (end - start).TotalSeconds / total; // 确定粒度
            var date = new List<string>
                       {
                           start.ToString("yy-M-d HH:mm")
                       };

            var dataType = TORM.Query<SqlLog>()
                               .Select(x => x.TableName)
                               .Group(x => x.TableName)
                               .Find<string>();
            var data = new ArrayList();

            foreach (var item in dataType)
            {
                var sql = new StringBuilder();
                for (var i = 0; i < total; i++)
                {
                    var s = start.AddSeconds(particle);
                    if (i != 0)
                    {
                        sql.Append("\r\n");
                    }

                    sql.Append($"SELECT COUNT(1) as Value FROM SqlLog WHERE TableName = '{item}' AND EndTime > '{start:yyyy-MM-dd HH:mm:ss}' AND EndTime < '{s:yyyy-MM-dd HH:mm:ss}'");
                    if (i != total - 1)
                    {
                        sql.Append(" UNION ALL");
                    }

                    start = s;
                    date.Add(s.ToString("M-d HH:mm"));
                }
                data.Add(new
                {
                    name = item,
                    type = "line",
                    stack = $"每{particle:0}秒",
                    data = TORM.Query<Data>(sql.ToString()).Select(x => x.Value)
                });
            }

            return Json((dataType, data, date));
        }
    }

    [Table("Log", DBTypeEnum.MySQL)]
    class Data
    {
        public long Value { get; set; }
    }
}