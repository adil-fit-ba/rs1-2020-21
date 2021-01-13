// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var connection = new signalR.HubConnectionBuilder().withUrl("/myhub").build();

connection.on("prijemPoruke", function (user, message) {
    porukaWarning("hello " + user + ": " + message);
});

connection.start().then(function () {
    console.info("started signalR hub");

}).catch(function (err) {
    return console.error(err.toString());
});