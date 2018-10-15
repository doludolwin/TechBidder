using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TechBid
{
	public partial class payment : System.Web.UI.Page
	{
		SqlConnection con;
		public String paymentamount;
		public String nocredits = null;
		public String amount=null;
		public String productid = null;
		public String name;
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session["id"] == null)
			{
				Response.Redirect("index.aspx");
			}
			else if(Request.QueryString["credits"] == null && Request.QueryString["amount"] == null)
			{
				Response.Redirect("loginindex.aspx");
			}
			else if(Request.QueryString["credits"] != null && Request.QueryString["amount"] == null)
			{
				calculatecreditamount();
				findauthname();
			}
			else if (Request.QueryString["credits"] == null && Request.QueryString["amount"] != null)
			{
				productamount();
				findauthname();
			}
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
		public void calculatecreditamount()
		{
			int credits = int.Parse(Request.QueryString["credits"]);
			paymentamount = (credits * 10).ToString();
			nocredits = credits.ToString();
		}
		public void productamount()
		{
			amount = Request.QueryString["amount"].ToString();
			productid = Request.QueryString["productid"].ToString();
		}
		public void creditpayment(object sender, EventArgs e)
		{
			int user_id = int.Parse(Session["id"].ToString());
			string username = Session["username"].ToString();

			string cardnumber = Request.Form["cardnumber"].ToString().Replace(" ",string.Empty);
			string cardholder = Request.Form["cardholder"].ToString();
			string expirydate = Request.Form["expirydate"].ToString();
			string cvv = Request.Form["cvv"].ToString();
			string credits = Request.Form["credits"].ToString();
			string amount = Request.Form["amount"].ToString();

			String query="insert into payment values('"+ user_id+ "','" + cardnumber + "','" + cardholder + "','" + expirydate + "','" + int.Parse(cvv) + "','" + int.Parse(amount) + "','" + DateTime.Now.ToString() + "',0);";
			connection();

			SqlCommand command = new SqlCommand(query, con);
			int cmd = command.ExecuteNonQuery();
			HttpCookie cookie;
			if (cmd != 1)
			{
				con.Close();
				cookie = new HttpCookie("payunsucess");
				cookie.Value = "false";
				cookie.Expires = DateTime.Now.AddSeconds(1);
				Response.SetCookie(cookie);
				Response.Redirect("loginindex.aspx");
			}


			query = "insert into credit_purchase values('" + DateTime.Now.ToString() + "','" + amount + "','" + credits + "','" + user_id + "');";
			SqlCommand insertpurchase = new SqlCommand(query, con);
			insertpurchase.ExecuteNonQuery();

			query = "select * from credit_details where user_id='" + user_id + "';";
			SqlCommand getcredits = new SqlCommand(query, con);
			SqlDataReader reader = getcredits.ExecuteReader();
			int totalcredits = 0;
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					totalcredits = int.Parse(reader["credits"].ToString());
				}

				reader.Close();
				totalcredits += int.Parse(credits);
				query="update credit_details set credits='"+ totalcredits +"',updated_at='"+DateTime.Now.ToString() +"' where user_id='"+ user_id +"'; ";
				SqlCommand updatecredit = new SqlCommand(query, con);
				updatecredit.ExecuteNonQuery();
			}
			else
			{
				reader.Close();
				query = "insert into credit_details values('" + credits + "','" + DateTime.Now.ToString() + "','" + user_id + "');";
				SqlCommand insertcredit = new SqlCommand(query, con);
				insertcredit.ExecuteNonQuery();
			}
			con.Close();

			cookie = new HttpCookie("paysucess");
			cookie.Value = "false";
			cookie.Expires = DateTime.Now.AddSeconds(1);
			Response.SetCookie(cookie);
			Response.Redirect("loginindex.aspx");

		}

		public void productpayment(object sender, EventArgs e)
		{
			int user_id = int.Parse(Session["id"].ToString());
			string username = Session["username"].ToString();

			string cardnumber = Request.Form["cardnumber"].ToString().Replace(" ", string.Empty);
			string cardholder = Request.Form["cardholder"].ToString();
			string expirydate = Request.Form["expirydate"].ToString();
			string cvv = Request.Form["cvv"].ToString();
			string amount = Request.Form["payamount"].ToString();
			String query = "insert into payment values('" + user_id + "','" + cardnumber + "','" + cardholder + "','" + expirydate + "','" + int.Parse(cvv) + "','" + int.Parse(amount) + "','" + DateTime.Now.ToString() + "','" + Request.Form["productid"].ToString() +"');";
			connection();
			SqlCommand command = new SqlCommand(query, con);
			int cmd = command.ExecuteNonQuery();
			HttpCookie cookie;
			if (cmd != 1)
			{
				con.Close();
				cookie = new HttpCookie("payunsucess");
				cookie.Value = "false";
				cookie.Expires = DateTime.Now.AddSeconds(1);
				Response.SetCookie(cookie);
				Response.Redirect("winhistory.aspx");
			}

			cookie = new HttpCookie("paysucess");
			cookie.Value = "false";
			cookie.Expires = DateTime.Now.AddSeconds(1);
			Response.SetCookie(cookie);
			Response.Redirect("winhistory.aspx");

		}
	}
}