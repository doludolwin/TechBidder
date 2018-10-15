using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class adminseller : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> sellers = new List<ProductDetails>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 3)
			{
				Response.Redirect("index.aspx");
			}
			sellerhistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}
		public void sellerhistory()
		{
			connection();
			String query = "select users.*,seller_details.* from users inner join seller_details on users.Id=seller_details.user_id where users.role=2;";
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

					query = "select count(*) from product_details where user_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.Credits=int.Parse(cmd.ExecuteScalar().ToString());

					query = "select count(*) from payment inner join selled_products on payment.product_id=selled_products.product_id where selled_products.seller_id='" + reader["user_id"].ToString() + "';";
					cmd = new SqlCommand(query, con);
					product.winuser = cmd.ExecuteScalar().ToString();

					sellers.Add(product);
					
				}

			}

		}
	}
}