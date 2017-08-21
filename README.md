# SqlHelper
(ORM) sql 帮助类 方便sql 的增删改查 操作 

## 依赖项
---
  1. Dapper (如果nuget没能还原此包需要手动安装下)
  2. Configuration .net 程序集中引用
---

## 示例
##### 查询
```
var sh = new SqlHelper<User>(); // 需要指定查询返回的对象
sh.AddShow("Name"); //需要显示的字段, 对应了User中的属性
sh.AddShow("Pwd,Sex"); // 可以分开Add也可以一次写进以 , 分割
sh.AddWhere(""); // 添加查询条件 具体参数见代码
sh.AddJoin("","",....); // 添加多表链接 具体参数见代码 (复杂关系可以直接传入链接字符串)
var data = sh.Select(); // 查询返回 IEnumerable<User>
```

