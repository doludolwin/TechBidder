<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminindex.aspx.cs" Inherits="TechBid.adminindex" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>TechBid | Home</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://fonts.googleapis.com/css?family=Lobster" rel="stylesheet">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="./resources/style.css">
    <script type="text/javascript" src="./resources/site.js"></script>
</head>
<body>

    <!-- navbar markup -->
    <nav>
        <div id="nav-wrapper">

            <div class="branding">
                <a href="index.aspx"><h2><span>TechBidder</span></h2></a>
            </div>

            <div class="adminlogin">
               <div class="tile is-ancestor">
        
                  <div class="tile is-parent is-2 anim">
                    <a href="adminseller.aspx">
                    <article class="tile is-child notification is-info">
                      <p class="sub-title">Sellers</p>
                    </article>
                    </a>
                  </div> 
        
                  <div class="tile is-parent is-2 anim">
                   <a href="#">
                    <article class="tile is-child notification is-danger">
                      <p class="sub-title">Buyers</p>
                    </article>
                    </a>
                  </div>
                    <div class="tile is-parent is-2 anim">
                    <a href="adminbid.aspx">
                    <article class="tile is-child notification is-primary">
                      <p class="sub-title">Bid Hisory</p>
                    </article>
                    </a>
                  </div>
                  <div class="tile is-parent is-2 anim">
                    <a href="adminpayment.aspx">
                    <article class="tile is-child notification is-warning">
                      <p class="sub-title">Payments</p>
                    </article>
                    </a>
                  </div>
                  <div class="tile is-parent is-2 anim">
                    <a href="adminproduct">
                    <article class="tile is-child notification is-success">
                      <p class="sub-title">Products</p>
                    </article>
                    </a>
                  </div>
                   <div class="tile is-parent is-2 anim">
                    <a href="logout.aspx">
                    <article class="tile is-child notification is-success">
                      <p class="sub-title">Logout</p>
                    </article>
                    </a>
                  </div>
             </div>
          </div>
       </div>
    </nav>


    <!-- Live auction part  -->
    <div class="live-auction">

         <div id="live-auction__header">
            <h2>Live Auctions</h2>
        </div>
         <%if (liveproducts.Count == 0) {%>
        <section class="hero is-small is-warning is-bold">
          <div class="hero-body">
            <div class="container has-text-centered">
              <h1 class="title">
                No Live Auctions
              </h1>
              <h2 class="subtitle">
                Please Check Later
              </h2>
            </div>
          </div>
        </section>
        <%} %>
        <%else{ %> 

        <%for(int i = 0; i <  liveproducts.Count; i++){%>
        <div class="cards">
                <div class="mycard">

                    <div class="image-wrapper">
                        <img src="<%= "./Uploads/"+liveproducts[i].Photo %>" alt="">
                    </div>
                         <div class="image-wrapper">
                            <div class="mycard-header">
                                    <%= liveproducts[i].Name %>
                            </div>
                            <div id="mrp">
                                 Current Winner : <%= liveproducts[i].winuser %>
                            </div>
                            <div id="image-footer">
                                <div id="image-footer__left">                                
                                    Current Price : <%=  liveproducts[i].winprice %>
                                </div>
                                <div id="image-footer__right">
                                   Total Bids : <%= liveproducts[i].Credits %>x <i class="fa fa-star" aria-hidden="true"></i>
                                </div>                                
                            </div>
                        <div id="timer-wrapper">
                            <div id="bet-price">
                                <i class="fas fa-rupee-sign"></i>Bid Price : <%= liveproducts[i].PriceIncrease %>
                            </div>
                            Ends in :
                            <div id="<%= "countdown"+liveproducts[i].id %>">
                                <%=  liveproducts[i].end_time %>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
        <%} %>
    <%} %>
    <!-- upcoming Auction part -->
        <div class="upcoming" >
        <div id="live-auction__header">
            <h2>Upcoming Auctions</h2>
        </div>
         <%if (products.Count == 0) {%>
        <section class="hero is-small is-info is-bold">
          <div class="hero-body">
            <div class="container has-text-centered">
              <h1 class="title">
                No Closed Auctions
              </h1>
              <h2 class="subtitle">
                Please Check Later
              </h2>
            </div>
          </div>
        </section>
        <%} %>
        <%else{ %> 

        <%for(int i = 0; i < products.Count; i++){%>
        <div class="cards">
                <div class="mycard">

                    <div class="image-wrapper">
                        <img src="<%= "./Uploads/"+products[i].Photo %>" alt="">
                    </div>
                         <div class="image-wrapper">
                            <div class="mycard-header">
                                    <%= products[i].Name %>
                            </div>
                            <div id="mrp">
                                  <%= products[i].Description %>
                            </div>
                            <div id="image-footer">
                                <div id="image-footer__left">                                
                                    Start Price :<%= products[i].Price %>
                                </div>
                            </div>
                        <div id="timer-wrapper">
                            <div id="bet-price">
                                <i class="fas fa-rupee-sign"></i>Bid price : <%= products[i].PriceIncrease %>
                            </div>
                             Starts In :&nbsp;
                            <div id="<%= "countdown"+products[i].id %>">
                               <%= products[i].Start_time %>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
        <%} %>
         <%} %>
    </div>
         <!--- Closed Auctions -->
        <div class="upcoming" >
        <div id="live-auction__header">
            <h2>Closed Auctions</h2>
        </div>
     <%if (closedproducts.Count == 0) {%>
        <section class="hero is-small is-info is-bold">
          <div class="hero-body">
            <div class="container has-text-centered">
              <h1 class="title">
                No Closed Auctions
              </h1>
              <h2 class="subtitle">
                Please Check Later
              </h2>
            </div>
          </div>
        </section>
        <%} %>
        <%else{ %> 

        <%for(int i = 0; i < closedproducts.Count; i++){%>
        <div class="cards">
                <div class="mycard">

                    <div class="image-wrapper">
                        <img src="<%= "./Uploads/"+closedproducts[i].Photo %>" alt="">
                    </div>
                         <div class="image-wrapper">
                            <div class="mycard-header">
                                    <%= closedproducts[i].Name %>
                            </div>
                            <div id="mrp">
                                 <%=closedproducts[i].Price %>
                            </div>
                            <div id="image-footer">
                                <div id="image-footer__left">
                                    <%= closedproducts[i].Description %>
                                </div>
                            <div id="image-footer__right">
                                Winner  :
                                    <%= closedproducts[i].winuser %> <i class="fa fa-star" aria-hidden="true"></i>
                                </div>
                            </div>
                         <div id="timer-wrapper">
                            <div id="bet-price">
                                Final Price  :
                                <i class="fas fa-rupee-sign"></i><%= closedproducts[i].winprice %>
                            </div>                   
                        </div> 
             
                    </div>
                </div>
        </div>
        <%} %>
    <%} %>
    </div>
    </div>
  </body>
 </html>
