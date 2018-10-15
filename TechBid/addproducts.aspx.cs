using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
namespace TechBid
{
	public partial class addproducts : System.Web.UI.Page
	{
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 2)
			{
				Response.Redirect("index.aspx");
			}
		}

		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}

		public void insertProducts(object sender, EventArgs e)
		{
	
			String name = Request.Form["name"];
			String price = Request.Form["price"];
			DateTime bidstarttime =Convert.ToDateTime(Request.Form["biddatetime"].ToString().Replace(' ','T'));
			//Response.Write(bidstarttime);
			String bidtime = Request.Form["bidtime"];
			String description = Request.Form["description"];
			DateTime bidendtime = bidstarttime.AddMinutes(int.Parse(bidtime));
			String filePath="";
			HttpPostedFile postedFile = Request.Files["productimage"];

			//String imagename = name.Replace(" ", string.Empty) + bidstarttime.ToString().Replace(" ",string.Empty) + Path.GetExtension(postedFile.FileName).ToString();
			if (postedFile != null && postedFile.ContentLength > 0)
			{
				if (!Directory.Exists(Server.MapPath("./Uploads/")))
				{

					Directory.CreateDirectory(Server.MapPath("./Uploads/"));
				}
				filePath = Server.MapPath("./Uploads/")+ Path.GetFileName(postedFile.FileName);
				postedFile.SaveAs(filePath);
			}
			else
			{
				Response.Write("no imge");
			}

			String query= "insert into product_details (name,description,price,bid_start_time,photo,bid_time_intervel,user_id,bid_end_time) values ('" + name + "','" + description + "','" + int.Parse(price) + "','" + bidstarttime.ToString() + "','" + Path.GetFileName(postedFile.FileName).ToString() + "','" + int.Parse(bidtime) + "','" + int.Parse(Session["id"].ToString()) + "','" + bidendtime.ToString() + "');";
			//String query = "insert into product_details  (name,description,price,bid_start_time,photo,bid_time_intervel,user_id,bid_end_time) values ('"+ name +"','"+ description +"','" + int.Parse(price) +"','"+ bidstarttime.ToString() + "','" + filePath.ToString() + "',30,19,'dfsdfs');";
			connection();
			SqlCommand inserrproduct = new SqlCommand(query, con);
			try
			{
				int cmd=inserrproduct.ExecuteNonQuery();
				con.Close();
				Response.Write(cmd);
			}
			catch(Exception ex)
			{
				con.Close();
				Response.Write("addproducts.aspx");
			}
			con.Close();
			Response.Redirect("sellerindex.aspx");

		}
	}
}