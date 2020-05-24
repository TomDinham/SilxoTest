const path = require('path');
var express = require('express');
var app = express();
// app.get('/', function (req, res) {
//   res.sendfile(__dirname+"/index.html");
// });
const port = process.env.PORT||3000;
app.use(express.static(__dirname+ "/public"));
app.get("/",function(req,res)
{
    res.sendFile(path.join(__dirname+"/index.html"));
});


app.listen(port);
  

