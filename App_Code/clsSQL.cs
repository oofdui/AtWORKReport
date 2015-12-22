using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;

/// <summary>
/// คลาสจัดการเกี่ยวกับฐานข้อมูลทั้งหมด เช่น Insert Update Query หรือ ตัวจัดการเกี่ยวกับคำสั่ง SQL
/// </summary>
public class clsSQL
{
	public clsSQL()
	{
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	}

    public enum DBType
    {
        /// <summary>
        /// ใช้กับฐานข้อมูล SQL Server
        /// </summary>
        SQLServer,
        /// <summary>
        /// ใช้กับฐานข้อมูล MySQL
        /// </summary>
        MySQL,
        /// <summary>
        /// ใช้กับฐานข้อมูล ODBC
        /// </summary>
        ODBC
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// clsSQL.Bind("SELECT * FROM member",clsSQL.DBType.MySQL,"cs");
    /// </example>
	public DataTable Bind(string strSql, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        DataTable dt = new DataTable();

        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType==DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlDataAdapter myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcDataAdapter myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                myDa_ODBC.Fill(dt);
                myConn_ODBC.Dispose();
                myDa_ODBC.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlDataAdapter myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(strSql, myConn_MySQL);

                myDa_MySQL.Fill(dt);
                myConn_MySQL.Dispose();
                myDa_MySQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlDataAdapter myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Execute คำสั่ง SQL แล้วเก็บค่าที่ได้ใส่ DataTable โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// dt = Bind(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
	public DataTable Bind(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        DataTable dt = new DataTable();
        int i;

        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlDataAdapter myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcDataAdapter myDa_ODBC = new OdbcDataAdapter(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_ODBC.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_ODBC.Fill(dt);
                myConn_ODBC.Dispose();
                myDa_ODBC.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlDataAdapter myDa_MySQL = new MySql.Data.MySqlClient.MySqlDataAdapter(strSql, myConn_MySQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_MySQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_MySQL.Fill(dt);
                myConn_MySQL.Dispose();
                myDa_MySQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlDataAdapter myDa_SQL = new SqlDataAdapter(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myDa_SQL.SelectCommand.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                myDa_SQL.Fill(dt);
                myConn_SQL.Dispose();
                myDa_SQL.Dispose();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return dt;
                }
                else
                {
                    dt.Dispose();
                    return null;
                }
            }
        }
        else
        {
            return null;
        }
    }
	
    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// clsSQL.Return("SELECT MAX(id) FROM member",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public string Return(string strSql, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        string strReturn = "";

        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcCommand myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlCommand myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(strSql, myConn_MySQL);
                try
                {
                    myConn_MySQL.Open();
                    strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                    myConn_MySQL.Close();
                    myCmd_MySQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_MySQL.Dispose();
                    myConn_MySQL.Close();
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
        }
        return strReturn;
    }

