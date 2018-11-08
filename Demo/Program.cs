using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var name = "asdasd";
            //var id = 123123;

            //Expression<Func<Model, bool>> exp = x => x.Name == "123";

            //Expression<Func<Model, bool>> exp2 = x => x.Id >= 6666;

            //var date = DateTime.Now;
            //for (int i = 0; i < 100000; i++)
            //{
            //    Expression<Func<Model, bool>> exp3 = x => x.Birthday > DateTime.Now.AddDays(i).AddHours(i * 2);
            //    Explain(exp3);
            //}
            //Console.WriteLine((DateTime.Now - date).Milliseconds);

            //Expression<Func<Model, bool>> exp5 = x => x.Birthday > DateTime.Now;

            //Expression<Func<Model, bool>> exp4 = x => x.Name == name.ToUpper().Replace("A", "1");

            //Explain(exp);
            //Explain(exp2);

            //Explain(exp5);
            //Explain(exp4);

            //Expression<Func<Model, Model2, object>> exp2 = (x, y) => x.Id == y.Id2;

            //ExplainTool.Explain(exp2);

            //Expression<Func<Model, []object>> exp5 =x=> ;
            //Expression<Func<Model, object[]>> exp5 = x => new object[] { x.Name, x.Birthday };
            var a = new [] { "1", "2" };
            Console.ReadLine();
        }


        static void Explain(Expression<Func<Model, bool>> exp)
        {
            ExplainTool.Explain(exp);
        }

    }


    public class ExplainTool
    {
        private static readonly string[] Expressions =
        {
            "LambdaExpression",
            "BinaryExpression",
            "MemberExpression",
            "ConstantExpression",
            "MethodCallExpression",
            "UnaryExpression",
            "NewArrayExpression"
        };

        private static readonly Dictionary<string, IExplain> Ports = InitPorts();

        public static void Explain(Expression exp)
        {
            if (exp != null)
            {
                GetPort(exp).Explain(exp);
            }
        }
        /// <summary>
        /// 初始化接口实例
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, IExplain> InitPorts()
        {
            var ports = new Dictionary<string, IExplain>();
            var nameSpace = typeof(ExplainTool).Namespace;
            foreach (var exp in Expressions)
            {
                var type = Type.GetType($"{nameSpace}.Explain{exp.Replace("Expression", "")}");
                ports.Add(exp, (IExplain)Activator.CreateInstance(type ?? throw new InvalidOperationException()));
            }
            return ports;
        }

        /// <summary>
        /// 获取一个对应类型的接口实例
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static IExplain GetPort(Expression exp)
        {
            var type = SwitchExpression(exp);

            return Ports[type];
        }

        /// <summary>
        /// 获取表达的类型描述
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string SwitchExpression(Expression exp)
        {
            switch (exp)
            {
                case BinaryExpression _:
                    return "BinaryExpression";
                case BlockExpression _:
                    return "BlockExpression";
                case ConditionalExpression _:
                    return "ConditionalExpression";
                case ConstantExpression _:
                    return "ConstantExpression";
                case DebugInfoExpression _:
                    return "DebugInfoExpression";
                case DefaultExpression _:
                    return "DefaultExpression";
                case DynamicExpression _:
                    return "DynamicExpression";
                case GotoExpression _:
                    return "GotoExpression";
                case IndexExpression _:
                    return "IndexExpression";
                case InvocationExpression _:
                    return "InvocationExpression";
                case LabelExpression _:
                    return "LabelExpression";
                case LambdaExpression _:
                    return "LambdaExpression";
                case ListInitExpression _:
                    return "ListInitExpression";
                case LoopExpression _:
                    return "LoopExpression";
                case MemberExpression _:
                    return "MemberExpression";
                case MemberInitExpression _:
                    return "MemberInitExpression";
                case MethodCallExpression _:
                    return "MethodCallExpression";
                case NewArrayExpression _:
                    return "NewArrayExpression";
                case NewExpression _:
                    return "NewExpression";
                case ParameterExpression _:
                    return "ParameterExpression";
                case RuntimeVariablesExpression _:
                    return "RuntimeVariablesExpression";
                case SwitchExpression _:
                    return "SwitchExpression";
                case TryExpression _:
                    return "TryExpression";
                case TypeBinaryExpression _:
                    return "TypeBinaryExpression";
                case UnaryExpression _:
                    return "UnaryExpression";
                default:
                    throw new Exception(nameof(exp));
            }

        }
    }

    public interface IExplain
    {
        void Explain(Expression exp);
    }

    public abstract class BaseExplain<T> : IExplain where T : Expression
    {
        public abstract void Explain(T exp);

        public void Explain(Expression exp)
        {
            Explain(exp as T);
        }
    }

    public class ExplainLambda : BaseExplain<LambdaExpression>
    {
        public override void Explain(LambdaExpression exp)
        {
            ExplainTool.Explain(exp.Body);
        }
    }

    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp)
        {
            Console.WriteLine((exp.Left as MemberExpression).Member.Name);
            Console.WriteLine(exp.NodeType.ToString());
            ExplainTool.Explain(exp.Right);
        }
    }

    public class ExplainMember : BaseExplain<MemberExpression>
    {
        public override void Explain(MemberExpression exp)
        {
            if (exp.Expression != null)
            {
                if (exp.Expression is ConstantExpression constant)
                {
                    Console.WriteLine(constant.Value.GetType().InvokeMember(exp.Member.Name, BindingFlags.GetField, null, constant.Value, null));
                }
                else
                {
                    Console.WriteLine(exp.Member.Name);
                }
            }
            else
            {
                //ExplainTool.Explain(exp.Member.);
                Console.WriteLine(exp.Member.Name);
            }
        }
    }

    public class ExplainConstant : BaseExplain<ConstantExpression>
    {
        public override void Explain(ConstantExpression exp)
        {
            Console.WriteLine(exp.Value);
        }
    }

    public class ExplainMethodCall : BaseExplain<MethodCallExpression>
    {
        public override void Explain(MethodCallExpression exp)
        {
            Console.WriteLine(Expression.Lambda(exp).Compile().DynamicInvoke());
        }
    }

    public class ExplainUnary : BaseExplain<UnaryExpression>
    {
        public override void Explain(UnaryExpression exp)
        {
            ExplainTool.Explain(exp.Operand);
            //Console.WriteLine(exp.);
            //throw new NotImplementedException();
        }
    }

    public class ExplainNewArray : BaseExplain<NewArrayExpression>
    {
        public override void Explain(NewArrayExpression exp)
        {
            foreach (var item in exp.Expressions)
            {
                ExplainTool.Explain(item);
            }
        }
    }

    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
    }

    public class Model2
    {
        public int Id2 { get; set; }

        public string Name2 { get; set; }

        public DateTime Birthday2 { get; set; }
    }
}
