<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="seller.aspx.cs" Inherits="TechBid.seller" %>

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
    <% if (Request.Cookies["login"] != null ) { Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "FailureLoginAlert();", true); } %>
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
                        <input id="signin" class="input" type="password" placeholder="Password" name="password" required>
                        <a href="index.aspx"><i>Are You an Buyer Sign Up Here</i></a>
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
            <h2>Auction Sale For Free</h2>
            <p>Sell Your Products Through Auction</p>
            <form method="post" runat="server" class="signup">

                    <input class="input" type="text" placeholder="Name" name="name" required>
                    <input class="input" type="text" placeholder="username" name="username" required>
                    <input class="input" type="text" placeholder="Address" name="address" required>
                    <input id="password-fuck" class="input" type="password" placeholder="Choose Password" name="password" minlength="8" required>
                    <input class="input" type="email" placeholder="E-mail" name="email" required>
                    <input class="input" type="text" placeholder="Mobile Number" name="mobilenumber" maxlength="10" minlength="10" required>


                    <label>
                        <input type="checkbox" required>  I accept the Terms and Conditions and the Privacy Policy of TechBidder.com
                    </label>
                    <input class="button is-primary" type="submit" value="Create Account" runat="server" onserverclick="sellerSignup">

            </form>
        </div>

    </header>
</body>
</html>
