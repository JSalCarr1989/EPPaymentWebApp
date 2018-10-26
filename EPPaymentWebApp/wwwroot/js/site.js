// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {


    $("#requestPaymentForm").submit(function (e) {



        var requestData = {
            mpaccount:$('#mp_account').val(),
            mpproduct: $('#mp_product').val(),
            mporder: $('#mp_order').val(),
            mpreference: $('#mp_reference').val(),
            mpnode: $('#mp_node').val(),
            mpconcept: $('#mp_concept').val(),
            mpamount: $('#mp_amount').val(),
            mpcustomername: $('#mp_customername').val(),
            mpcurrency: $('#mp_currency').val(),
            mpsignature: $('#mp_signature').val(),
            mpurlsuccess: $('#mp_urlsuccess').val(),
            mpurlfailure: $('#mp_urlfailure').val(),
            mpregistersb: $('#mp_registersb').val()
        }


        console.log(requestData);


        return true;
    })


})