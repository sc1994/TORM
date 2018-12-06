using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Explain
{
    public abstract class BaseExplain<T> : IExplain where T : Expression
    {
        public abstract void Explain(T exp, Content info);

        public void Explain(Expression exp, Content info)
        {
            try
            {
                Explain(exp as T, info);
            }
            catch (Exception ex)
            {
                ExplainTool.RedisSub.PublishAsync("ExplainErrorLog", JsonConvert.SerializeObject(new
                {
                    ex.Message,
                    ex.HelpLink,
                    ex.StackTrace,
                    ex.Source,
                    All = ex.ToString()
                }));
                throw;
            }
        }
    }
}
