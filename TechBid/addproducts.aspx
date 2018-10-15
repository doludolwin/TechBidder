<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addproducts.aspx.cs" Inherits="TechBid.addproducts" %>

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
       <script>
        function producthistory() {

            window.location = "sellerproduct.aspx";
        }
    </script>
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
                <div class="input-wrapper" style="width: 200px">
                     <%--<input class="button is-primary is-rounded" type="submit" onclick="addproducts();" value="prduct for bid" style="width:200px">--%>
                </div>
                <div class="input-wrapper" style="width: 200px">
                    <input class="button is-primary is-rounded" type="submit" value="Product History" onclick="producthistory();" style="width:200px">
                </div>


                <div class="input-wrapper" style="width: 80px">
                    <div class="dropdown">
                           <input class="button is-primary" type="submit" value="Sony"  style="width: 150px">
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

    <!-- header markup -->
    <header>

        <div id="signup-wrapper">
            <h2>Register Products For Bid</h2>
            <p>it takes less than 30 seconds.</p>
            <form method="post" runat="server" class="signup" enctype="multipart/form-data" name="f1" onsubmit=" return productvalidate()">

                         <div class="file input"  id="password-fuck">
                          <label class="file-label">
                            <input class="file-input" type="file" name="productimage" required>
                            <span class="file-cta">
                              <span class="file-icon">
                                <i class="fas fa-upload"></i>
                              </span>
                              <span class="file-label">
                                Product picture
                              </span>
                            </span>
                          </label>
                        </div>
                    <input class="input" type="text" placeholder="Name" name="name" required>
                    <input class="input" type="text" placeholder="Starting Price" name="price" id="price" onfocusout="checkdigit(this)" required>
                    <input class="input" type="datetime-local" id="password-fuck" placeholder="Bid Date And Time in 24 hour" name="biddatetime" min="<%=DateTime.Now.ToString("yyyy-MM-dd HH:MM").Replace(" ","T") %>" required>
                    <input class="input" type="text" placeholder="Bid Time In Minutes" name="bidtime" id="bid" onfocusout="checkdigit(this)" required>                  
                    <textarea class="input textarea" id="password-fuck" placeholder="Description" name="description" rows="5" required></textarea>
                    <input class="button is-primary" type="submit" value="Add Products" runat="server" onserverclick="insertProducts">

            </form>
        </div>

    </header>
</body>
</html>
