using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class rebidproducts : System.Web.UI.Page
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
			rebidproduct();
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
			string query = "select * from buyer_details where user_id='" + Session["id"].ToString() + "';";
			SqlCommand sqlCommand = new SqlCommand(query, con);
			SqlDataReader reader = sqlCommand.ExecuteReader();
			while (reader.Read())
			{
				name = reader["name"].ToString();
			}
			con.Close();
		}

		public void rebidproduct()
		{
			connection();
			string query = "select * from product_details where user_id='" + Session["id"].ToString() + "';";

			SqlCommand cmd = new SqlCommand(query, con);

			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					string Start_time = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
					string[] starttokens = Start_time.Split('-');
					Start_time = starttokens[2] + "-" + starttokens[1] + "-" + starttokens[0] + "T" + starttokens[3] + ":" + starttokens[4] + ":" + starttokens[5];
					DateTime startdate = Convert.ToDateTime(Start_time);;

					string end_time = reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");
					string[] tokens = end_time.Split('-');
					end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
					DateTime enddate = Convert.ToDateTime(end_time);
					if (startdate >= DateTime.Now || enddate >= DateTime.Now)
					{
						continue;
					}

					query = "select * from selled_products where product_id='" + reader["Id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					SqlDataReader sellreader = cmd.ExecuteReader();
					if (sellreader.HasRows)
					{
						continue;
					}
					var product = new ProductDetails();
					product.id = int.Parse(reader["Id"].ToString());
					product.Name = reader["name"].ToString();
					product.Description = reader["description"].ToString();
					product.Price = reader["price"].ToString();
					product.Start_time = Start_time;
					product.Photo = reader["photo"].ToString();
					product.Time_intervel = reader["bid_time_intervel"].ToString();
					product.end_time = end_time;

					products.Add(product);
				}
			}
		}
	}
}