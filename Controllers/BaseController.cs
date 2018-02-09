using GestaoPatrimonio.Models;
using Mvc.Mailer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestaoPatrimonio.Controllers
{
    public class BaseController : Controller
    {
        public GestaoPatrimonioEntities db = new GestaoPatrimonioEntities();
        public SqlConnection oSqlConnection;
        public Thread myThread;

        public int ExecutaSQLNonQuery(string sql)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sql;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = this.PegaConexao();
            sqlCommand.CommandTimeout = 300000;
            return sqlCommand.ExecuteNonQuery();
        }

        public DataTable ExecutaSQLAxys(string sql)
        {
            DbDataReader reader = (DbDataReader)null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection("");
                sqlCommand.CommandTimeout = 30000;
                reader = (DbDataReader)sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
            }
            DataTable dataTable = this.ObterTabela(reader);
            this.oSqlConnection.Close();
            return dataTable;
        }

        public DataTable ExecutaSQL(string sql)
        {
            DbDataReader reader = (DbDataReader)null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = this.PegaConexao();
                sqlCommand.CommandTimeout = 30000;
                reader = (DbDataReader)sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
            }
            DataTable dataTable = this.ObterTabela(reader);
            this.oSqlConnection.Close();
            return dataTable;
        }

        private SqlConnection PegaConexao()
        {
            try
            {
                this.oSqlConnection = new SqlConnection("");
                this.oSqlConnection.Open();
            }
            catch (Exception ex)
            {
                this.oSqlConnection = this.PegaConexao();
            }
            return this.oSqlConnection;
        }

        public bool CheckLogin(string email, string pass, string hash)
        {
            bool bRet = false;
            User user = null;
            //
            Utils.Security.Encryption cryp = new Utils.Security.Encryption();
            pass = cryp.Encrypt(pass);
            //
            if (!string.IsNullOrEmpty(hash))
            {
                user = db.User.Where(a => a.Email.Equals(email) && a.Pass.Equals(pass) && a.Count == 0 && a.Ative == 0 && a.Hash.Equals(hash)).FirstOrDefault();
                //
                if (user != null)
                {
                    user.Count = 1;
                    user.Ative = 1;
                    user.Last = DateTime.Now;
                    //
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    //
                    bRet = true;
                }
            }
            else
            {
                user = db.User.Where(a => a.Email.Equals(email) && a.Pass.Equals(pass) && a.Ative == 1).FirstOrDefault();
                //
                if (user != null)
                {
                    ++user.Count;
                    user.Ative = 1;
                    user.Last = DateTime.Now;
                    //
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    //
                    bRet = true;
                }
            }
            //
            if (bRet)
            {
                //Session["UserLogado"] = user.ID;
                FormsAuthentication.SetAuthCookie(email, false);
            }
            return bRet;
        }

        public bool UserRegister(string email, string pass, out string hash)
        {
            bool bRet = false;
            Utils.Security.Encryption cryp = new Utils.Security.Encryption();
            pass = cryp.Encrypt(pass);
            //
            User user = new User();
            user.Email = email;
            user.Pass = pass;
            user.Count = 0;
            user.Create = DateTime.Now;
            user.Last = DateTime.Now;
            user.Hash = user.GetHashCode().ToString();
            user.Ative = 0;
            hash = user.Hash;
            //
            db.User.Add(user);
            db.SaveChanges();
            bRet = true;
            //
            return bRet;
        }

        private DataTable ObterTabela(DbDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();
            DataTable dataTable = new DataTable();
            foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
            {
                if (!dataTable.Columns.Contains(row["ColumnName"].ToString()))
                {
                    DataColumn column = new DataColumn()
                    {
                        ColumnName = row["ColumnName"].ToString(),
                        Unique = Convert.ToBoolean(row["IsUnique"]),
                        AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]),
                        ReadOnly = Convert.ToBoolean(row["IsReadOnly"])
                    };
                    dataTable.Columns.Add(column);
                }
            }
            while (reader.Read())
            {
                DataRow row = dataTable.NewRow();
                for (int ordinal = 0; ordinal < dataTable.Columns.Count; ++ordinal)
                    row[ordinal] = reader.GetValue(ordinal);
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public string SendEmail_Register(string email, string hash)
        {
            MvcMailMessage mvcMailMessage1 = new MvcMailMessage();
            SmtpClientWrapper wrapper = new SmtpClientWrapper();
            //
            string link = string.Empty;
            if (((HttpRequestWrapper)((HttpContextWrapper)HttpContext).Request).Url.Host.ToLower().Equals("localhost"))
            {
                link = "<a href='http://localhost/where_gestao_patrimonio/Account/Login?hash=" + hash + "'>Link to complete the registration.</a>";
            }
            else
            {
                link = "<a href='http://www.neukirchen.com.br/Account/Login?hash=" + hash + "'>Link to complete the registration.</a>";
            }
            //
            mvcMailMessage1.From = new MailAddress("contact@neukirchen.com.br");
            mvcMailMessage1.Subject = "No Replly";
            mvcMailMessage1.To.Add(new MailAddress(email));
            mvcMailMessage1.Body = "To complete registration click on the link below: <br />" + link;
            mvcMailMessage1.IsBodyHtml = true;
            //
            var SmtpServer = "";
            int SmtpPort = 0;
            var SmtpPass = "";
            var SmtpUser = "";
            //
            var client = new SmtpClient(SmtpServer, SmtpPort);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(SmtpUser, SmtpPass);
            //
            wrapper = new SmtpClientWrapper(client);
            try
            {
                mvcMailMessage1.Send(wrapper);
            }
            catch (Exception ex2)
            {
                return ex2.Message;
            }
            //
            return "ok";
        }


        public JsonResult PegaStatusSensor(string idsensor)
        {
            string str = "0";
            try
            {
                DataTable dataTable = ExecutaSQL("SELECT Status FROM [DB_9F9B8A_neuk].[dbo].[Sensor] WHERE ID = " + idsensor);
                if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                    str = !dataTable.Rows[0]["Status"].ToString().ToLower().Equals("false") ? "on" : "off";
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = "erro",
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = str,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLatLng(string id)
        {
            string str1 = "";
            string str2 = "";
            try
            {
                DataTable dataTable = ExecutaSQL("SELECT TOP 1 [ID], Lat, Lng FROM LogAccessPosiotion WHERE Sensor_ID = " + id + " ORDER BY ID DESC");
                if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                {
                    str1 = dataTable.Rows[0]["Lat"].ToString();
                    str2 = dataTable.Rows[0]["Lng"].ToString();
                }
            }
            catch (Exception ex)
            {
                return this.Json((object)new
                {
                    data = "erro",
                    success = true,
                    errors = string.Empty
                }, JsonRequestBehavior.AllowGet);
            }
            return this.Json((object)new
            {
                data = "Ok",
                Lat = str1,
                Lng = str2,
                success = true,
                errors = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

namespace Utils.Security
{
    public class Encryption
    {
        private const string CryptoKey = "2014073012345678";
        private static Encryption _instance;
        private RijndaelManaged _rijndael;

        public static Encryption Instance
        {
            get
            {
                return Encryption._instance ?? (Encryption._instance = new Encryption());
            }
        }

        public string Decrypt(string encryptedText)
        {
            return Encoding.UTF8.GetString(this.Decrypt(Convert.FromBase64String(encryptedText), this.GetEncryptor()));
        }

        public string Encrypt(string plainText)
        {
            return Convert.ToBase64String(this.Encrypt(Encoding.UTF8.GetBytes(plainText), this.GetEncryptor()));
        }

        public RijndaelManaged GetEncryptor()
        {
            if (this._rijndael == null)
            {
                byte[] numArray = new byte[16];
                byte[] bytes = Encoding.UTF8.GetBytes("2014073012345678");
                Array.Copy((Array)bytes, (Array)numArray, Math.Min(numArray.Length, bytes.Length));
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Mode = CipherMode.CBC;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.KeySize = 128;
                rijndaelManaged.BlockSize = 128;
                rijndaelManaged.Key = numArray;
                rijndaelManaged.IV = numArray;
                this._rijndael = rijndaelManaged;
            }
            return this._rijndael;
        }

        private byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        private byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
    }
}