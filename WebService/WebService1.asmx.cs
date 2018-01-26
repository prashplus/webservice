using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;


namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        SqlConnection cn = new SqlConnection(@"Data Source=systems.database.windows.net;Initial Catalog=systems;Persist Security Info=True;User ID=prashant;Password=Techn0logy");
      //  SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-J2BMBQO\SQLEXPRESS;Initial Catalog=net;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int login(string uname, string pass)
        {
          //  SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-J2BMBQO\SQLEXPRESS;Initial Catalog=net;Integrated Security=True");
           // SqlCommand cmd = new SqlCommand();
            try
            {
                cn.Open();
                string ins = "select * from login where name=('" + uname + "') and pass=('" + pass + "')";
                cmd = new SqlCommand(ins, cn);
                String username = uname;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {

                    return 1;
                }
                else
                {
                    cn.Close();
                    return 0;
                }
            }
            catch (Exception e)
            {
                cn.Close();
                return 0;
            }
        }

        public class dataBucket
        {
            public String[] str = new string[11];
        }

        [WebMethod]
        public dataBucket getUserData(float cid)
        {
            //String[] str = new String[11];
            dataBucket d1 = new dataBucket();

            string ins = "select CID,CustomerName,Address,AreaName,CityName,StateName,Mobile,PackageName,BillingDate,IsActive from main1 where CID=(" + cid + ");";

            SqlCommand com = new SqlCommand(ins, cn);

            try
            {
                cn.Open();

                using (SqlDataReader read = com.ExecuteReader())
                {
                    while (read.Read())
                    {
                        d1.str[1] = (read["CID"].ToString());
                        d1.str[2] = (read["CustomerName"].ToString());
                        d1.str[3] = (read["Address"].ToString());
                        d1.str[4] = (read["AreaName"].ToString());
                        d1.str[5] = (read["CityName"].ToString());
                        d1.str[6] = (read["StateName"].ToString());
                        d1.str[7] = (read["Mobile"].ToString());
                        d1.str[8] = (read["PackageName"].ToString());
                        d1.str[9] = (read["BillingDate"].ToString());
                        d1.str[10] = (read["IsActive"].ToString());
                    }
                }
                cn.Close();
                return d1;
            }
            finally
            {
                cn.Close();
            }
        }
        [WebMethod]
        public int addCustomer(float CID, string CustomerName, string Address, string AreaName, string CityName,
            string StateName, string CountryName, float Mobile, string Email, string PackageName, string CustomerType,
            string ConnectionType, string SrNo, string Charges)
        {
            try
            {
                float x = 0;
                cn.Open();

                string ins = "insert into main1 (CID,CustomerName,Address,AreaName,CityName,StateName,CountryName,Mobile,Email,PackageName,CustomerType,ConnectionType,SrNo,Charges) values (" + CID + ",'" + CustomerName + "','" + Address + "','" + AreaName + "','" + CityName + "','" + StateName + "','" + CountryName + "','" + Mobile + "','" + Email + "','" + PackageName + "','" + CustomerName + "','" + ConnectionType + "','" + SrNo + "','" + Charges + "');";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        [WebMethod]
        public int rmCustomer(float CID)
        {
            try
            {
                cn.Open();
                String ins = "delete from main1 where CID=(" + CID + ")";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
        [WebMethod]
        public int extend(float CID, int mon)
        {
            try
            {
                cn.Open();
                DateTime date1 = new DateTime();
                string ins = "select * from main1 where CID=('" + CID + "')";
                cmd = new SqlCommand(ins, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                int cnt = 0;
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        date1 = Convert.ToDateTime(reader["BillingDate"].ToString());
                        date1 = date1.AddMonths(mon);
                        cnt++;
                    }
                }
                else
                {
                    Console.WriteLine("No record found.");
                    return 0;
                }
                reader.Close();
                if (cnt > 0)
                {
                    string ins2 = "UPDATE main1 SET BillingDate = '" + date1.ToString() + "' where CID=" + CID + "";

   
                    cmd = new SqlCommand(ins2, cn);
                    int a = cmd.ExecuteNonQuery();
                    return 1;

                }
                return 0;

            }
            catch (Exception e1)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }

        [WebMethod]

        public int request(string name, string add)
        {
            try
            {
                cn.Open();
                string ins = "insert into request (name,address) values ('" + name + "','" + add + "');";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        [WebMethod]

        public int complaint(float cid, string description)
        {
            try
            {
                float x = 0;
                cn.Open();

                string ins = "insert into complaint (cid,description) values (" + cid + ",'" + description + "');";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

        [WebMethod]
        public int rmrequest(int id)
        {
            try
            {
                cn.Open();
                String ins = "delete from request where id=(" + id + ")";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
        [WebMethod]
        public int rmcomplaint(int id)
        {
            try
            {
                cn.Open();
                String ins = "delete from complaint where id=(" + id + ")";
                cmd = new SqlCommand(ins, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
    } 
}
