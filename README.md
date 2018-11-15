# SqlHelper
（未完成）目的是实现`=>`表达式进行数据库的增删改查。

>### TODO
- 子查询
- 同一个`Where`中的优先级
- 解析表达式性能优化
- 事务
- 异常消息完善
- （可行性不高）sql 缓存，data 缓存
- 更多表的 `JOIN`

>### 事项
- 不能支持一个`Where`中使用括号表明优先级如：`Where(x=>x.a == "1" && (x.b == 2 || x.b == 3))`。建议使用`Where(x=>x.a == "1").Where(x.b == 2 || x.b == 3))`，同一个`Where`表示同一个优先级。目前即使错误的写法解释器会选择忽略优先级，需要特别注意踩坑！
- `IN`、`NOT IN` 本应是 `a.Any(x=>x=="1")` 来表示`x IN (a)`，但个人觉的不直观（相对于sql语法）。因写成 `x.In(a)` 
- 不能支持 `Where(x => x.Name.In(new List<string> { "3", "4" }))`，`In`中直接`new List<T>{ x,x,x }`。除此之外`Where(x => x.Name.In(new List<string> { "3", "4" }.ToArray()))`或者`Where(x => x.Name.In(new []{ "3", "4" }))` 都是可以的，不过`.In`方法限制了数据类型不能为`new List`，无需担心踩坑。
- `DateTime.x`情况比较特殊，如：`Where(x => x.Date != DateTime.Today)`，解析器不能直接获取他的值。目前是穷举了`DateTime.`可能出现的状态，
- 子查询，目前依然没有实现子查询。也没有关于子查询的方法较好的封装方式。
- 目前最多支持到5表的`JOIN`

>### 好处
- 链式：`ORM.Query().Where(x => x.a == "1").OrderA(x => x.b).Find();`
- 使用 `StartsWith`，`Contains`，`EndsWith`。表示`LIKE`的不同状态。`Where(x => x.Name.EndsWith("2"))`
- 强类型限制 ：表达式的左右类型需一致，编译时验证
- 多级表达式：`Where(x => x.a == "1" && x.b == "2" && x.c == 3)`
- 漏斗形的方法筛选 如：`ORM.Query().Select(x => x.a).Where(x => x.b == 1).OrderA(x => x.c).Find()`类似这样的强制调用顺序，规范代码。
- `todo`性能方面

>### 查询
- `todo`

>### 更新
- `todo`

>### 插入数据
- `todo`

>### 删除
- `todo`