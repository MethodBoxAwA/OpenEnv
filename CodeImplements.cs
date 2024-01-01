using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEnv
{
    internal class CodeImplements
    {
        internal interface IGenericExecutable
        {
            /// <summary>
            /// 方法或语句的名称
            /// </summary>
            string Name { get; set; }

            /// <summary>
            /// 方法或语句的参数列表
            /// </summary>
            string[]? Parameters { get; set; }
        }

        /// <summary>
        /// 表示语句
        /// </summary>
        internal class Statement : IGenericExecutable
        {
            public string Name { get; set; } = "if";

            public string[]? Parameters { get; set; }

            public override string ToString()
            {
                return Executables is not null
                    ?$"语句类型:{Name}，包含的成员数：{Executables.Length}。"
                    :$"语句类型:{Name}，包含的成员数：0。";
            }

            /// <summary>
            /// 语句包含的可执行内容
            /// </summary>
            IGenericExecutable[]? Executables { get; set; }
        }

        /// <summary>
        /// 表示方法
        /// </summary>
        internal class Method : IGenericExecutable
        {
            public string Name { get; set; } = "Exit";

            public string[]? Parameters { get; set; }

            public override string ToString()
            {
                return Parameters is not null
                    ? $"方法名:{Name}，参数个数：{Parameters.Length}。"
                    : $"方法名:{Name}，参数个数：0。";
            }
        }

        /// <summary>
        /// 表示直接的动作或关键字
        /// </summary>
        internal class Verb : IGenericExecutable
        {
            public string Name { get; set; } = "run";

            public string[]? Parameters { get; set; }

            public override string ToString()
            {
                return Parameters is not null
                    ? $"关键字:{Name}，参数个数：{Parameters.Length}。"
                    : $"关键字:{Name}，参数个数：0。";
            }
        }
    }
}