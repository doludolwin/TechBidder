<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderhistory.aspx.cs" Inherits="TechBid.orderhistory" %>

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
        <%if (orders.Count == 0)
            { %>
             <div id="bid_history">
                <h2>No Order History</h2>
            </div>
        <%} %>
        <%else
    { %>
        <div id="bid_history">
            <h2>Order History</h2>
        </div>
           <table class="table is-bordered is-striped is-fullwidth is-hoverable">
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Name</th>
                        <th>Payment Amount</th>
                        <th>Payment Date</th>
                    </tr>
                </thead>
               <tbody>
                    <%for (int i = 0; i < orders.Count; i++)
                    {%>
                   <tr>
                       <th>
                            <img src="<%= "./Uploads/" + orders[i].Photo %>" width="200" height="200" />
                       </th>
                       <th>
                            <%= orders[i].Name %>
                       </th>
                       <th>
                            <%= orders[i].Credits %>
                       </th>
                       <th>
                           <%= orders[i].Start_time %>
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

