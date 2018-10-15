<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="TechBid.payment" %>

<!DOCTYPE html>
<html lang="en" >

<head>
  <meta charset="UTF-8">
  <title>Payment card checkout</title>
  <link href="https://fonts.googleapis.com/icon?family=Material+Icons"
      rel="stylesheet">
      <link rel="stylesheet" href="./resources/paymentstyle.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
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
                <h2><span>TechBidder</span></h2>
            </div>

            <div class="login">
                <div class="input-wrapper" style="width: 200px">
                     <!-- <input class="button is-primary is-rounded" type="submit" value="You have 10 credits" style="width:200px"> -->
                </div>
                  <div class="input-wrapper" id="creditbuy" style="width: 200px">
                       
                           <!-- <input class="button is-primary is-rounded" type="submit" value="buy credits" style="width:200px" onclick="buycredits();"> -->
                       
                    </div>

                <div class="input-wrapper" style="width: 80px">
                    <div class="dropdown">
                        <input class="button is-primary" type="submit" value=<%= name %>  style="width: 150px">
                       <div class="dropdown-content is-rounded">
                        <a href="bidhistory.aspx">Bid History</a>
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



 <!-- <div class="tips">
Payment card number: (4) VISA, (51 -> 55) MasterCard, (36-38-39) DinersClub, (34-37) American Express, (65) Discover, (5019) dankort
</div>
    -->
<div class="container">
  <div class="col1">
    <div class="card">
      <div class="front">
        <div class="type">
          <img class="bankid"/>
        </div>
        <span class="chip"></span>
        <span class="card_number">&#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; </span>
        <div class="date"><span class="date_value">MM / YYYY</span></div>
        <span class="fullname">FULL NAME</span>
      </div>
      <div class="back">
        <div class="magnetic"></div>
        <div class="bar"></div>
        <span class="seccode">&#x25CF;&#x25CF;&#x25CF;</span>
        <span class="chip"></span><span class="disclaimer">This card is property of Random Bank of Random corporation. <br> If found please return to Random Bank of Random corporation - 21968 Paris, Verdi Street, 34 </span>
      </div>
    </div>
  </div>
  <div class="col2">
    <form method="post" runat="server">
        <label>Card Number</label>
        <input class="number" type="text" ng-model="ncard" maxlength="19" minlength="19" name="cardnumber" onkeypress='return event.charCode >= 48 && event.charCode <= 57' required/>
        <label>Cardholder name</label>
        <input class="inputname" type="text" name="cardholder" placeholder="Card Holder" required/>
        <label>Expiry date</label>
        <input class="expire" type="text" name="expirydate" placeholder="MM / YYYY" maxlength="7" minlength="7" required/>
        <label>Security Number</label>
        <input class="ccv" type="text" placeholder="CVC" name="cvv" maxlength="3" minlength="3" onkeypress='return event.charCode >= 48 && event.charCode <= 57' required/>
        <%if (paymentamount != null && nocredits != null) {%>
        <input type="hidden" name="amount" value=<%= paymentamount %> />
        <input type="hidden" name="credits" value=<%= nocredits %> />
        <button class="buy" type="submit" runat="server" onserverclick="creditpayment"><i class="material-icons">lock</i> Pay <%= paymentamount %> <i class="fas fa-rupee-sign"></i></button>
        <%} %>
        <%if (amount != null && productid != null ){%>
        <input type="hidden" name="payamount" value=<%= amount %> />
        <input type="hidden" name="productid" value=<%= productid %> />
        <button class="buy" type="submit" runat="server" onserverclick="productpayment"><i class="material-icons">lock</i> Pay <%= amount %> <i class="fas fa-rupee-sign"></i></button>
        <%} %>
    </form>
  </div>
</div>
  <script src='https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular.min.js'></script>
<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js'></script>
<script src='https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js'></script>
    <script  src="./resources/index.js"></script>

</body>

</html>
