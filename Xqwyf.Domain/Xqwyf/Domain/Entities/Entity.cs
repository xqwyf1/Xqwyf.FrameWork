using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Xqwyf.Domain.Entities
{
    /// <summary>
    /// 所有实体的基础类
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 获取实体的主键列表
        /// </summary>
        public IDictionary<Type, String> PriKeys
        {
            get
            {
                if (priKeys.Count == 0)
                {
                    foreach (var item in this.GetType().GetProperties())
                    {
                        var priattribute = item.GetCustomAttribute<KeyAttribute>(false);

                        if (priattribute != null)
                        {
                            priKeys.Add(item.PropertyType, item.Name);
                        }
                    }
                };
                return priKeys;
            }
        }

        /// <summary>
        /// 获取主键的字符串
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return PriKeys.JoinAsString(",");
        }

        /// <summary>
        /// 
        /// </summary>
        public int PriKeyNum
        {
            get { return PriKeys.Count; }
        }

        private static IDictionary<Type, String> priKeys = new Dictionary<Type, String>();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Keys = {PriKeys.ToString()}";
        }
    }
}
