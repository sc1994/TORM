using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ORM;

namespace Monito.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        [HttpPost]
        public JsonResult GetData(DateTime start, DateTime end)
        {
            var total = 300;
            var particle = (end - start).TotalSeconds / total; // 确定粒度
            var date = new List<string>
                       {
                           start.ToString("yy-M-d HH:mm")
                       };
            var sql = new StringBuilder();
            for (var i = 0; i < total; i++)
            {
                var s = start.AddSeconds(particle);
                if (i != 0)
                {
                    sql.Append("\r\n");
                }

                sql.Append($"SELECT COUNT(1) as Value FROM SqlLog WHERE EndTime > '{start:yyyy-MM-dd HH:mm:ss}' AND EndTime < '{s:yyyy-MM-dd HH:mm:ss}'");
                if (i != total - 1)
                {
                    sql.Append(" UNION ALL");
                }

                start = s;
                date.Add(s.ToString("M-d HH:mm"));
            }
            var data = TORM.Query<Data>(sql.ToString()).Select(x => x.Value);
            return Json((data, date));
        }
    }

    [Table("Log", DBTypeEnum.MySQL)]
    class Data
    {
        public long Value { get; set; }
    }
}