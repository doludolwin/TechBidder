using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class orderhistory : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> orders = new List<ProductDetails>();
		public String credits;
		public String name;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["id"] == null)
			{
				Response.Redirect("index.aspx");
			}
			findauthname();
			findcredits();
			userorderhistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}
		public void findauthname()
		{
			connection();
			string query = "select * from buyer_details where user_id='" + Session["id"].ToString() + "';";
			SqlCommand sqlCommand = new SqlCommand(query, con);
			SqlDataReader reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				name = reader["name"].ToString();
			}
			con.Close();
		}
		public void findcredits()
		{
			connection();
			string query = "select * from credit_details where user_id='" + Session["id"].ToString() + "';";
			SqlCommand sqlCommand = new SqlCommand(query, con);
			SqlDataReader reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				credits = reader["credits"].ToString();
			}
			con.Close();
		}
		public void userorderhistory()
		{
			string query = "select product_details.name,product_details.photo,payment.amount,payment.paymentdate from payment inner join product_details on payment.product_id=product_details.id where payment.product_id > 0 and payment.user_id='" + Session["id"] + "';";
			connection();
			SqlCommand command = new SqlCommand(query, con);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					var order = new ProductDetails();
					order.Name = reader["name"].ToString();
					order.Photo = reader["photo"].ToString();
					order.Credits = int.Parse(reader["amount"].ToString());
					order.Start_time = reader["paymentdate"].ToString();
					orders.Add(order);
				}
			}
			con.Close();
		}
	}
}