using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class adminpayment : System.Web.UI.Page
	{
		SqlConnection con;
		public List<ProductDetails> payments = new List<ProductDetails>();
		public String name;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 3)
			{
				Response.Redirect("index.aspx");
			}
			sellerpaymenthistory();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}
		public void sellerpaymenthistory()
		{
			connection();
			string query = "select payment.*,credit_purchase.* from payment inner join credit_purchase on credit_purchase.purcahse_date=payment.paymentdate where payment.product_id=0 and credit_purchase.user_id=payment.user_id;";
			SqlCommand select = new SqlCommand(query, con);
			SqlDataReader payreader = select.ExecuteReader();
			if (payreader.HasRows)
			{
				while (payreader.Read())
				{
					var product = new ProductDetails();
					product.Credits = int.Parse(payreader["credits"].ToString());
					product.Price = payreader["amount"].ToString();
					product.Start_time = payreader["paymentdate"].ToString();
					query = "select name from buyer_details where user_id='" + payreader["user_id"].ToString() + "';";
					select = new SqlCommand(query, con);
					product.winuser = select.ExecuteScalar().ToString();
					product.end_time = payreader["cardholder"].ToString();
					payments.Add(product);
				}
			}				
		}
	}
}