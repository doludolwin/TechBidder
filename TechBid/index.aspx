<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TechBid.index" %>

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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.17.0/jquery.validate.min.js"></script>
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="./resources/style.css">
    <script type="text/javascript" src="./resources/site.js"></script>
    <% if (Request.Cookies["login"] != null) { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "FailureLoginAlert();", true); } %>
    <% if (Request.Cookies["log"] != null) { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "PleaseLoginAlert();", true); } %> 
</head>
<body>

    <!-- navbar markup -->
    <nav>
        <div id="nav-wrapper">

            <div class="branding">
                 <a href="index.aspx"><h2><span>TechBidder</span></h2></a>
            </div>

            <div class="login">
                <form method="get" action="loginindex.aspx">
                    <div class="input-wrapper">
                        <input id="signin" class="input" type="text" placeholder="Username or Email" name="username" required>
                    </div>
                    <div class="input-wrapper">
                        <input id="signin" class="input" type="password" placeholder="Password" name="password" minlength="8" required>
                        <a href="seller.aspx"><i>Are You A Seller Sign Up here</i></a>
                    </div>
                    <div class="input-wrapper" style="width: 80px">
                        <input class="button is-primary" type="submit" value="LOGIN" runat="server"> 
                    </div>
                </form>
            </div>
        </div>
    </nav>


    <!-- header markup -->
    <header>

        <div id="signup-wrapper">
            <h2>Free Sign Up</h2>
            <p>Sign-up takes less than 30 seconds.</p>
            <form name="signupform" method="post" runat="server" onsubmit="return signupValidation()">

                    <input class="input" type="text" placeholder="Name" name="name" required>
                    <input class="input" type="text" placeholder="username" name="username" required>
                    <input class="input" type="text" placeholder="Address" name="address" required>
                    <input class="input" type="text" placeholder="pincode" name="pincode" id="pincode" minlength="6" maxlength="6" onfocusout="checkdigits(this)" required>
                    <input class="input" type="text" placeholder="landmark" name="landmark" required>
                    <input id="password-fuck" class="input" type="password" placeholder="Choose Password" name="password" minlength="8"  required>
                    <input class="input" type="email" placeholder="E-mail" name="email" required>
                    <input class="input" type="text" placeholder="Mobile Number" name="mobilenumber" id="phone" maxlength="10" minlength="10" onfocusout="checkdigits(this)" required>


                    <label>
                        <input type="checkbox" required>  I accept the Terms and Conditions and the Privacy Policy of TechBidder.com
                    </label>
                    <input class="button is-primary" type="submit" value="Create Account" runat="server" onserverclick="buyerSignup">
            </form>
        </div>

    </header>


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
                                    Current Price: <%=  liveproducts[i].winprice %>
                                </div>
                                <div id="image-footer__right">
                                   Bid Credits : <%= liveproducts[i].Credits %>x <i class="fa fa-star" aria-hidden="true"></i>
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
                        <form action="bidnow.aspx" method="get">
                            <div class="button-wrapper">
                                <input type="hidden" value=" <%=  liveproducts[i].id %>" name="productid"/>
                                 <input type="hidden" value=" <%=  liveproducts[i].Credits %>" name="credits"/>
                                 <input type="hidden" value=" %=  liveproductss[i].PriceIncrease %>" name="priceIncrease"/>
                                <input class="button is-primary" type="submit" value="Bid Now" />   
                            </div> 
                        </form>
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
                                <div id="image-footer__right">
                                  Bid Credits :  <%= products[i].Credits %>x <i class="fa fa-star" aria-hidden="true"></i>
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


</html>




