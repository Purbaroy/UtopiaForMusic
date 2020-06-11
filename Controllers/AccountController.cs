using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMT563_Utopia.Models;
using System.Data.SqlClient;

namespace IMT563_Utopia.Controllers
{
    public class AccountController : Controller
    {

        //SqlCommand comm = new SqlCommand();
        //SqlDataReader dr;
        // GET: Account
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-U8FGPHP;database=ProjectX;Integrated Security=True;");

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //void connectionString()
        //{
        //    con.ConnectionString = "data source=DESKTOP-U8FGPHP;database=ProjectX;Integrated Security=True;";
        //}
        //[HttpPost]
        public ActionResult Verify(Account acc)
        {
           
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                cmd = new SqlCommand("SP_VERIFY_USER", connection);
                cmd.Parameters.Add(new SqlParameter("@Name", acc.Name));
                cmd.Parameters.Add(new SqlParameter("@Pass", acc.Password));
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            if (da == null)
            {
                return View("Error");
            }
            else
            {
                // return View("Home");

                return RedirectToAction("Home");
            }
        }

        public ActionResult Form()
        {
            return View();
        }
        public ActionResult CreateUser(AccountCreate acc)
        {
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd = new SqlCommand("SP_CREATE_FORM", connection);
            cmd.Parameters.Add(new SqlParameter("@FName", acc.FName));
            cmd.Parameters.Add(new SqlParameter("@LName", acc.LName));
            cmd.Parameters.Add(new SqlParameter("@email", acc.Email));
            cmd.Parameters.Add(new SqlParameter("@Gender", acc.gender));
            cmd.Parameters.Add(new SqlParameter("@City", acc.City));
            cmd.Parameters.Add(new SqlParameter("@Country", acc.Country));
            cmd.Parameters.Add(new SqlParameter("@Password", acc.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            {

                cmd.ExecuteNonQuery();
            }
            //return RedirectToAction("Home");
            return View("Login");
        }

        public ActionResult Home()
        {
            var model = new List<Home>();

            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                cmd = new SqlCommand("fetch_artist", connection);
                
                SqlDataReader rdr = cmd.ExecuteReader();
               
                while (rdr.Read())
                {

                    var home = new Home();
                    home.SrNo = Convert.ToString(rdr["ID"]);
                    home.ArtistName = Convert.ToString(rdr["ARTISTNAME"]);
                    home.Song = Convert.ToString(rdr["SONG"]);
                    home.Country = Convert.ToString(rdr["COUNTRY"]);
                    model.Add(home);
                    
                }
                rdr.Close();
                cmd2 = new SqlCommand("fetch_country", connection);
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {

                    var home2 = new Home();
                    home2.SearchCountry = Convert.ToString(rdr2["country_name"]);
                   
                    model.Add(home2);
                    
                }
                rdr2.Close();
            }
            catch(Exception ex)
            { throw ex; }

            return View(model);

        }
        [HttpPost]
        public ActionResult HomeWithSearch(string country)
        {
            var model = new List<HomeWithSearch>();

            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                
                cmd = new SqlCommand("fetch_artist_by_country", connection);
                cmd.Parameters.Add(new SqlParameter("@country", country));
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {

                    var home = new HomeWithSearch();
                    home.SrNo = Convert.ToString(reader["ID"]);
                    home.ArtistName = Convert.ToString(reader["ARTISTNAME"]);
                    home.Song = Convert.ToString(reader["SONG"]);
                    home.Country = Convert.ToString(reader["COUNTRY"]);
                    model.Add(home);

                }
                reader.Close();
                cmd2 = new SqlCommand("fetch_country", connection);
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    var home2 = new HomeWithSearch();
                    // var home2 = new Home();
                    home2.SearchCountry = Convert.ToString(rdr2["country_name"]);

                    model.Add(home2);

                }
                rdr2.Close();
            }
            catch (Exception ex)
            { throw ex; }

            return View(model);

        }
    }
}