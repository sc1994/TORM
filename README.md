# SqlHelper

（未完成）目的是实现`=>`表达式进行数据库的增删改查。

> ## 优先任务

- 迁移（支持 `model` 到 `table` ，或者 `table` 到 `model` ）

> ### TODO

- 子查询
- 同一个`Where`中的优先级
- 解析表达式性能优化
- 事务
- 异常消息完善
- （可行性不高）sql 缓存，data 缓存
- `Having` 理应遵循 `sql` 书写顺序
- 慢 `sql` 记录
- 嵌套式的数据结构

> ### 数据模型

- 需要指定库名，库类型。表名可不指定，默认类名。

```csharp
[Table("testDB", DBTypeEnum.MySQL, "testTable")]
class testTable
{
    public long ID { get; set; }
}
```

> ### 配置

- ORM 需要知道数据库对应的连接
- 在应用层配置文件 `appsettings.json`

```json
{
  "testDB": "server=localhost;database=testDB;uid=root;pwd=1233333;"
}
```

- 修改应用层的 `.csproj` 文件，添加节点。目的是为了在项目生成的时候，将配置文件生成到对应的文件位置。

```xml
<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

> ### 事项

- 不能支持一个`Where`中使用括号表明优先级如：`Where(x=>x.a == "1" && (x.b == 2 || x.b == 3))`。建议使用`Where(x=>x.a == "1").Where(x.b == 2 || x.b == 3))`，同一个`Where`表示同一个优先级。目前即使错误的写法解释器会选择忽略优先级，需要特别注意踩坑！
- `IN`、`NOT IN` 本应是 `a.Any(x=>x=="1")` 来表示`x IN (a)`，但个人觉的不直观（相对于sql语法）。因写成 `x.In(a)`
- `DateTime.x`情况比较特殊，如：`Where(x => x.Date != DateTime.Today)`，解析器不能直接获取他的值。目前是穷举了`DateTime.`可能出现的状态，
- 子查询，目前依然没有实现子查询。也没有关于子查询的方法较好的封装方式。
- 目前最多支持到13表的`JOIN`
- `Having` 没能遵循sql 书写顺序

> ### 好处

- 参数化（`where`/`having`/`join and`的传入值都是参数化处理）
- 链式：`ORM.Query().Where(x => x.a == "1").OrderA(x => x.b).Find();`
- 使用 `StartsWith`，`Contains`，`EndsWith`。表示`LIKE`的不同状态。`Where(x => x.Name.EndsWith("2"))`
- 强类型限制 ：表达式的左右类型需一致，编译时验证
- 多级表达式：`Where(x => x.a == "1" && x.b == "2" && x.c == 3)`
- 漏斗形的方法筛选 如：`ORM.Query().Select(x => x.a).Where(x => x.b == 1).OrderA(x => x.c).Find()`类似这样的强制调用顺序，规范代码。
- `todo`性能方面

> ### 查询

- 单表

```csharp
var result = ORM.Query<Rules>()
                .Select(x => new object[] { x.CreatedAt, x.ID })
                .Where(x => x.ID > 0)
                .OrderD(x => x.ID)
                .Find()
```

- 多表

```csharp
var result = ORM.Query<Rules, Schedules>()
                .Select((x, y) => new object[] { x.CreatedAt, x.ID, y.Content })
                .JoinL((x, y) => x.ScheduleID == y.ID)
                .Where((x, y) => x.ID > 0 && y.ID > 0)
                .OrderD((x, y) => x.ID)
                .Find<rulesView>(); // 可重新定义返回数据类型
```

- `Select`

```csharp
// 支持多种形式
.Select(x => new object[] { x.CreatedAt, x.ID }) // 当传入多个字段且字段类型不同，需要指定数组为object
.Select(x => new [] { x.CreatedAt, x.UpdateAt })
.Select(x => x.CreatedAt, x => x.ID)
.Select(x => (x.CreatedAt, "Create"), (x => x.ID, "Id")) // 当你希望别名的时候
```

- `Join`内联 /`JoinL`左联 /`JoinR`右联 /`JoinF`全联

```csharp
.Join((x, y) => x.ScheduleID == y.ID && y.ID > 0)
```

```sql
-- 对应sql
JOIN Schedules ON Schedules.ID = Rules.ScheduleID AND Schedules.ID > 0
```

- `Where`

```csharp
.Where(x => x.ScheduleID > 0 && x.ID > 0).Where(x => x.CreatedAt >= DateTime.Now || x.UpdateAt >= DateTime.Now)
```

```sql
-- 对应sql
WHERE 1 = 1 AND (Rules.ScheduleID > 0 AND Rules.ID > 0) AND (Rules.CreatedAt >= '2018-11-22 00:00:00' OR Rules.UpdateAt >= '2018-11-22 00:00:00')
```

- `Group` 和 `Having`

```csharp
.Group(x => x.ScheduleID).Having(x => x.ID > 0)
```

```sql
-- 对应sql
GROUP BY Rules.ScheduleID HAVING Rules.ID > 0
```

- `OrderA`/`OrderD`

```csharp
.OrderA(x => x.ID).OrderD(x => x.ScheduleID)
```

```sql
-- 对应sql
ORDER BY Rules.ID ASC, Rules.ScheduleID DESC
```

> ### 更新

- `todo`

> ### 插入数据

- `todo`

> ### 删除

- `todo`

> ### 事务

- `todo`