// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {


    $("#requestPaymentForm").submit(function (e) {



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
            MpSignature: $('#mp_signature').val(),
            MpUrlSuccess: $('#mp_urlsuccess').val(),
            MpUrlFailure: $('#mp_urlfailure').val(),
            MpRegisterSb: $('#mp_registersb').val()
        }


        //console.log(JSON.stringify(requestData));

      

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

        return true;
    })


})