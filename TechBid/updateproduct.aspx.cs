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
	public partial class updateproduct : System.Web.UI.Page
	{
		public string name;
		public string price;
		public string date;
		public string intervel;
		public string description;
		public string image;
		public string id;
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ((Session["id"] == null && Session["role"] == null) || int.Parse(Session["role"].ToString()) != 2)
			{
				Response.Redirect("index.aspx");
			}
			productdetails();
			
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}
		public void productdetails()
		{
			id = Request.QueryString["product_id"].ToString();

			connection();

			string query = "select * from product_details where Id='" + id + "';";

			SqlCommand cmd = new SqlCommand(query, con);
			SqlDataReader reader = cmd.ExecuteReader();
			if(reader.HasRows){
				while (reader.Read())
				{
					name = reader["name"].ToString();
					description = reader["description"].ToString();
					price = reader["price"].ToString();
					date = reader["bid_start_time"].ToString().Replace(":", "-").Replace(" ", "-");
					image = reader["photo"].ToString();
					intervel = reader["bid_time_intervel"].ToString();

					string[] tokens = date.Split('-');
					string end_time = tokens[2] + "-" + tokens[1] + "-" + tokens[0] + "T" + tokens[3] + ":" + tokens[4] + ":" + tokens[5];
					DateTime enddate = Convert.ToDateTime(end_time);

					 date = enddate.ToString("yyyy-MM-dd HH:MM").Replace(" ", "T");

				}
			}
			con.Close();
			
		}


		public void update(object sender, EventArgs e)
		{
			String id= Request.Form["productid"];
			String name = Request.Form["name"];
			String price = Request.Form["price"];
			DateTime bidstarttime = Convert.ToDateTime(Request.Form["biddatetime"].ToString().Replace(' ', 'T'));
			String bidtime = Request.Form["bidtime"];
			String description = Request.Form["description"];
			DateTime bidendtime = bidstarttime.AddMinutes(int.Parse(bidtime));
			String filePath = "";
			HttpPostedFile postedFile = Request.Files["productimage"];
			connection();
			string query;
			SqlCommand cmd;
			//String imagename = name.Replace(" ", string.Empty) + bidstarttime.ToString().Replace(" ",string.Empty) + Path.GetExtension(postedFile.FileName).ToString();
			if (postedFile != null && postedFile.ContentLength > 0)
			{
				if (!Directory.Exists(Server.MapPath("./Uploads/")))
				{

					Directory.CreateDirectory(Server.MapPath("./Uploads/"));
				}
				filePath = Server.MapPath("./Uploads/") + Path.GetFileName(postedFile.FileName);
				postedFile.SaveAs(filePath);

				 query = "update product_details set photo='" + Path.GetFileName(postedFile.FileName) + "' where Id='" + id + "';";

				 cmd = new SqlCommand(query, con);
				cmd.ExecuteNonQuery();
			}

			 query = "update product_details set name='" + name + "',price='" + price + "',bid_start_time='" + bidstarttime + "',description='" + description + "',bid_time_intervel='" + bidtime + "',bid_end_time='" + bidendtime + "' where Id='" + id + "';";

			 cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();

			con.Close();
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "updationSucessAlert()", true);
			//Response.Redirect("sellerindex.aspx");
		}
	}
}