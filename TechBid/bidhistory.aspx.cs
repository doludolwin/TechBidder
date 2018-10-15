using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class bidhistory : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> bids= new List<ProductDetails>();
		public String credits;
		public String name;
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session["id"] == null)
			{
				Response.Redirect("index.aspx");
			}
			findauthname();
			findcredits();
			userbidhistory();
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
		public void userbidhistory()
		{
			string query = "select product_details.name,product_details.photo,bid_on_product.credits,bid_on_product.biddatetime from bid_on_product inner join product_details on bid_on_product.user_id='" + Session["id"] + "' and bid_on_product.product_id=product_details.Id;";
			connection();
			SqlCommand command = new SqlCommand(query, con);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					var bid = new ProductDetails();
					bid.Name = reader["name"].ToString();
					bid.Photo = reader["photo"].ToString();
					bid.Credits = int.Parse(reader["credits"].ToString());
					bid.Start_time = reader["biddatetime"].ToString();
					bids.Add(bid);
				}
			}
			con.Close();
		}
	}
}