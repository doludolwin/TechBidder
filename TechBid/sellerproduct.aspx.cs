using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class sellerproduct : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> products = new List<ProductDetails>();
		public String name;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["id"] == null || Session["role"].ToString() != "2")
			{
				Response.Redirect("index.aspx");
			}

			sellerproductdetails();
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
		public void sellerproductdetails()
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
					product.Description = null;
					product.end_time = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
					string[] starttokens = product.end_time.Split('-');
					product.end_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];
					DateTime date = Convert.ToDateTime(product.end_time);

					query = "select count(*) from selled_products where product_id='" + reader["Id"] + "'";
					select = new SqlCommand(query, con);
					product.Credits = int.Parse(select.ExecuteScalar().ToString());
					if (product.Credits > 0)
					{
						product.Description = "Waiting for Payment";
					}

					query = "select * from payment where product_id ='" + reader["Id"] + "';";
					select = new SqlCommand(query, con);
					SqlDataReader payreader = select.ExecuteReader();
					if (payreader.HasRows)
					{
						while (payreader.Read())
						{
							product.winprice = payreader["amount"].ToString();
							product.Start_time = payreader["paymentdate"].ToString();
							query = "select name from buyer_details where user_id ='" + payreader["user_id"] + "';";
							select = new SqlCommand(query, con);
							product.winuser = select.ExecuteScalar().ToString();
							product.Description = "Payment Sucess";
						}

					}
					payreader.Close();
					
					query ="select count(*) from bid_on_product where product_id='"+ reader["Id"] + "'";
					select = new SqlCommand(query, con);
					product.Credits = int.Parse(select.ExecuteScalar().ToString());
					if(product.Credits == 0)
					{
						product.Description = "Waiting For Bids";
					}
					if(product.Description == null)
					{
						query = "select MAX(total_price_increase) from bid_on_product where product_id='" + reader["Id"] + "';";
						select = new SqlCommand(query, con);
						product.PriceIncrease = int.Parse(select.ExecuteScalar().ToString());
						product.Description = "Biding On Progress";
					}
					if (date >= DateTime.Now)
					{
						product.Description = "Upcoming Auction";
					}

					


					products.Add(product);
				}
			}

		} 
	}
}