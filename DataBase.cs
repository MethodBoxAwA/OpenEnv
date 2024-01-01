/*
 * @Author 拟南芥
 * @Time 2024/1/1
 * @Description 用于数据库相关操作的类
 */

using System.Data.OleDb;

namespace OpenEnv
{
    internal class DataBase
    {
        protected OleDbConnection Connection { get; set; }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="path">数据库路径</param>
        /// <param name="password">数据库密码</param>
        /// <returns>是否成功创建数据库</returns>
        public bool Build(string path,string password) 
        {
            if (Connection is null)
            {
                string connString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path}\data.mdb;Jet OLEDB:Database Password={password}";
                try
                {
                    this.Connection = new OleDbConnection(connString);
                    return true;
                }
                catch
                {
                    return false;
                }
           
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 执行指定的Access SQL语句
        /// </summary>
        /// <param name="command">指定的Access SQL语句</param>
        /// <exception cref="NullReferenceException">如果数据库对象尚未被创建就调用该方法会引发此异常</exception>
        public void Execute(string command)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    throw new NullReferenceException("数据库对象还未创建");
                }
            }
            OleDbCommand odCommand = Connection.CreateCommand();
            odCommand.CommandText = command;
            odCommand.ExecuteNonQuery();

            Connection.Close();
        }

        /// <summary>
        /// 对数据库进行查询
        /// </summary>
        /// <param name="command">指定的Access SQL语句</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">如果数据库对象尚未被创建就调用该方法会引发此异常</exception>
        public OleDbDataReader Query(string command)
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                    throw new NullReferenceException("数据库对象还未创建");
                }
            }
            OleDbCommand odCommand = Connection.CreateCommand();
            odCommand.CommandText = command;
            var result = odCommand.ExecuteReader();

            Connection.Close();
            return result;
        }
    }
}