using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://140.127.22.4/CowRecord/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    string strdbcon = "server=140.127.22.4;database=CowRecord;uid=b10056;pwd=b10056";
    SqlConnection objcon;
    SqlCommand sqlcmd;
    string sql;

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string InsertCow(string date,string sex,string weight)
    {
        string re = "false";
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "INSERT INTO Cow(BornDate,Sex,Weight) VALUES('" + date + "','" + sex + "','" + weight + "')";
            SqlCommand insertcmd = new SqlCommand(sql, objcon);
            insertcmd.ExecuteNonQuery();
            objcon.Close();

            objcon.Open();
            sql = "select ID from Cow";
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re = dr[0].ToString();
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
        return re;
    }

    [WebMethod]
    public string InsertRecord(string CowID, string Date, string Orther)
    {
        string re = "false";
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "INSERT INTO Record(CowID,Date,Orther) VALUES('" + CowID + "','" + Date + "','" + Orther + "')";
            SqlCommand insertcmd = new SqlCommand(sql, objcon);
            insertcmd.ExecuteNonQuery();
            objcon.Close();
            re = "true";
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
        return re;
    }

    [WebMethod]
    public string ScanCow(string CowID)
    {
        string re = null;
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "select * from Cow where ID =" + "'" + CowID + "'";
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re += dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString();
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        return re;
    }

    [WebMethod]
    public string List(string CowID)
    {
        string re = "No Data";
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "select * from Record where CowID ='" + CowID + "'";
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re += dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + ";";
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        if (re == "" || re == "No Data")
        {
            return "No Data";
        }
        else
        {
            return re;
        }

        
    }
}
