<%@ Page Title="Contact" Language="C#"  AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="TechBid.Contact" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>TechBid | Home</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.1/css/bulma.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Lobster" rel="stylesheet">
    <link rel="stylesheet" href="./resources/style.css">
    <script type="text/javascript">
        function successalert() {
                        swal({
                title: "Wow!",
                text: "Message!",
                type: "success"
            }).then(function() {
                window.location = "index.aspx";
            });
        }

    </script>
</head>
    <body>
    
 </body>
</html>