    /// <summary>
    /// Execute คำสั่ง SQL ที่คืนค่าเป็นค่าเดียว โดยสามารถใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>ข้อมูล</returns>
    /// <example>
    /// strSQL.Append("SELECT email FROM member WHERE id=?ID");
    /// lblMessage.Text = clsSQL.Return(strSQL.ToString(), new string[,] { { "?ID", txtTest.Text } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
	public string Return(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        string strReturn = "";
        int i;

        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcCommand myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_ODBC.Open();
                    strReturn = myCmd_ODBC.ExecuteScalar().ToString();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlCommand myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(strSql, myConn_MySQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_MySQL.Open();
                    strReturn = myCmd_MySQL.ExecuteScalar().ToString();
                    myConn_MySQL.Close();
                    myCmd_MySQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_MySQL.Dispose();
                    myConn_MySQL.Close();
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }
                try
                {
                    myConn_SQL.Open();
                    strReturn = myCmd_SQL.ExecuteScalar().ToString();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
        }
        return strReturn;
    }
	
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("DELETE FROM member WHERE id=1",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public bool Execute(string strSql, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        bool boolReturn;

        if (!string.IsNullOrEmpty(csSQL))
        {
            if (dbType == DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcCommand myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);
                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlCommand myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(strSql, myConn_MySQL);
                try
                {
                    myConn_MySQL.Open();
                    myCmd_MySQL.ExecuteNonQuery();
                    myConn_MySQL.Close();
                    myCmd_MySQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_MySQL.Dispose();
                    myConn_MySQL.Close();
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);
                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
        }
        else
        {
            boolReturn = false;
        }
        return boolReturn;
    }
	
    /// <summary>
    /// Execute คำสั่ง SQL ที่ใช้ในการบันทึกข้อมูล โดยสามารถระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strSql">SQL Query</param>
    /// <param name="arrParameter">SQL Parameter (new string[,] { { "?ID", txtTest.Text } })</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Execute("UPDATE webboard_type SET type_name=?NAME WHERE type_id=?ID", new string[,] { { "?ID", txtTest.Text }, { "?NAME", "ใช้ Array 2 มิติ" } }, clsSQL.DBType.MySQL, "cs");
    /// </example>
	public bool Execute(string strSql, string[,] arrParameter, DBType dbType, string appsetting_name)
    {
        string csSQL = System.Configuration.ConfigurationManager.AppSettings[appsetting_name];
        bool boolReturn;
        int i;

        if (!string.IsNullOrEmpty(csSQL) && arrParameter.Rank==2)
        {
            if (dbType == DBType.SQLServer)
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
            else if (dbType == DBType.ODBC)
            {
                OdbcConnection myConn_ODBC = new OdbcConnection(csSQL);
                OdbcCommand myCmd_ODBC = new OdbcCommand(strSql, myConn_ODBC);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_ODBC.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_ODBC.Open();
                    myCmd_ODBC.ExecuteNonQuery();
                    myConn_ODBC.Close();
                    myCmd_ODBC.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_ODBC.Dispose();
                    myConn_ODBC.Close();
                }
            }
            else if (dbType == DBType.MySQL)
            {
                MySql.Data.MySqlClient.MySqlConnection myConn_MySQL = new MySql.Data.MySqlClient.MySqlConnection(csSQL);
                MySql.Data.MySqlClient.MySqlCommand myCmd_MySQL = new MySql.Data.MySqlClient.MySqlCommand(strSql, myConn_MySQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_MySQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_MySQL.Open();
                    myCmd_MySQL.ExecuteNonQuery();
                    myConn_MySQL.Close();
                    myCmd_MySQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_MySQL.Dispose();
                    myConn_MySQL.Close();
                }
            }
            else
            {
                SqlConnection myConn_SQL = new SqlConnection(csSQL);
                SqlCommand myCmd_SQL = new SqlCommand(strSql, myConn_SQL);

                for (i = 0; i < arrParameter.Length / arrParameter.Rank; i++)
                {
                    myCmd_SQL.Parameters.AddWithValue(arrParameter[i, 0], arrParameter[i, 1]);
                }

                try
                {
                    myConn_SQL.Open();
                    myCmd_SQL.ExecuteNonQuery();
                    myConn_SQL.Close();
                    myCmd_SQL.Dispose();
                    boolReturn = true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    boolReturn = false;
                }
                finally
                {
                    myCmd_SQL.Dispose();
                    myConn_SQL.Close();
                }
            }
        }
        else
        {
            boolReturn = false;
        }
        return boolReturn;
    }
	
