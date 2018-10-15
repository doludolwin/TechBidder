using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class cancelorder : System.Web.UI.Page
	{
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{
			cancelexpiredorder();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True;MultipleActiveResultSets=true;");
			con.Open();

		}

		public void cancelexpiredorder()
		{
			connection();
			string orderid = Request.QueryString["orderid"].ToString();

			string query = "delete from selled_products where Id='" + orderid + "';";

			SqlCommand cmd = new SqlCommand(query, con);

			cmd.ExecuteNonQuery();

			con.Close();
			Response.Redirect("sellercart.aspx");

		}
	}
}