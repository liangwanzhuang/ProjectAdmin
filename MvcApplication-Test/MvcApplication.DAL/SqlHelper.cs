using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MvcApplication.DAL
{
    /// <summary>
    /// 数据库访问/操作帮助类
    /// </summary>
    public class SqlHelper
    {
        //数据库连接字符串
        public static string str = ConfigurationManager.ConnectionStrings["SKCDB"].ConnectionString;

        #region 非查询T-SQL语句、执行方法
        #region 执行普通文本sql语句
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region 执行存储过程sql方法
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型：CommandType.Text内容类型、CommandType.StoredProcedure存储过程的类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    try
                    {
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return res;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 执行事物sql方法
        /// <summary>
        /// 执行一个非查询的T-SQL语句、事务，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">SqlCommand命令</param>
        /// <param name="trans">事物对象</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, SqlTransaction trans, CommandType cmdType, params SqlParameter[] cmdParms)
        {

            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {

                    con.Open();
                    cmd.CommandText = cmdText;
                    if (trans != null)
                    {
                        cmd.Transaction = trans;
                    }
                    if (cmdParms != null)
                    {
                        cmd.Parameters.AddRange(cmdParms);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region 返回首行首列
        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    try
                    {
                        conn.Open();
                        object res = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        return res;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region 查询的T-SQL语句、执行方法



        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="sql">要执行的T-SQL语句</param>
        /// <param name="ps">参数列表</param>
        /// <returns>查询的数据表</returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] ps)
        {
            return ExecuteDataTable(sql, CommandType.Text, ps);
        }

        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="sql">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="ps">参数列表</param>
        /// <returns>查询的数据表</returns>
        public static DataTable ExecuteDataTable(string sql, CommandType type, params SqlParameter[] ps)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, str);
            if (ps != null)
            {
                sqlAdapter.SelectCommand.Parameters.AddRange(ps);
                sqlAdapter.SelectCommand.CommandType = type;
            }
            DataTable dt = new DataTable();
            sqlAdapter.Fill(dt);
            return dt;
        }







        #endregion
        #endregion





        #region 修改ExecuteNonQuery
        /// <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库事务处理 
        /// 使用参数数组提供参数
        /// </summary>
        public static object ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region 最新扩展方法
        /// <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库事务处理 
        /// 使用参数数组提供参数
        /// </summary>
        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, params  SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.CommandText = cmdText;
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        
    }

}
