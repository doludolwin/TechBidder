using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class bidnow : System.Web.UI.Page
	{
		SqlConnection con;
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session["id"] == null ||  int.Parse(Session["role"].ToString()) != 1)
			{
				HttpCookie cookie = new HttpCookie("log");
				cookie.Value = "false";
				cookie.Expires = DateTime.Now.AddSeconds(1);
				Response.SetCookie(cookie);
				Response.Redirect("index.aspx");
			}
			bidforproduct();
		}
		public void connection()
		{

			con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dolwi\source\repos\techbid\TechBid\App_Data\TechBid.mdf;Integrated Security=True");
			con.Open();

		}

		public void bidforproduct()
		{
			int user_id = int.Parse(Session["id"].ToString());
			int product_id = int.Parse(Request.QueryString["productid"]);
			int pcredits = int.Parse(Request.QueryString["credits"]);
			int priceincrease = int.Parse(Request.QueryString["priceIncrease"]);
			int bid_no=0;
			string bid;
			string end_date="";
			string time="";
			DateTime bid_end_date;
			DateTime end_time;
			connection();

			string query = "select * from product_details where Id='" + product_id + "';";
			SqlCommand selectproduct = new SqlCommand(query, con);
			SqlDataReader reader = selectproduct.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					end_date= reader["bid_end_time"].ToString();
					time = reader["bid_time_intervel"].ToString();
				}
			}
			reader.Close();
			bid_end_date = Convert.ToDateTime(end_date);
			if(bid_end_date < DateTime.Now)
			{
				con.Close();
				Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "bidFailureAlert();", true);
			}
			

			query = "select max(bid_no) from bid_on_product where product_id='"+product_id+"';";
			
			SqlCommand selectbid = new SqlCommand(query, con);

			if (selectbid.ExecuteScalar().ToString() == string.Empty)
			{
				bid_no++;				
			}
			else
			{
				bid_no = Convert.ToInt32(selectbid.ExecuteScalar());
				bid_no++;
			}
			
			/**if (readerb.HasRows)
			{
				while (readerb.Read())
				{
					Response.Write(reader["max"]);
					//bid_no = int.Parse(readerb["bid_no"].ToString());
				}
				bid_no++;
			}
			else
			{
				bid_no++;
			}*/
			//readerb.Close();

			query = "insert into bid_on_product values('" + product_id + "','" + Session["id"].ToString() + "','" + pcredits + "','" + DateTime.Now.ToString() + "','" + bid_no + "','" + bid_no*int.Parse(priceincrease.ToString()) + "');";

			SqlCommand insertbid = new SqlCommand(query, con);
			insertbid.ExecuteNonQuery();

			query = "select credits from credit_details where user_id='" + Session["id"].ToString() + "';";
			SqlCommand selectcredit = new SqlCommand(query, con);

			int totalcredits = Convert.ToInt32(selectcredit.ExecuteScalar());

			int newcredit = totalcredits - pcredits;
			query = "update credit_details set credits='" + newcredit + "';";
			SqlCommand updatecredit = new SqlCommand(query, con);
			updatecredit.ExecuteNonQuery();

			end_time=bid_end_date.AddMinutes(2);
			query = "update product_details set bid_end_time='" + end_time.ToString() + "' where Id='"+product_id+"';";
			updatecredit = new SqlCommand(query, con);
			updatecredit.ExecuteNonQuery();

			con.Close();

			

			HttpCookie cookie = new HttpCookie("bidsucess");
			cookie.Value = "false";
			cookie.Expires = DateTime.Now.AddSeconds(1);
			Response.SetCookie(cookie);
			Response.Redirect("loginindex.aspx");
		}
	}


}