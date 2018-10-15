<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paymenthistory.aspx.cs" Inherits="TechBid.paymenthistory" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>TechBid | Home</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU" crossorigin="anonymous">
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
                     <input class="button is-primary is-rounded" type="submit" value="You have <%= credits %> credits" style="width:200px">
                </div>
                    <div class="input-wrapper" id="creditbuy" style="width: 200px">
                       
                            <input class="button is-primary is-rounded" type="submit" value="buy credits" style="width:200px" onclick="buycredits();">
                       
                    </div>
                <div class="input-wrapper" style="width: 80px">
                    <div class="dropdown">
                           <input class="button is-primary" type="submit" value=<%= name %>  style="width: 150px">
                      <div class="dropdown-content is-rounded">
                        <a href="orderhistory.aspx">Order History</a>
                        <a href="winhistory.aspx">Win History</a>
                        <a href="paymenthistory.aspx">payment History</a>
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
           <% if (payments.Count == 0)
               { %>
            <div id="live-auction__header">
                <h2>No Payment History</h2>
            </div>
             <%} %>
        <%else
    { %> 
            <div id="bid_history">
                <h2>Payment History</h2>
            </div>
           <table class="table is-bordered is-striped is-fullwidth is-hoverable">
                <thead>
                    <tr>
                        <th>Payment Type</th>
                        <th>Card Holder</th>
                        <th>Card Number</th>
                        <th>Amount</th>
                        <th>Product Name</th>
                        <th>Product Photo</th> 
                        <th>Payment Time</th>
                    </tr>
                </thead>
               <tbody>
                    <%for (int i = 0; i < payments.Count; i++)
    {%>
                   <tr>
                       <th>
                            <%= payments[i].winuser %>
                       </th>
                       <th>
                            <%= payments[i].Name %>
                       </th>
                       <th>
                           <%= payments[i].Description %>
                       </th>
                      <th>
                           <%= payments[i].Price %>
                      </th>
                       <%if (payments[i].winprice != null)
                           { %>
                      <th>
                           <%= payments[i].winprice %>
                      </th>
                       <th>
                            <img src="<%= "./Uploads/" + payments[i].Photo %>" width="200" height="200" />
                      </th>
                       <%} %>
                        <%else
                           { %>
                      <th>
                           Credits
                      </th>
                       <th>
                            <i class="far fa-star"></i>
                      </th>
                       <%} %>
                      <th>
                           <%= payments[i].Start_time %>
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
