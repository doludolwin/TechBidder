using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class adminbuyer : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> buyers = new List<ProductDetails>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 3)
			{
				Response.Redirect("index.aspx");
			}
			buyerhistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}
		public void buyerhistory()
		{
			connection();
			String query = "select users.*,buyer_details.* from users inner join buyer_details on users.Id=buyer_details.user_id where users.role=1;";
			SqlCommand cmd = new SqlCommand(query, con);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{

				while (reader.Read())
				{

					var product = new ProductDetails();
					product.Name = reader["name"].ToString();
					product.Description = reader["email"].ToString();
					product.Photo = reader["address"].ToString();
					product.winprice = reader["phone_no"].ToString();

					query = "select count(*) from selled_products where buyer_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.Credits = int.Parse(cmd.ExecuteScalar().ToString());

					query = "select count(*) from bid_on_product where user_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.PriceIncrease = int.Parse(cmd.ExecuteScalar().ToString());

					query = "select Sum(credits) from credit_purchase where user_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.Price = cmd.ExecuteScalar().ToString();


					query = "select count(*) from payment inner join selled_products on payment.product_id=selled_products.product_id where selled_products.buyer_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.winuser = cmd.ExecuteScalar().ToString();

					buyers.Add(product);

				}

			}

		}
	}
}