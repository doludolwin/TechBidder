using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class removeproduct : System.Web.UI.Page
	{
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 2)
			{
				Response.Redirect("index.aspx");
			}
			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

			string query = "select * from bid_on_product where product_id='" + Request.QueryString["productid"].ToString() + "';";
			SqlCommand cmd = new SqlCommand(query, con);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				con.Close();
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "removeFailureAlert()", true);
				Response.Redirect("sellerindex.aspx");
			}
			else
			{
				reader.Close();
				query = "delete from product_details where Id='" + Request.QueryString["productid"].ToString() + "';";
				cmd = new SqlCommand(query, con);
				cmd.ExecuteNonQuery();
				con.Close();
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "removeSucessAlert()", true);
				Response.Redirect("sellerindex.aspx");
			}
			

			
		}
	}
}