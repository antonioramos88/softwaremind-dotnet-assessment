// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#addEmployeeButton").on("click", function () {
        debugger;
        let newEmployee = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            address: $("#address").val(),
            phoneNumber: $("#phone").val()
        };
        $.post('http://localhost:5203/Employees/CreateEmployee', newEmployee, function () {
            $('#newEmployeeModal').modal('hide');
            document.location.reload();
        });
    });
});