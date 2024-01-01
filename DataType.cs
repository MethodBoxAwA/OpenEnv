/*
 * @Author 拟南芥
 * @Time 2023/12/31
 * @Description 用于存储数据类型的类
 */

namespace OpenEnv
{
    internal class DataType
    {
        internal enum BehaviorType
        {
            Signal,
            OnStart,
            OnClock,
            OnTrigger,
            OnShutDown
        }
    }
}
