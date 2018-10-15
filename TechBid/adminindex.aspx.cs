using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class adminindex : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> products = new List<ProductDetails>();
		public List<ProductDetails> liveproducts = new List<ProductDetails>();
		public List<ProductDetails> closedproducts = new List<ProductDetails>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 3)
			{
				Response.Redirect("index.aspx");
			}
			liveAuctionDetails();
			UpcomingDetails();
			ClosedProductDetails();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}
		public void liveAuctionDetails()
		{
			connection();
			String query = "select * from product_details;";
			SqlCommand selectusers = new SqlCommand(query, con);
			SqlDataReader reader = selectusers.ExecuteReader();
			while (reader.Read())
			{
				var product = new ProductDetails();
				product.id = int.Parse(reader["Id"].ToString());
				product.Name = reader["name"].ToString();
				product.Description = reader["description"].ToString();
				product.Price = reader["price"].ToString();
				product.Start_time = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
				product.Photo = reader["photo"].ToString();
				product.Time_intervel = reader["bid_time_intervel"].ToString();
				product.end_time = reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");

				string[] starttokens = product.Start_time.Split('-');
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];
				DateTime startdate = Convert.ToDateTime(product.Start_time);
				product.Start_time = startdate.ToString();

				string[] tokens = product.end_time.Split('-');
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				DateTime enddate = Convert.ToDateTime(product.end_time);
				product.end_time = enddate.ToString();
				if (startdate >= DateTime.Now || enddate <= DateTime.Now)
				{
					continue;
				}
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];


				if (int.Parse(product.Price) < 1000)
				{
					product.Credits = 1;
				}
				else if (int.Parse(product.Price) < 5000)
				{
					product.Credits = 2;
				}
				else if (int.Parse(product.Price) < 10000)
				{
					product.Credits = 3;
				}
				else if (int.Parse(product.Price) < 25000)
				{
					product.Credits = 5;
				}
				else if (int.Parse(product.Price) < 75000)
				{
					product.Credits = 8;
				}
				else
				{
					product.Credits = 10;
				}


				if (int.Parse(product.Price) < 1000)
				{
					product.PriceIncrease = 10;
				}
				else if (int.Parse(product.Price) < 5000)
				{
					product.PriceIncrease = 25;
				}
				else if (int.Parse(product.Price) < 10000)
				{
					product.PriceIncrease = 50;
				}
				else if (int.Parse(product.Price) < 25000)
				{
					product.PriceIncrease = 75;
				}
				else if (int.Parse(product.Price) < 75000)
				{
					product.PriceIncrease = 85;
				}
				else
				{
					product.PriceIncrease = 100;
				}
				query = "select buyer_details.name,bid_on_product.total_price_increase from bid_on_product inner join buyer_details on bid_on_product.user_id=buyer_details.user_id where bid_on_product.bid_no=(select MAX(bid_no) from bid_on_product) and product_id='" + product.id + "'; ";
				selectusers = new SqlCommand(query, con);
				SqlDataReader userreader = selectusers.ExecuteReader();
				if (userreader.HasRows)
				{
					while (userreader.Read())
					{
						product.winuser = userreader["name"].ToString();
						product.winprice = (int.Parse(userreader["total_price_increase"].ToString()) + int.Parse(product.Price)).ToString();
					}
				}
				else
				{
					product.winuser = "Waiting For Bids";
					product.winprice = product.Price;
				}
				userreader.Close();
				query = "select count(*) from bid_on_product where product_id='" + product.id + "';";
				selectusers = new SqlCommand(query, con);
				product.Credits = int.Parse(selectusers.ExecuteScalar().ToString());
				liveproducts.Add(product);
			}
			con.Close();
		}
		public void UpcomingDetails()
		{

			connection();
			String query = "select * from product_details;";
			SqlCommand selectusers = new SqlCommand(query, con);
			SqlDataReader reader = selectusers.ExecuteReader();
			while (reader.Read())
			{
				var product = new ProductDetails();
				product.id = int.Parse(reader["Id"].ToString());
				product.Name = reader["name"].ToString();
				product.Description = reader["description"].ToString();
				product.Price = reader["price"].ToString();
				product.Start_time = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
				product.Photo = reader["photo"].ToString();
				product.Time_intervel = reader["bid_time_intervel"].ToString();
				product.end_time = reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");


				string[] starttokens = product.Start_time.Split('-');
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];
				DateTime startdate = Convert.ToDateTime(product.Start_time);
				product.Start_time = startdate.ToString();


				string[] tokens = product.end_time.Split('-');
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				DateTime date = Convert.ToDateTime(product.end_time);
				product.end_time = date.ToString();
				if (startdate <= DateTime.Now || date <= DateTime.Now)
				{
					continue;
				}
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];


				if (int.Parse(product.Price) < 1000)
				{
					product.Credits = 1;
				}
				else if (int.Parse(product.Price) < 5000)
				{
					product.Credits = 2;
				}
				else if (int.Parse(product.Price) < 10000)
				{
					product.Credits = 3;
				}
				else if (int.Parse(product.Price) < 25000)
				{
					product.Credits = 5;
				}
				else if (int.Parse(product.Price) < 75000)
				{
					product.Credits = 8;
				}
				else
				{
					product.Credits = 10;
				}


				if (int.Parse(product.Price) < 1000)
				{
					product.PriceIncrease = 10;
				}
				else if (int.Parse(product.Price) < 5000)
				{
					product.PriceIncrease = 25;
				}
				else if (int.Parse(product.Price) < 10000)
				{
					product.PriceIncrease = 50;
				}
				else if (int.Parse(product.Price) < 25000)
				{
					product.PriceIncrease = 75;
				}
				else if (int.Parse(product.Price) < 75000)
				{
					product.PriceIncrease = 85;
				}
				else
				{
					product.PriceIncrease = 100;
				}

				products.Add(product);
			}
			con.Close();
		}

		public void ClosedProductDetails()
		{
			connection();
			String query = "select * from product_details;";
			SqlCommand selectusers = new SqlCommand(query, con);
			SqlDataReader reader = selectusers.ExecuteReader();
			while (reader.Read())
			{
				var product = new ProductDetails();
				product.id = int.Parse(reader["Id"].ToString());
				product.Name = reader["name"].ToString();
				product.Description = reader["description"].ToString();
				product.Price = reader["price"].ToString();
				product.Start_time = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
				product.Photo = reader["photo"].ToString();
				product.Time_intervel = reader["bid_time_intervel"].ToString();
				product.end_time = reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");

				string[] starttokens = product.Start_time.Split('-');
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];
				DateTime startdate = Convert.ToDateTime(product.Start_time);
				product.Start_time = startdate.ToString();

				string[] tokens = product.end_time.Split('-');
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				DateTime enddate = Convert.ToDateTime(product.end_time);
				product.end_time = enddate.ToString();
				if (startdate >= DateTime.Now || enddate >= DateTime.Now)
				{
					continue;
				}
				product.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
				product.Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];

				query = "select * from selled_products where product_id='" + product.id + "';";
				SqlCommand selectselled = new SqlCommand(query, con);
				SqlDataReader productread = selectselled.ExecuteReader();

				if (productread.HasRows)
				{
					int user_id = 0;
					while (productread.Read())
					{
						user_id = int.Parse(productread["buyer_id"].ToString());
						product.winprice = productread["final_price"].ToString();
					}
					productread.Close();
					query = "select name from buyer_details where user_id='" + user_id + "';";
					selectselled = new SqlCommand(query, con);
					product.winuser = selectselled.ExecuteScalar().ToString();

				}
				else
				{
					int user_id = 0;
					String price = "0";
					query = "select * from bid_on_product where bid_no=(select max(bid_no) from bid_on_product where product_id='" + product.id + "') and product_id='" + product.id + "'; ";

					SqlCommand selectbid = new SqlCommand(query, con);
					SqlDataReader sqlreader = selectbid.ExecuteReader();
					if (sqlreader.HasRows)
					{
						while (sqlreader.Read())
						{
							user_id = int.Parse(sqlreader["user_id"].ToString());
							price = sqlreader["total_price_increase"].ToString();
						}
						int totalprice = int.Parse(product.Price) + int.Parse(price);
						query = "insert into selled_products values('" + product.id.ToString() + "','" + reader["user_id"].ToString() + "','" + user_id + "','" + totalprice + "');";
						sqlreader.Close();
						SqlCommand insertselect = new SqlCommand(query, con);
						insertselect.ExecuteNonQuery();
					}
					else
					{
						continue;
					}
				}

				closedproducts.Add(product);
			}
			con.Close();
		}
	}
}