using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class adminbid : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> bids = new List<ProductDetails>();
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 3)
			{
				Response.Redirect("index.aspx");
			}
			userbidhistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}
		public void userbidhistory()
		{
			string query = "select product_details.name,product_details.photo,bid_on_product.credits,bid_on_product.biddatetime,buyer_details.name as bidder from bid_on_product inner join product_details on bid_on_product.product_id=product_details.Id inner join buyer_details on bid_on_product.user_id=buyer_details.user_id;";
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
					bid.winuser = reader["bidder"].ToString();
					bid.Credits = int.Parse(reader["credits"].ToString());
					bid.Start_time = reader["biddatetime"].ToString();
					bids.Add(bid);
				}
			}
			con.Close();
		}
	}
}