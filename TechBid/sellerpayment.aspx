<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sellerpayment.aspx.cs" Inherits="TechBid.sellerpayment" %>

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
                           <input class="button is-primary" type="submit" value="<%= name %>"  style="width: 150px">
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


    <!-- header markup  -->
    <header>

        <div id="history-wrapper">
        <%if (payments.Count == 0)
            { %>
             <div id="bid_history">
                <h2>No Payments Yet</h2>
            </div>
        <%} %>
        <%else
    { %>
        <div id="bid_history">
            <h2>Product Payments</h2>
        </div>
           <table class="table is-bordered is-striped is-fullwidth is-hoverable">
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Name</th>
                        <th>Amount</th>
                        <th>Paid By</th>
                        <th>Account Holder</th>
                        <th>Payment Date</th>
                    </tr>
                </thead>
               <tbody>
                    <%for (int i = 0; i < payments.Count; i++)
                    {%>
                   <tr>
                       <th>
                            <img src="<%= "./Uploads/" + payments[i].Photo %>" width="200" height="200" />
                       </th>
                       <th>
                            <%= payments[i].Name %>
                       </th>
                       <th>
                            <%= payments[i].Price %>
                       </th>
                       <th>
                           <%= payments[i].winuser %>
                       </th>
                       <th>
                           <%= payments[i].end_time %>
                       </th>
                      <th>
                           <%= payments[i].Start_time %>
                      </th>
                   </tr>
                   <%} %>
               </tbody>

           </table>
            <%} %>
        </div>

    </body>
</html>
