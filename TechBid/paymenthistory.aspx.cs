using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class paymenthistory : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> payments = new List<ProductDetails>();
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
			userpaymenthistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
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
		public void userpaymenthistory()
		{
			string query = "select * from payment where user_id='"+Session["id"]+"'";
			connection();
			SqlCommand command = new SqlCommand(query, con);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					var payment = new ProductDetails();
					payment.Name = reader["cardholder"].ToString();
					payment.Description = reader["cardnumber"].ToString();
					payment.Price = reader["amount"].ToString();
					payment.Start_time = reader["paymentdate"].ToString();
					int productid = int.Parse(reader["product_id"].ToString());
					if(productid == 0)
					{
						payment.winuser = "Credits Purchase";
					}
					else
					{
						payment.winuser = "Product Purchase";
					}

					if(productid > 0)
					{
						query = "select * from product_details where Id='" + productid + "';";
						command = new SqlCommand(query, con);
						SqlDataReader productreader = command.ExecuteReader();
						if (productreader.HasRows)
						{
							while (productreader.Read())
							{
								payment.Photo = productreader["photo"].ToString();
								payment.winprice = productreader["name"].ToString();
							}
						}
						productreader.Close();
					}
					payments.Add(payment);
				}
			}
			con.Close();
		}
	}
}