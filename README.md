# SqlHelper
(ORM) sql 帮助类 方便sql 的增删改查 操作 

## 依赖项
- Dapper (如果nuget没能还原此包需要手动安装下)
- Configuration (.net 程序集中引用此依赖)

## 建议
- 直接将代码文件(.cs) 复制到你的项目中, 不建议生成项目然后拷贝.dll文件到项目中,
- 原因是因为代码还是比较容易理解的,方便你的二次开发(或代码排错)
- 还有就是因为有些api本人不能保证很好理解, 可能在你遇到问题的时候最好能做到单步的调试

## 示例
##### 查询
```
var sh = new SqlHelper<User>(); // 需要指定查询返回的对象
 /*
    关于<User> 
        1. 这边的User会被指认为表名称(如果含有Model之类的后缀会被去除)
        2. 如果你需要返回的值类型不等同于表名
            比如你想返回类型为 UserView 但是实际的表名称为 User 可以使用一参的构造函数
            var sh = new SqlHelper<UserView>("User");
        
 */ 
sh.AddShow("Name"); //需要显示的字段, 对应了User中的属性
sh.AddShow("Pwd,Sex"); // 可以分开Add也可以一次写进以 , 分割
sh.AddWhere(""); // 添加查询条件 具体参数见代码
sh.AddJoin("","",....); // 添加多表链接 具体参数见代码
(复杂关系可以直接传入链接字符串)
/*
    关于 AddJoin 
        1.使用此配置之后/前,需要配置 属性 Alia . 此属性配置了主表的别名
        2.AddJoin 提供了重载 , 需要你仔细的看下参数, 才能知道你当前需要的sql适用与何种重载
*/
var data = sh.Select(); // 查询返回 IEnumerable<User>
```
##### 分页
```
var sh = new SqlHelper<User>
{
  PageConfig = new PageConfig
  {
    // 分页配置信息
  }
};
sh.AddShow("");
.
.
.
var data = sh.Select();
var total = sh.Total;
var sql = sh.SqlString.ToString();
```
##### 新增
```
var sh = new SqlHelper<User>(new User
{
  Name = "123",
  Pwd = "456"
});
sh.Insert(); // 插入
```
##### 更新
```
var sh = new SqlHelper<User>();
sh.AddUpdate("Name","sc"); 
sh.AddUpdate("Sex","先生"); // 添加更新内容
sh.AddWhere("Id","666"); // 更新条件
sh.Update(); // 仔细更新
```
