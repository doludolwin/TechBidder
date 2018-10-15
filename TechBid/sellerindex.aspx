<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sellerindex.aspx.cs" Inherits="TechBid.sellerindex" %>


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
    <script>
        function addproducts() {
            window.location = "addproducts.aspx";    
        }
        function producthistory() {

            window.location = "sellerproduct.aspx";
        }
    </script>
</head>
<body>

    <!-- navbar markup -->
    <nav>
        <div id="nav-wrapper">

            <div class="branding">
                <a href="index.aspx"><h2><span>TechBidder</span></h2></a>
            </div>

            <div class="login">
                <div class="input-wrapper" style="width: 200px">
                     <input class="button is-primary is-rounded" type="submit" onclick="addproducts();" value="prduct for bid" style="width:200px">
                </div>
                <div class="input-wrapper" style="width: 200px">
                    <input class="button is-primary is-rounded" type="submit" value="Product History" onclick="producthistory();" style="width:200px">
                </div>


                <div class="input-wrapper" style="width: 80px">
                    <div class="dropdown">
                           <input class="button is-primary" type="submit" value="<%= name %>" style="width: 150px">
                      <div class="dropdown-content">
                        <a href="sellerpayment.aspx">Payment History</a>
                        <a href="sellerorder">Order History</a>
                        <a href="sellercart">Product Cart</a>
                        <a href="rebidproducts">Rebid Products</a>
                        <a href="logout.aspx">Logout</a>
                      </div>
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
                            <div class="button-wrapper" style="display:inline;" >
                                <form action="updateproduct.aspx" method="get" style="display:inline;">
                                    <input type="hidden" value= <%=  products[i].id %> name="product_id"/>
                                    <input class="button is-primary" type="submit" value="Update" style="display:inline;vertical-align:middle" /> 
                                 </form>
                                 <form action="removeproduct.aspx" method="get" style="display:inline;">                                  
                                    <input type="hidden" value= <%=  products[i].id %> name="productid"/>
                                    <input class="button is-primary" type="submit" value="Remove" style="display:inline;vertical-align:middle" />   
                                </form>
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
