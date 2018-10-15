using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class winhistory : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> wins = new List<ProductDetails>();
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
			winproducts();
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

		public void winproducts()
		{
			string query = "select product_details.Id,product_details.name,product_details.photo,product_details.bid_end_time,seller_details.name as seller,selled_products.final_price from selled_products inner join product_details on selled_products.product_id=product_details.Id inner join seller_details on selled_products.seller_id=seller_details.user_id where  selled_products.buyer_id='" + Session["id"]+"';";
			connection();
			SqlCommand command = new SqlCommand(query, con);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					var win = new ProductDetails();
					win.winprice = " ";
					query = "select * from payment where product_id = '" + reader["Id"].ToString() + "';";
					command = new SqlCommand(query, con);
					SqlDataReader payreader = command.ExecuteReader();
					//if (payreader.HasRows)
					//{
					//	win.end_time = "true";
					//	win.winprice = "true";
					//} 
					win.id = int.Parse(reader["Id"].ToString());
					win.Name = reader["name"].ToString();
					win.Photo = reader["photo"].ToString();
					win.Price =reader["final_price"].ToString();
					win.winuser = reader["seller"].ToString();

					string end_time= reader["bid_end_time"].ToString().Replace(":", "-").Replace(" ", "-");
					string[] tokens = end_time.Split('-');
					end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
					DateTime enddate = Convert.ToDateTime(end_time).AddDays(2);

					string end = enddate.ToString().Replace(":", "-").Replace(" ", "-");
					tokens = end.Split('-');
					win.end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
					if (payreader.HasRows)
					{
						win.end_time = "true";
						win.winprice = "true";
					}
					else if (enddate < DateTime.Now && win.winprice != "true")
					{
						win.winprice = "false";
						win.end_time = "false";
					}

					wins.Add(win);
				}
			}
			con.Close();
		}
	}

}