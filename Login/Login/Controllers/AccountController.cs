using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using System.Data.SqlClient;
namespace Login.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-PM6C0F8; database = WPF; integrated security = SSPI; ";
        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from tbl_login where username ='" + acc.Name + "' and password='" + acc.Password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View("Error");
            }
            else
            {
                con.Close();
                return View("Registration");
            }
        }
        // GET: Register
        public ActionResult Registration()
        {
            return View("");
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from tbl_login where username ='" + acc.Name + "'and password='" + acc.Password + "'and email='" + acc.Email + "'";
            dr = com.ExecuteReader();
            if (ModelState.IsValid)
            {
                var check = dbo.tbl_login.FirstOrDefault(s => s.Email == _username.email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return View("Error");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View("Login");
                }

            }
        }
    }
}