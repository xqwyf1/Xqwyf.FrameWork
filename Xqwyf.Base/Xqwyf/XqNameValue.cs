using System;
using System.Collections.Generic;
using System.Text;

namespace  Xqwyf
{

    [Serializable]
    public class XqNameValue : XqNameValue<string>
    {
        public XqNameValue()
        {

        }

        public XqNameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// 能够用于存储 Name/Value (或者 Key/Value)对.
    /// </summary>
    [Serializable]
    public class XqNameValue<T>
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }

        public XqNameValue()
        {

        }

        public XqNameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}
