using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class seller : System.Web.UI.Page
	{
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}

		public void sellerSignup(object sender, EventArgs e)
		{
			int cmd = 0;
			String id = null;
			String name = Request.Form["name"];
			String username = Request.Form["username"];
			String address = Request.Form["address"];
			String email = Request.Form["email"];
			String mobile = Request.Form["mobilenumber"];
			String password = Request.Form["password"];
			connection();

			String query = "insert into users values('" + email + "','" + username + "','" + password + "',2);";

			SqlCommand command = new SqlCommand(query, con);
			try
			{
				cmd = command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				con.Close();
				Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "FailureSignUpAlert();", true);
			}

			query = "select * from users where email='" + email + "' AND user_name='" + username + "';";
			SqlCommand selectusers = new SqlCommand(query, con);

			SqlDataReader reader = selectusers.ExecuteReader();
			while (reader.Read())
			{
				id = reader["Id"].ToString();
			}
			reader.Close();
			query = "insert into seller_details (name,address,phone_no,user_id) values('" + name + "','" + address + "','" + mobile + "','" + int.Parse(id) + "');";
			SqlCommand insertseller = new SqlCommand(query, con);
			cmd = insertseller.ExecuteNonQuery();
			con.Close();
			Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "successSignUpAlert();", true);
		}
	}
}