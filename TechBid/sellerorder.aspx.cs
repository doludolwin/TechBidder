using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class sellerorder : System.Web.UI.Page
	{
		SqlConnection con;
		public String name;
		public List<ProductDetails> orders = new List<ProductDetails>();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["id"] == null || Session["role"].ToString() != "2")
			{
				Response.Redirect("index.aspx");
			}
			sellerorderhistory();
			findauthname();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}
		public void findauthname()
		{
			connection();
			string query = "select * from seller_details where user_id='" + Session["id"].ToString() + "';";
			SqlCommand sqlCommand = new SqlCommand(query, con);
			SqlDataReader reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				name = reader["name"].ToString();
			}
			con.Close();
		}
		public void sellerorderhistory()
		{
			connection();
			string query = "select * from product_details where user_id='" + Session["id"].ToString() + "';";

			SqlCommand select = new SqlCommand(query, con);
			SqlDataReader reader = select.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					var product = new ProductDetails();
					product.Name = reader["name"].ToString();
					product.Photo = reader["photo"].ToString();
					query = "select * from payment where product_id='" + reader["Id"].ToString() + "';";
					select = new SqlCommand(query, con);
					SqlDataReader payreader = select.ExecuteReader();
					if (payreader.HasRows)
					{
						while (payreader.Read())
						{
							product.Price = payreader["amount"].ToString();
							product.Start_time = payreader["paymentdate"].ToString();
							query = "select * from buyer_details where user_id='" + payreader["user_id"].ToString() + "';";
							select = new SqlCommand(query, con);
							SqlDataReader userreader = select.ExecuteReader();
							if (userreader.HasRows)
							{
								while (userreader.Read())
								{
									product.winuser = userreader["name"].ToString();
									product.Description = userreader["address"].ToString() + ", " + userreader["pincode"].ToString();
									product.winprice = userreader["phone_no"].ToString();
								}
							}
						}
						payreader.Close();
					}
					else
					{
						continue;
					}
					orders.Add(product);
				}

			}

		}
	}
	
}