﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {


    $("#requestPaymentForm").submit(function (e) {


        var hash = CryptoJS.HmacSHA256($('#mp_order').val() + $('#mp_reference') + $('#mp_amount').val(), "secret");
        var hashInBase64 = CryptoJS.enc.Base64.stringify(hash);
        


        var requestData = {
            MpAccount:$('#mp_account').val(),
            MpProduct: $('#mp_product').val(),
            MpOrder: $('#mp_order').val(),
            MpReference: $('#mp_reference').val(),
            MpNode: $('#mp_node').val(),
            MpConcept: $('#mp_concept').val(),
            MpAmount: $('#mp_amount').val(),
            MpCustomerName: $('#mp_customername').val(),
            MpCurrency: $('#mp_currency').val(),
            MpSignature: hashInBase64,
            MpUrlSuccess: $('#mp_urlsuccess').val(),
            MpUrlFailure: $('#mp_urlfailure').val(),
            MpRegisterSb: $('#mp_registersb').val(),
            BeginPaymentId: $('#BeginPaymentId').val()
        }


        console.log(JSON.stringify(requestData));

      

        $.ajax({
            url: 'https://localhost:5001/api/RequestPayment',
            type: 'post',
            dataType: 'json',
            contentType:'application/json',
            data: JSON.stringify(requestData),
            statusCode: {

                201: function () {
                    console.log("it was a 201");
                }

            },
            success: function (response) {
                console.log(response);
            }
        })

        return false;
    })


})