    /// <summary>
    /// Insert ข้อมูลลงฐานข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strField">ชื่อฟิลด์ (hn;pname)</param>
    /// <param name="strData">ข้อมูล ('15-12-20102';'นาย')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="alert_error">True=กรณีเกิด Error ให้แสดงคำเตือน , False=ไม่แสดง</param>
    /// <param name="alert_label_name">ชื่อ Label ที่ใช้แสดงคำเตือน Error ถ้าไม่ระบุ จะแสดงเป็น MessageBox</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Insert("TableName", "hn;pname", "'15-12-20102';'นาย'", clsSQL.DBType.MySQL, "cs", true, "lblWarn");
    /// </example>
	public bool Insert(string strTable, string strField, string strData, DBType dbType, string appsetting_name, bool alert_error,string alert_label_name)
    {
        bool rtnValue = false;
        string[] arrField = strField.Split(';');
        string[] arrData = strData.Split(';');
        if (arrField.Length == arrData.Length)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(strTable) && !string.IsNullOrEmpty(strField) && !string.IsNullOrEmpty(strData) && !string.IsNullOrEmpty(appsetting_name))
            {
                strSql.Append("INSERT INTO ");
                strSql.Append(strTable);
                strSql.Append("(");
                strSql.Append(strField.Replace(";", ","));
                strSql.Append(")");
                strSql.Append("VALUES");
                strSql.Append("(");
                strSql.Append(strData.Replace(";", ","));
                strSql.Append(")");
                if (Execute(strSql.ToString(), dbType, appsetting_name))
                {
                    rtnValue = true;
                    strSql.Length = 0; strSql.Capacity = 0;
                }
                else
                {
                    if (alert_error == true)
                    {
                        string strError = "เกิดข้อผิดพลาดที่คำสั่ง : " + strSql.ToString();
                        if (!string.IsNullOrEmpty(alert_label_name))
                        {
                            Page page = (Page)HttpContext.Current.Handler;
                            Label lblAlert=(Label)page.FindControl(alert_label_name);
                            if(lblAlert!=null)
                            {
                                lblAlert.Text = strError;
                            }
                        }
                        else
                        {
                            clsJS clsJS = new clsJS();
                            clsJS.Alert(strError);
                        }
                    }
                    strSql.Length = 0; strSql.Capacity = 0;
                    rtnValue = false;
                }
            }
        }
        else
        {
            if (alert_error == true)
            {
                string strError = "เกิดข้อผิดพลาด : Table(" + strTable + ") จำนวน Field(" + arrField.Length.ToString() + ") และ Data(" + arrData.Length.ToString() + ") ไม่เท่ากัน";
                if (!string.IsNullOrEmpty(alert_label_name))
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    Label lblAlert = (Label)page.FindControl(alert_label_name);
                    if (lblAlert != null)
                    {
                        lblAlert.Text = strError;
                    }
                }
                else
                {
                    clsJS clsJS = new clsJS();
                    clsJS.Alert(strError);
                }
            }
        }
        return rtnValue;
    }

    /// <summary>
    /// Insert ข้อมูลลงฐานข้อมูล โดยส่งค่ามาเป็นลิส
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strField">ลิสของชื่อฟิลด์</param>
    /// <param name="strData">ลิสของข้อมูล</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="alert_error">True=กรณีเกิด Error ให้แสดงคำเตือน , False=ไม่แสดง</param>
    /// <param name="alert_label_name">ชื่อ Label ที่ใช้แสดงคำเตือน Error ถ้าไม่ระบุ จะแสดงเป็น MessageBox</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    public bool Insert(string strTable, string[] strField, string[] strData, DBType dbType, string appsetting_name, bool alert_error, string alert_label_name)
    {
        #region Remark
        /*############################ Example ############################
        Insert ข้อมูลด้วย Array
        clsSQL.Insert("STAFF",new string[]{"st_id","usern"},new string[]{"'151246'","'Oofdui'"},"SQL","cs",true,"lblMessage");
        #################################################################*/
        #endregion

        StringBuilder strSql = new StringBuilder();
        bool rtnValue = false;
        int i;

        if (strField.Length == strData.Length)
        {
            strSql.Append("INSERT INTO ");
            strSql.Append(strTable);
            strSql.Append("(");

            for (i = 0; i < strField.Length; i++)
            {
                strSql.Append(strField[i]);

                if (i < strField.Length - 1)
                {
                    strSql.Append(",");
                }
            }

            strSql.Append(")");
            strSql.Append("VALUES");
            strSql.Append("(");

            for (i = 0; i < strData.Length; i++)
            {
                strSql.Append(strData[i]);

                if (i < strData.Length - 1)
                {
                    strSql.Append(",");
                }
            }

            strSql.Append(")");

            if (Execute(strSql.ToString(), dbType, appsetting_name))
            {
                rtnValue = true;
                strSql.Length = 0; strSql.Capacity = 0;
            }
            else
            {
                if (alert_error == true)
                {
                    string strError = "เกิดข้อผิดพลาดที่คำสั่ง : " + strSql.ToString();
                    if (!string.IsNullOrEmpty(alert_label_name))
                    {
                        Page page = (Page)HttpContext.Current.Handler;
                        Label lblAlert = (Label)page.FindControl(alert_label_name);
                        if (lblAlert != null)
                        {
                            lblAlert.Text = strError;
                        }
                        else
                        {
                            clsJS clsJS = new clsJS();
                            clsJS.Alert(strError);
                        }
                    }
                    else
                    {
                        clsJS clsJS = new clsJS();
                        clsJS.Alert(strError);
                    }
                }
                strSql.Length = 0; strSql.Capacity = 0;
                rtnValue = false;
            }
        }
        else
        {
            if (alert_error == true)
            {
                string strError = "เกิดข้อผิดพลาด : Table(" + strTable + ") จำนวน Field(" + strField.Length.ToString() + ") และ Data(" + strData.Length.ToString() + ") ไม่เท่ากัน";
                if (!string.IsNullOrEmpty(alert_label_name))
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    Label lblAlert = (Label)page.FindControl(alert_label_name);
                    if (lblAlert != null)
                    {
                        lblAlert.Text = strError;
                    }
                    else
                    {
                        clsJS clsJS = new clsJS();
                        clsJS.Alert(strError);
                    }
                }
                else
                {
                    clsJS clsJS = new clsJS();
                    clsJS.Alert(strError);
                }
            }
        }
        return rtnValue;
    }

    /// <summary>
    /// Insert ข้อมูลลงฐานข้อมูล โดยส่งค่าฟิลด์และข้อมูลมาเป็นลิส และ ใช้ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสชื่อฟิลด์และข้อมูล (new string[,] { { "region_id", "?ID" } })</param>
    /// <param name="arrParameter">SQL Parameter (new string[,]{{"?ID","3"}})</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">คืน SQL Query ที่โปรแกรมสร้างให้</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();string outSQL;
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     "MySQL","cs",out outSQL
    /// );
    /// clsSQL.Insert(
    ///     "PROVINCE",
    ///     new string[,] { { "region_id", "?ID" }, { "province_id", "79" }, { "province_name", "'ทดสอบ 2'" },{"province_sort","99"} }, 
    ///     new string[,]{{"?ID","3"}},
    ///     clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// </example>
    public bool Insert(string strTable, string[,] arrValue, string[,] arrParameter, DBType dbType, string appsetting_name, out string outSQL)
    {
        bool boolReturn = false;
        outSQL = "";
        StringBuilder strSQL = new StringBuilder();
        int i;

        if (arrValue.Rank == 2)
        {
            strSQL.Append("INSERT INTO ");
            strSQL.Append(strTable);
            strSQL.Append("(");

            //########## Field ##########
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }

            strSQL.Append(")VALUES(");

            //########## Value ##########
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }

            strSQL.Append(");");

            outSQL = strSQL.ToString();
            boolReturn = Execute(strSQL.ToString(), arrParameter, dbType, appsetting_name);
        }

        return boolReturn;
    }
    
    /// <summary>
    /// Update ข้อมูลในฐานข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strField">ชื่อฟิลด์ (hn;pname)</param>
    /// <param name="strData">ข้อมูล ('15-12-20102';'นาย')</param>
    /// <param name="strWhere">เงื่อนไข WHERE (hn='15-12-10000')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="alert_error">True=กรณีเกิด Error ให้แสดงคำเตือน , False=ไม่แสดง</param>
    /// <param name="alert_label_name">ชื่อ Label ที่ใช้แสดงคำเตือน Error ถ้าไม่ระบุ จะแสดงเป็น MessageBox</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Update("TableName", "hn;pname", "'15-12-20102';'นาย'","hn='15-12-10000'", clsSQL.DBType.MySQL, "cs", true, "lblWarn");
    /// </example>
    public bool Update(string strTable, string strField, string strData, string strWhere, DBType dbType, string appsetting_name, bool alert_error, string alert_label_name)
    {
        bool rtnValue = false;
        string[] arrField = strField.Split(';');
        string[] arrData = strData.Split(';');
        if (arrField.Length == arrData.Length)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(strTable) && !string.IsNullOrEmpty(strField) && !string.IsNullOrEmpty(strData) && !string.IsNullOrEmpty(strWhere) && !string.IsNullOrEmpty(appsetting_name))
            {
                int i = 0;
                strSql.Append("UPDATE ");
                strSql.Append(strTable);
                strSql.Append(" SET ");
                for (i = 0; i <= arrField.Length-1; i++)
                {
                    if (!string.IsNullOrEmpty(arrField[i]) && !string.IsNullOrEmpty(arrData[i]))
                    {
                        strSql.Append(arrField[i].ToString());
                        strSql.Append("=");
                        strSql.Append(arrData[i].ToString());
                        if (i < arrField.Length-1)
                        {
                            strSql.Append(",");
                        }
                    }
                }
                strSql.Append(" WHERE ");
                strSql.Append(strWhere);
                
                if (Execute(strSql.ToString(), dbType, appsetting_name))
                {
                    rtnValue = true;
                    strSql.Length = 0; strSql.Capacity = 0;
                }
                else
                {
                    if (alert_error == true)
                    {
                        string strError = "เกิดข้อผิดพลาดที่คำสั่ง : " + strSql.ToString();
                        if (!string.IsNullOrEmpty(alert_label_name))
                        {
                            Page page = (Page)HttpContext.Current.Handler;
                            Label lblAlert = (Label)page.FindControl(alert_label_name);
                            if (lblAlert != null)
                            {
                                lblAlert.Text = strError;
                            }
                        }
                        else
                        {
                            clsJS clsJS = new clsJS();
                            clsJS.Alert(strError);
                        }
                    }
                    strSql.Length = 0; strSql.Capacity = 0;
                    rtnValue = false;
                }
            }
        }
        else
        {
            if (alert_error == true)
            {
                string strError = "เกิดข้อผิดพลาด : Table(" + strTable + ") จำนวน Field(" + arrField.Length.ToString() + ") และ Data(" + arrData.Length.ToString() + ") ไม่เท่ากัน";
                if (!string.IsNullOrEmpty(alert_label_name))
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    Label lblAlert = (Label)page.FindControl(alert_label_name);
                    if (lblAlert != null)
                    {
                        lblAlert.Text = strError;
                    }
                }
                else
                {
                    clsJS clsJS = new clsJS();
                    clsJS.Alert(strError);
                }
            }
        }
        return rtnValue;
    }

    /// <summary>
    /// Update ข้อมูลในฐานข้อมูล โดยส่งลิสของฟิลด์ และ ลิสของข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strField">ลิสของชื่อฟิลด์</param>
    /// <param name="strData">ลิสของข้อมูล</param>
    /// <param name="strWhere">เงื่อนไข WHERE (hn='15-12-10000')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="alert_error">True=กรณีเกิด Error ให้แสดงคำเตือน , False=ไม่แสดง</param>
    /// <param name="alert_label_name">ชื่อ Label ที่ใช้แสดงคำเตือน Error ถ้าไม่ระบุ จะแสดงเป็น MessageBox</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Update("STAFF",new string[]{"st_id","usern"},new string[]{"'151246'","'Oofdui'"},"st_id='151246'",clsSQL.DBType.MySQL,"cs",true,"lblMessage");
    /// </example>
    public bool Update(string strTable, string[] strField, string[] strData, string strWhere, DBType dbType, string appsetting_name, bool alert_error, string alert_label_name)
    {
        StringBuilder strSql = new StringBuilder();
        bool rtnValue = false;
        int i;

        if (strField.Length == strData.Length)
        {
            strSql.Append("UPDATE ");
            strSql.Append(strTable);
            strSql.Append(" SET ");
            for (i = 0; i < strField.Length; i++)
            {
                strSql.Append(strField[i]);
                strSql.Append("=");
                strSql.Append(strData[i]);
                if (i < strField.Length - 1)
                {
                    strSql.Append(",");
                }
            }
            strSql.Append(" WHERE ");
            strSql.Append(strWhere);

            if (Execute(strSql.ToString(), dbType, appsetting_name))
            {
                rtnValue = true;
                strSql.Length = 0; strSql.Capacity = 0;
            }
            else
            {
                if (alert_error == true)
                {
                    string strError = "เกิดข้อผิดพลาดที่คำสั่ง : " + strSql.ToString();
                    if (!string.IsNullOrEmpty(alert_label_name))
                    {
                        Page page = (Page)HttpContext.Current.Handler;
                        Label lblAlert = (Label)page.FindControl(alert_label_name);
                        if (lblAlert != null)
                        {
                            lblAlert.Text = strError;
                        }
                        else
                        {
                            clsJS clsJS = new clsJS();
                            clsJS.Alert(strError);
                        }
                    }
                    else
                    {
                        clsJS clsJS = new clsJS();
                        clsJS.Alert(strError);
                    }
                }
                strSql.Length = 0; strSql.Capacity = 0;
                rtnValue = false;
            }
        }
        else
        {
            if (alert_error == true)
            {
                string strError = "เกิดข้อผิดพลาด : Table(" + strTable + ") จำนวน Field(" + strField.Length.ToString() + ") และ Data(" + strData.Length.ToString() + ") ไม่เท่ากัน";
                if (!string.IsNullOrEmpty(alert_label_name))
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    Label lblAlert = (Label)page.FindControl(alert_label_name);
                    if (lblAlert != null)
                    {
                        lblAlert.Text = strError;
                    }
                    else
                    {
                        clsJS clsJS = new clsJS();
                        clsJS.Alert(strError);
                    }
                }
                else
                {
                    clsJS clsJS = new clsJS();
                    clsJS.Alert(strError);
                }
            }
        }
        return rtnValue;
    }

    /// <summary>
    /// Update ข้อมูลในฐานข้อมูล โดยส่งลิสของฟิลด์และข้อมูล พร้อมระบุ SQL Parameter ได้
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="arrValue">ลิสของชื่อฟิลด์และข้อมูล</param>
    /// <param name="arrParameter">SQL Parameter</param>
    /// <param name="strWhere">เงื่อนไข WHERE (province_id=?ID)</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="outSQL">SQL Query ที่โปรแกรมสร้างขึ้น</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL clsSQL = new clsSQL();
    /// string outSQL;
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {"?ID","78"} },
    ///     "province_id=?ID",
    ///     clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// clsSQL.Update(
    ///     "PROVINCE",
    ///     new string[,] {{ "province_name", "'ทดสอบ 2'" }, { "province_sort", "1" } },
    ///     new string[,] { {} },
    ///     "province_id=78",clsSQL.DBType.MySQL,"cs",out outSQL
    /// );
    /// </example>
    public bool Update(string strTable, string[,] arrValue, string[,] arrParameter, string strWhere, DBType dbType, string appsetting_name, out string outSQL)
    {
        bool boolReturn = false;
        outSQL = "";
        StringBuilder strSQL = new StringBuilder();
        int i;

        if (arrValue.Rank == 2)
        {
            strSQL.Append("UPDATE ");
            strSQL.Append(strTable);
            strSQL.Append(" SET ");

            //########## Field=Value ##########
            for (i = 0; i < arrValue.Length / arrValue.Rank; i++)
            {
                strSQL.Append(arrValue[i, 0]);
                strSQL.Append("=");
                strSQL.Append(arrValue[i, 1]);
                if (i < (arrValue.Length / arrValue.Rank) - 1)
                {
                    strSQL.Append(",");
                }
            }

            strSQL.Append(" WHERE ");
            strSQL.Append(strWhere);
            strSQL.Append(";");

            outSQL = strSQL.ToString();
            boolReturn = Execute(strSQL.ToString(), arrParameter, dbType, appsetting_name);
        }

        return boolReturn;
    }

    /// <summary>
    /// Delete ข้อมูลในฐานข้อมูล
    /// </summary>
    /// <param name="strTable">ชื่อ Table</param>
    /// <param name="strWhere">เงื่อนไข WHERE (hn='Test')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <param name="alert_error">True=กรณีเกิด Error ให้แสดงคำเตือน , False=ไม่แสดง</param>
    /// <param name="alert_label_name">ชื่อ Label ที่ใช้แสดงคำเตือน Error ถ้าไม่ระบุ จะแสดงเป็น MessageBox</param>
    /// <returns>True=รันสำเร็จ , False=ไม่สำเร็จ</returns>
    /// <example>
    /// clsSQL.Delete("TableName", "hn='Test'", clsSQL.DBType.MySQL, "cs", true, "lblWarn");
    /// </example>
    public bool Delete(string strTable, string strWhere, DBType dbType, string appsetting_name, bool alert_error, string alert_label_name)
    {
        bool rtnValue = false;
        StringBuilder strSql = new StringBuilder();

        if (!string.IsNullOrEmpty(strTable) && !string.IsNullOrEmpty(appsetting_name))
        {
            strSql.Append("DELETE ");
            strSql.Append("FROM ");
            strSql.Append(strTable);
            strSql.Append(" WHERE ");
            strSql.Append(strWhere);

            if (Execute(strSql.ToString(), dbType, appsetting_name))
            {
                rtnValue = true;
                strSql.Length = 0; strSql.Capacity = 0;
            }
            else
            {
                if (alert_error == true)
                {
                    string strError = "เกิดข้อผิดพลาดที่คำสั่ง : " + strSql.ToString();
                    if (!string.IsNullOrEmpty(alert_label_name))
                    {
                        Page page = (Page)HttpContext.Current.Handler;
                        Label lblAlert = (Label)page.FindControl(alert_label_name);
                        if (lblAlert != null)
                        {
                            lblAlert.Text = strError;
                        }
                    }
                    else
                    {
                        clsJS clsJS = new clsJS();
                        clsJS.Alert(strError);
                    }
                }
                strSql.Length = 0; strSql.Capacity = 0;
                rtnValue = false;
            }
        }
        else
        {
            if (alert_error == true)
            {
                string strError = "เกิดข้อผิดพลาด : คุณระบุตัวแปรไม่ครบ";

                if (!string.IsNullOrEmpty(alert_label_name))
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    Label lblAlert = (Label)page.FindControl(alert_label_name);
                    if (lblAlert != null)
                    {
                        lblAlert.Text = strError;
                    }
                }
                else
                {
                    clsJS clsJS = new clsJS();
                    clsJS.Alert(strError);
                }
            }
        }

        return rtnValue;
    }

    /// <summary>
    /// สร้างประโยค WHERE จาก CheckBoxList
    /// </summary>
    /// <param name="cbControl">ชื่อคอนโทรล CheckBoxList</param>
    /// <param name="strField">ชื่อฟิลด์ที่ใช้สร้าง SQL Query</param>
    /// <param name="strJoin">ตัวเชื่อมประโยค SQL Query (OR,AND)</param>
    /// <param name="booQuote">True=ใช้สัญลักษณ์ ' ครอบข้อมูล , False=ไม่ใช้ ' ครอบ</param>
    /// <param name="booBracket">True=ใช้ () ครอบ SQL Query ที่ได้ , False=ไม่ใช้ () ครอบ</param>
    /// <returns>คืนค่า SQL Where เฉพาะส่วนที่ตรวจสอบกับ CheckBoxList</returns>
    /// <example>
    /// clsSQL.QueryBuilder_Where(cbGender,"sex","OR",true,true);
    /// </example>
    public string QueryBuilder_Where(CheckBoxList cbControl, string strField, string strJoin, bool booQuote, bool booBracket)
    {
        StringBuilder strSQL=new StringBuilder();
        string strReturn = "";
        int i;
        string strQuote = (booQuote == true ? "'" : "");
        strJoin = " " + strJoin + " ";

        strSQL.Length = 0; strSQL.Capacity = 0;
        for (i = 0; i <= cbControl.Items.Count - 1; i++)
        {
            if(cbControl.Items[i].Selected==true){
                if (cbControl.Items[i].Value == "all")
                {
                    strSQL.Length = 0;strSQL.Capacity = 0;
                    break;
                }
                strSQL.Append(strField + "=");
                strSQL.Append(strQuote);
                strSQL.Append(cbControl.Items[i].Value);
                strSQL.Append(strQuote);
                strSQL.Append(strJoin);
            }
        }
        if (!string.IsNullOrEmpty(strSQL.ToString()))
        {
            if (strSQL.ToString().Length > strJoin.Length)
            {
                strReturn = (booBracket == true ? "(" : "") +
                    strSQL.ToString().Substring(0, strSQL.ToString().Length - strJoin.Length) + 
                    (booBracket == true ? ")" : "");
            }
        }
        strSQL.Length = 0; strSQL.Capacity = 0; strSQL = null;
        return strReturn;
    }

    /// <summary>
    /// Execute คำสั่ง SQL กับไฟล์ Excel
    /// </summary>
    /// <param name="strSQL">SQL Query (Insert into [ACCOUNT$](id,usern,pwd,type)VALUES('999999','username','password','admin'))</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>True=รันสำเร็จ , False=รันไม่สำเร็จ</returns>
    /// <example>
    /// ExecuteExcel("Insert into [ACCOUNT$](id,usern,pwd,type)VALUES('999999','username','password','admin')", "Upload/ACCOUNT.xlsx");
    /// </example>
    public bool ExecuteExcel(string strSQL, string pathShortXLSX)
    {
        bool rtnValue = false;
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
                ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            OleDbConnection myConn = new OleDbConnection(cs);
            try
            {
                OleDbCommand myCmd = new OleDbCommand(strSQL, myConn);
                myConn.Open();
                myCmd.ExecuteNonQuery();
                myConn.Close();
                rtnValue = true;
            }
            catch (Exception ex)
            {

            }
        }

        return rtnValue;
    }

    /// <summary>
    /// Execute คำสั่ง SQL กับไฟล์ Excel โดยใช้วิธีคืนค่า
    /// </summary>
    /// <param name="strSQL">SQL Query (SELECT COUNT(id) FROM [ACCOUNT$])</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>คืนค่าที่ได้จาก SQL Query</returns>
    /// <example>
    /// ReturnExcel("SELECT COUNT(id) FROM [ACCOUNT$]","Upload/ACCOUNT.xlsx");
    /// </example>
    public string ReturnExcel(string strSQL, string pathShortXLSX)
    {
        string rtnValue = "";
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists)
        {
            string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
                ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            OleDbConnection myConn = new OleDbConnection(cs);
            try
            {
                OleDbCommand myCmd = new OleDbCommand(strSQL, myConn);
                myConn.Open();
                rtnValue = myCmd.ExecuteScalar().ToString();
                myConn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        return rtnValue;
    }

    /// <summary>
    /// สร้าง DataTable จากไฟล์ Excel ที่เราระบุ โดยสร้างจาก SQL Query ที่เราระบุเอง
    /// </summary>
    /// <param name="strSQL">SQL Query (SELECT * FROM [ACCOUNT$])</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>DataTable ที่ได้ตาม SQL Query</returns>
    /// <example>
    /// BindExcel("SELECT * FROM [ACCOUNT$]", "Upload/ACCOUNT.xlsx");
    /// </example>
    public DataTable BindExcel(string strSQL, string pathShortXLSX)
    {
        DataTable dt = new DataTable();
        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));

        if (objInfo.Exists == false)
        {
            clsJS clsJS = new clsJS();
            clsJS.Alert("ไม่พบไฟล์ที่คุณระบุ");
            return dt;
        }

        string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
            ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

        OleDbConnection myConn = new OleDbConnection(cs);

        try
        {
            OleDbDataAdapter myDa = new OleDbDataAdapter(strSQL, myConn);
            myDa.Fill(dt);
        }
        catch (Exception ex)
        {
            // เกิดข้อผิดพลาด เช่น ไม่มี Column ที่เลือก , ชื่อ Sheet ผิด , Statement ผิด
        }

        return dt;
    }

    /// <summary>
    /// ใช้สร้าง DataTable จากการ Query ข้อมูลในไฟล์ Excel โดยการระบุแยก เช่น ชื่อชีต ชื่อคอลัมที่ต้องการ และ เงื่อนไข
    /// </summary>
    /// <param name="strSheetName">ชื่อ Sheet ในไฟล์ Excel</param>
    /// <param name="strColumnName">ชื่อฟิลด์ที่ต้องการใน DataTable (id,usern,pwd)</param>
    /// <param name="strWhere">WHERE Query (usern like '%002%')</param>
    /// <param name="pathShortXLSX">ไฟล์พาร์ธ (Upload/ACCOUNT.xlsx)</param>
    /// <returns>DataTable ที่ได้ตาม SQL Query</returns>
    /// <example>
    /// BindExcel("ACCOUNT", "id,usern,pwd", "usern like '%002%'", "Upload/ACCOUNT.xlsx");
    /// </example>
    public DataTable BindExcel(string strSheetName, string strColumnName, string strWhere, string pathShortXLSX)
    {
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();

        FileInfo objInfo = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX));
        if (objInfo.Exists == false)
        {
            clsJS clsJS = new clsJS();
            clsJS.Alert("ไม่พบไฟล์ที่คุณระบุ");
            return dt;
        }

        string cs = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            System.Web.HttpContext.Current.Server.MapPath(pathShortXLSX) +
            ";Extended Properties='Excel 12.0 Xml;HDR=YES'";

        strSQL.Append("SELECT ");
        strSQL.Append((!string.IsNullOrEmpty(strColumnName) ? strColumnName : "*"));
        strSQL.Append(" FROM ");
        strSQL.Append((!string.IsNullOrEmpty(strSheetName) ? "[" + strSheetName + "$]" : "[Sheet1$]"));
        strSQL.Append((!string.IsNullOrEmpty(strWhere) ? " WHERE " + strWhere : ""));

        OleDbConnection myConn = new OleDbConnection(cs);

        try
        {
            OleDbDataAdapter myDa = new OleDbDataAdapter(strSQL.ToString(), myConn);
            myDa.Fill(dt);
        }
        catch (Exception ex)
        {
            // เกิดข้อผิดพลาด เช่น ไม่มี Column ที่เลือก , ชื่อ Sheet ผิด , Statement ผิด
        }

        return dt;
    }

    /// <summary>
    /// หา UID ใหม่ในฐานข้อมูล จากเงื่อนไขที่เรากำหนด
    /// </summary>
    /// <param name="id_column_name">ชื่อฟิลด์ที่ต้องการ (UID)</param>
    /// <param name="fromTable">ชื่อ Table</param>
    /// <param name="whereStr">เงื่อนไขพิเศษ อาจเว้นว่างไว้ก็ได้ (member_active='1')</param>
    /// <param name="strDBType">ชนิดของฐานข้อมูล เช่น sql,odbc,mysql</param>
    /// <param name="appsetting_name">ชื่อตัวแปรที่เก็บ ConnectionString ในไฟล์ AppSetting</param>
    /// <returns>คืนค่า UID ใหม่</returns>
    /// <example>
    /// clsSQL.GetNewID("member_id","MEMBER","member_active='1'",clsSQL.DBType.MySQL,"cs");
    /// </example>
    public int GetNewID(string id_column_name, string fromTable, string whereStr, DBType dbType, string appsetting_name)
    {
        StringBuilder strSQL = new StringBuilder();
        int id=0;
        string functionName;

        if (dbType==DBType.SQLServer)
        {
            functionName = "ISNULL";
        }
        else if (dbType==DBType.MySQL)
        {
            functionName = "IFNULL";
        }
        else
        {
            functionName = "IFNULL";
        }

        strSQL.Append("SELECT ");
        strSQL.Append(functionName + "(MAX(" + id_column_name + "),0)+1 ");
        strSQL.Append("FROM ");
        strSQL.Append(fromTable + " ");
        if (!string.IsNullOrEmpty(whereStr))
        {
            strSQL.Append("WHERE ");
            strSQL.Append(whereStr);
        }

        clsSQL clsSQL = new clsSQL();

        id = int.Parse(clsSQL.Return(strSQL.ToString(), dbType, appsetting_name));

        return id;
    }
	
    /// <summary>
    /// เช็คข้อมูลก่อนบันทึกลงฐานข้อมูล เป็นว่างจะคืนเป็น null หากไม่ว่าง สามารถระบุว่าจะให้ใส่ ' ด้วยหรือไม่
    /// </summary>
    /// <param name="Value">ค่าที่ต้องการให้ตรวจสอบ</param>
    /// <param name="IsVarchar">True=ใส่เครื่องหมาย ' ครอบข้อมูล กรณีข้อมูลไม่ว่าง , False=ไม่ใส่ ' กรณีข้อมูลไม่ว่าง</param>
    /// <returns>ข้อมูลหลังการตรวจสอบและปรับค่า</returns>
    /// <example>
    /// clsSQL.GetNull("",true);        return null
    /// clsSQL.GetNull("ทดสอบ",true);   return 'ทดสอบ'
    /// </example>
    public string GetNull(string Value, bool IsVarchar = true)
    {
        string rtnValue = "";

        if (!string.IsNullOrEmpty(Value))
        {
            if (IsVarchar)
            {
                rtnValue = "'" + Value + "'";
            }
            else
            {
                rtnValue = Value;
            }
        }
        else
        {
            rtnValue = "null";
        }

        return rtnValue;
    }

    /// <summary>
    /// กรองอักขระที่อาจก่อให้เกิดความผิดพลาด เมื่อใช้ร่วมกับ SQL Query
    /// </summary>
    /// <param name="strInput">คำที่ต้องการตรวจสอบ</param>
    /// <returns>คำที่ผ่านการตรวจอักขระพิเศษแล้ว</returns>
    /// <example>
    /// clsDefault.CodeFilter("สวัสดีครับ&nbsp;เราจะมาจัดการโค้ดต่างๆกันนะ เช่น 'โค้ดแบบนี้'");
    /// Output : สวัสดีครับ เราจะมาจัดการโค้ดต่างๆกันนะ เช่น ''โค้ดแบบนี้''
    /// </example>
    public string CodeFilter(string strInput)
    {
        string strOutput;

        strOutput = strInput.Trim();
        if (!string.IsNullOrEmpty(strOutput))
        {
            strOutput = strOutput.Replace("&nbsp;", " ");
            strOutput = strOutput.Replace("''", "'"); // กันไว้ กรณีที่เรา Replace เป็น '' มาแล้วรอบนึง
            strOutput = strOutput.Replace("'", "''");
        }

        return strOutput;
    }
}
