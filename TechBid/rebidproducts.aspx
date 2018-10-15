<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rebidproducts.aspx.cs" Inherits="TechBid.rebidproducts" %>

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

    <div class="live-auction">
    <% if (products.Count == 0)
             { %>
        <div id="live-auction__header">
            <h2>No Rebid Produts</h2>
        </div>
    <%} %>
        <%else
             { %>
        <div id="live-auction__header">
            <h2>Rebid Products</h2>
        </div>
        <%for (int i = 0; i < products.Count; i++)
             {%>
        <div class="cards">
                <div class="mycard">
                    <div class="image-wrapper">
                        <img src="<%= "./Uploads/" + products[i].Photo %>" alt="">
                    </div>
                         <div class="image-wrapper">
                            <div class="mycard-header">
                                    <%= products[i].Name %>
                            </div>
                            <div id="mrp">
                                 <%= products[i].Price %>
                            </div>
                            <div id="image-footer">
                                <div id="image-footer__left">
                                    <%= products[i].Description %>
                                </div>
                                <div id="image-footer__right">
                                   
                                </div>
                            </div> 
                        <form action="updateproduct.aspx" method="get">
                            <div class="button-wrapper">
                                 <input type="hidden" value= <%=  products[i].id %> name="product_id"/>
                                <input class="button is-primary" type="submit" value="Rebid" />                                                           
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