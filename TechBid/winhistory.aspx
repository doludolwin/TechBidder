<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="winhistory.aspx.cs" Inherits="TechBid.winhistory" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>TechBid | Home</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
    <link href="https://fonts.googleapis.com/css?family=Lobster" rel="stylesheet">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="./resources/style.css">
    <script type="text/javascript" src="./resources/site.js"></script>
    <% if (Request.Cookies["paysucess"] != null) { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "PaymentSucessAlert();", true); } %>
    <% if (Request.Cookies["payunsucess"] != null) { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "PaymentFailureAlert();", true); } %> 

</head>
<body id="loginindex">

    <!-- navbar markup -->
    <nav>
        <div id="nav-wrapper">

            <div class="branding">
               <a href="index.aspx"><h2><span>TechBidder</span></h2></a>
            </div>

            <div class="login">
                <div class="input-wrapper" style="width: 200px">
                     <input class="button is-primary is-rounded" type="submit" value="You have <%= credits %> credits" style="width:200px">
                </div>
                    <div class="input-wrapper" id="creditbuy" style="width: 200px">
                       
                            <input class="button is-primary is-rounded" type="submit" value="buy credits" style="width:200px" onclick="buycredits();">
                       
                    </div>
                <div class="input-wrapper" style="width: 80px">
                    <div class="dropdown">
                           <input class="button is-primary" type="submit" value=<%= name %>  style="width: 150px">
                      <div class="dropdown-content is-rounded">
                        <a href="bidhistory.aspx">Bid History</a>
                        <a href="orderhistory.aspx">Order History</a>
                        <a href="paymenthistory.aspx">payment History</a>
                        <a href="logout.aspx">Logout</a>
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </nav>

    <div class="live-auction">
    <% if (wins.Count == 0)
             { %>
        <div id="live-auction__header">
            <h2>No Win History</h2>
        </div>
    <%} %>
        <%else
             { %>
        <div id="live-auction__header">
            <h2>Win History</h2>
        </div>
        <%for (int i = 0; i < wins.Count; i++)
             {%>
        <div class="cards">
                <div class="mycard">
                    <div class="image-wrapper">
                        <img src="<%= "./Uploads/" + wins[i].Photo %>" alt="">
                    </div>
                         <div class="image-wrapper">
                            <div class="mycard-header">
                                    <%= wins[i].Name %>
                            </div>
                            <div id="mrp">
                                 <%= wins[i].Price %>
                            </div>
                            <div id="image-footer">
                                <div id="image-footer__left">
                                    seller:&nbsp
                                    <%=  wins[i].winuser %>
                                </div>
                                <div id="image-footer__right">
                                   
                                </div>
                            </div> 
                              <%if(wins[i].winprice == " ")
                                    {%>
                                <div id="timer-wrapper">
                                    Make Payment In
                                    <div id="<%= "countdown" + wins[i].id %>">
                                        <%=  wins[i].end_time %>
                                    </div>
                                  </div>
                                <%} %>
                        <form action="payment.aspx" method="get">
                            <div class="button-wrapper">
                                <input type="hidden" value= <%=  wins[i].Price %> name="amount"/>
                                 <input type="hidden" value= <%=  wins[i].id %> name="productid"/>
                                <%if (wins[i].winprice == "true")
                                    { %>
                                <input class="button is-primary" type="submit" value="Paid" disabled="disabled"/>  
                                <%} %>
                                <%else if (wins[i].winprice == "false")
                                  { %>                                
                                <input class="button is-primary" type="submit" value="Payment Time Expired" disabled="disabled"/>

                               <%} %>

                                <%else
                                 { %>
                                <input class="button is-primary" type="submit" value="<%= "Pay" + wins[i].Price %> " /> 
                                 
                                
                                <%} %>
                            </div> 
                        </form>
                    </div>
                </div>
        </div>
        <%} %>
     <%} %>
    </div>

</body>
</html>
