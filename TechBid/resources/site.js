function successSignUpAlert() {

        swal("Sign Up Sucessful!", "Please log in!", "success").then(function () {
        window.location = "index.aspx";
        });

}

function FailureSignUpAlert() {

    swal("Something went Wrong!", "Please try again!", "error").then(function () {
        window.location = "index.aspx";
    });

}

function FailureLoginAlert() {

    swal("Check Your Credentials!","Please try again!", "error").then(function () {
        window.location = "index.aspx";
    });

}
function PleaseLoginAlert() {

    swal("Please Log in", "", "error").then(function () {
        window.location = "index.aspx";
    });

} 

function PaymentSucessAlert() {

    swal("Payment Sucessful!", "Enjoy in Bidding!", "success").then(function () {
        window.location = "loginindex.aspx";
    });

}

function PaymentFailureAlert() {

    swal("Something went Wrong!", "Please try again!", "error").then(function () {
        window.location = "loginindex.aspx";
    });

}
function bidFailureAlert() {

    swal("Bid Intervel Expired!", "Come Back again!", "error").then(function () {
        window.location = "loginindex.aspx";
    });

}
function bidSucessAlert() {

    swal("Bidding Sucessful!", "Enjoy in Bidding!", "success").then(function () {
        window.location = "loginindex.aspx";
    });

}

function updationSucessAlert() {

    swal("Product Updated Sucessfully!", "Enjoy in Bidding!", "success").then(function () {
        window.location = "sellerindex.aspx";
    });

}
function removeSucessAlert() {

    swal("Product Removed Sucessfully!", "Enjoy in Bidding!", "success").then(function () {
        window.location = "sellerindex.aspx";
    });

}

function removeFailureAlert() {

    swal("Rebid Product Can not be removed!", "", "error").then(function () {
        window.location = "sellerindexindex.aspx";
    });

}

window.onload = function () {

    var selectors = '[id^="countdown"]';
    var ids = document.querySelectorAll('div[id^="countdown"]');
    ids.forEach(function (divid) {

        var productid = divid.id;
        //var productid = document.getElementById
        var time = document.getElementById(productid).innerText;
        var enddate = new Date(time);
        var x = setInterval(function () {

            // Get todays date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = enddate - now;

            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            // Output the result in an element with id="demo"
            document.getElementById(productid).innerHTML = days + "d " + hours + "h "
                + minutes + "m " + seconds + "s ";

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                window.location="index.aspx"
            }
        }, 1000);

    });   
}



function buycredits() {

    swal({
        title: "Buy Credits",
        content: {
            element: "input",
            attributes: {
                placeholder: "Number of Credits You want to buy",
                type: "text",
                required: true,
                name:"credits"
            },
        },
        buttons: {
            cancel: true,
            confirm: true
        }
    }).then(
        function (text) {
        if (text === "") {

            swal("You need to give the Number of credits!", "", "error");
            return false;
        }
        else if (text === null) {

            //swal("You need to give the Number of credits!", "", "error");
            return false;
        }
        else {

            window.location = 'payment.aspx?credits=' + text;
        }
        
    });
}

function checkcredits(credits,totalcredits,event) {

 
    if (credits > totalcredits) {

        swal("You Dont Have Enough Credits ", "Please Buy Some Credits!", "error");
        return false;
    }

    event.preventDefault();
    swal("Do you want to bid on this product", {
        buttons: {
            cancel: true,
            confirm: true,
        },
    }).then(function (isConfirm) {

        if (isConfirm) {
            event.target.submit();
        }
        return false;

    });

}


function checkdigits(event) {

    var x = document.getElementById("pincode").value;
    var y = document.getElementById("phone").value;
    if (isNaN(x) || isNaN(y)) {

        document.getElementById("pincode").value = " ";
        document.getElementById("phone").value = " ";
        swal("Only Numbers Are Allowed!", "", "error");
    }    
}


function checkdigit(event) {

    var x = document.getElementById("price").value;
    var y = document.getElementById("bid").value;
    if (isNaN(x) || isNaN(y)) {

        document.getElementById("price").value = " ";
        document.getElementById("bid").value = " ";
        swal("Only Numbers Are Allowed!", "", "error");
    }
}


