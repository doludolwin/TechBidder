<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminproduct.aspx.cs" Inherits="TechBid.adminproduct" %>

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
                   <a href="adminbuyer.aspx">
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
                    <a href="adminproduct.aspx">
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

     <!-- header markup  -->
    <header>

        <div id="history-wrapper">
        <%if (products.Count == 0)
            { %>
             <div id="bid_history">
                <h2>No Product History</h2>
            </div>
        <%} %>
        <%else
    { %>
        <div id="bid_history">
            <h2>Product History</h2>
        </div>
           <table class="table is-bordered is-striped is-fullwidth is-hoverable">
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Name</th>
                        <th>Total Number Of Bids</th>
                        <th>Status</th>
                        <th>Price Increase</th>
                        <th>Bid Winner</th>
                        <th>Final Price</th>
                        <th>Payment Date</th> 
                        
                    </tr>
                </thead>
               <tbody>
                    <%for (int i = 0; i < products.Count; i++)
                    {%>
                   <tr>
                       <th>
                            <img src="<%= "./Uploads/" + products[i].Photo %>" width="200" height="200" />
                       </th>
                       <th>
                            <%= products[i].Name %>
                       </th>
                       <th>
                           <%= products[i].Credits %>
                       </th>
                       <th>
                           <%= products[i].Description %>
                       </th>
                      <th>
                           <%= products[i].PriceIncrease %>
                       </th>
                       <th>
                           <%= products[i].winuser %>
                       </th>
                       <th>
                            <%= products[i].winprice %>
                       </th>
                       <th>
                           <%= products[i].Start_time %>
                       </th>
                   </tr>
                   <%} %>
               </tbody>

           </table>
            <%} %>
        </div>

    </header>


    </body>
    </html>
