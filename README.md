# SqlHelper
(ORM) sql 帮助类 方便sql 的增删改查 操作 

## 依赖项
- Dapper (如果nuget没能还原此包需要手动安装下)
- Configuration (.net 程序集中引用此依赖)

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
sh.AddWhere("Id",666); // 更新条件
sh.Update(); // 仔细更新
```
##### 删除
```
var sh = new SqlHelper<User>();
sh.AddWhere("Id",666); // 删除条件,可为多
sh.Delete(); // 执行删除
```
```
var sh = new SqlHelper<User>();
sh.DeleteByPrimaryKey(666); // 依据主键删除
```
```
var sh = new SqlHelper<User>();
sh.Delete(" AND Id = 666 "); // 删除满足当前条件的数据
```
### 开坑
- [ ] 代码拆分
- [ ] 支持多个地址链接
- [ ] 支持多个库的操作
- [ ] 精简 Method 命名
- [ ] 配置显示字段的方法重载 Expression 参数

