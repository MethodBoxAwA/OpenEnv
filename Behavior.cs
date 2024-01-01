using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OpenEnv
{
    internal class Behavior
    {
        /// <summary>
        /// 任务行为的名称
        /// </summary>
        string BehaviorName { get; set; } = "DefaultName";

        /// <summary>
        /// 任务行为的描述
        /// </summary>
        string BehaviorDescription { get; set; } = "There is no any description";

        /// <summary>
        /// 任务行为的运行配置
        /// </summary>
        Interval? Interval { get; set; }

        /// <summary>
        /// 任务行为的具体行为树
        /// </summary>
        CodeImplements.Statement TopmostStatement { get; set; }

        public string ToSQL()
        {
            var binary = ExtraConvert.SerializeObject(TopmostStatement);
            var fileName = $@"{Application.StartupPath}\Codes\{new Random().Next(114514, 8888888)}.cde";
            StreamWriter writer = new StreamWriter(fileName);

            writer.Write(binary);
            writer.Flush();
            writer.Close();

            
            Expression<Func<string>> buildExpression = () => string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("INSERT INTO tb_Behaviors(Name,Description,Type,Start,Delta,Trigger,Lang) VALUES (" + $"'{BehaviorName}',", $"'{BehaviorDescription}',"), $"{(int)Interval!.BehaviorType},"), $"{Interval!.StartTime.ToFileTimeUtc()},"), $"{Interval!.DeltaTime},"), $"{Interval!.TriggerTime.ToFileTimeUtc()},"), $"'{fileName}')");
            return buildExpression.Compile()().ToString();
        }
    }

    internal class Interval
    {
        /// <summary>
        /// 任务计划的类型
        /// </summary>
        public DataType.BehaviorType BehaviorType { get; set; }

        /// <summary>
        /// 计划任务的开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 计划任务的重复周期
        /// </summary>
        public int DeltaTime { get; set; }

        /// <summary>
        /// 计划任务的触发时间
        /// </summary>
        public DateTime TriggerTime { get; set; }

        /// <summary>
        /// 重写时间的字符串转换方法
        /// </summary>
        /// <returns>转换的触发时间字符串</returns>
        /// <exception cref="NotImplementedException">当传入了错误的BehaviorType枚举值时触发</exception>
        public override string ToString()
        {
            return BehaviorType switch
            {
                DataType.BehaviorType.Signal => $"{StartTime.ToString("G")}",
                DataType.BehaviorType.OnStart=> $"从{StartTime.ToString("G")}，每次启动",
                DataType.BehaviorType.OnShutDown => $"从{StartTime.ToString("G")}，每次关闭",
                DataType.BehaviorType.OnClock => $"从{StartTime.ToString("G")}，每隔{DeltaTime}分钟",
                DataType.BehaviorType.OnTrigger => $"从{StartTime.ToString("G")}，每当{TriggerTime.ToString("HH:mm:ss")}",
                _ => throw new NotImplementedException()
            };   
        }
    }
}
