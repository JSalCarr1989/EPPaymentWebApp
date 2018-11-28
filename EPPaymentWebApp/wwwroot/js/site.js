// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {


    $("#requestPaymentForm").submit(function (e) {


       
        $.ajax({
            url: 'https://localhost:8443/api/Hash',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({
                    paymentOrder: $('#mp_order').val(),
                    paymentReference: $('#mp_reference').val(),
                    paymentAmount: $('#mp_amount').val()
                }),
            success: function (response) {
                
                $('input[name="mp_signature"]').val(response.hash);

              var  requestData = {
                    MpAccount: $('#mp_account').val(),
                    MpProduct: $('#mp_product').val(),
                    MpOrder: $('#mp_order').val(),
                    MpReference: $('#mp_reference').val(),
                    MpNode: $('#mp_node').val(),
                    MpConcept: $('#mp_concept').val(),
                    MpAmount: $('#mp_amount').val(),
                    MpCustomerName: $('#mp_customername').val(),
                    MpCurrency: $('#mp_currency').val(),
                    MpSignature: response.hash,
                    MpUrlSuccess: $('#mp_urlsuccess').val(),
                    MpUrlFailure: $('#mp_urlfailure').val(),
                    MpRegisterSb: $('#mp_registersb').val(),
                    BeginPaymentId: $('#BeginPaymentId').val()
                }


        $.ajax({
            url: 'https://localhost:8443/api/RequestPayment',
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
                return true;
            }
        })

            }
        })


        return true;
    })

 

})


