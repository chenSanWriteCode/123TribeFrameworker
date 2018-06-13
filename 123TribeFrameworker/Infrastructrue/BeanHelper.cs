using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using _123TribeFrameworker.Entity;

namespace _123TribeFrameworker.Infrastructrue
{
    public class BeanHelper
    {
        public static void CopyBean<Aim, Source>(ref Aim aim, Source source) where Aim : class, new() where Source : class, new()
        {
            try
            {
                var Types = source.GetType();//获得类型  
                var Typea = typeof(Aim);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo ap in Typea.GetProperties())
                    {
                        if (ap.Name == sp.Name && ap.PropertyType.Name == sp.PropertyType.Name)//判断属性名是否相同  
                        {
                            ap.SetValue(aim, sp.GetValue(source, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception)
            {
                aim = new Aim();
            }
        }
        /// <summary>
        /// 整合结果
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result<object> combineResult(params Result<object>[] results)
        {
            Result<object> result = new Result<object>();
            if (results!=null)
            {
                foreach (var item in results)
                {
                    if (!item.result)
                    {
                        result.addError(item.message);
                    }
                }
            }
            return result;
        }


    }
}