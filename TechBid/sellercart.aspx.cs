using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class sellercart : System.Web.UI.Page
	{
		SqlConnection con;
		public String name;
		public List<ProductDetails> carts = new List<ProductDetails>();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["id"] == null || Session["role"].ToString() != "2")
			{
				Response.Redirect("index.aspx");
			}
			sellercarts();
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
		public void sellercarts()
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
					product.Credits = int.Parse(reader["Id"].ToString());
					
					query = "select * from payment where product_id='" + reader["Id"].ToString() + "';";
					select = new SqlCommand(query, con);
					SqlDataReader payreader = select.ExecuteReader();
					if (payreader.HasRows)
					{
						continue;
					}
					payreader.Close();

					query = "select * from selled_products where product_id='" + reader["Id"].ToString() + "';";
					select = new SqlCommand(query, con);
					SqlDataReader orderreader = select.ExecuteReader();
					if (orderreader.HasRows)
					{
						while (orderreader.Read())
						{
							string end_time = reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");
							string[] tokens = end_time.Split('-');
							end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
							DateTime enddate = Convert.ToDateTime(end_time).AddDays(2);
							if (enddate < DateTime.Now)
							{
								product.Time_intervel = "true";
							}


							product.id = int.Parse(orderreader["Id"].ToString());
							product.Price = orderreader["final_price"].ToString();
							product.Start_time = reader["bid_end_time"].ToString();
							query = "select * from buyer_details where user_id='" + orderreader["buyer_id"].ToString() + "';";
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
					carts.Add(product);
				}

			}

		}
	}
}