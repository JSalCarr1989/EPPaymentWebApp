﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {


    $('input[name="mp_amount"]').keyup(function () {

        if ($(this).val().indexOf('.') != -1) {
            if ($(this).val().split(".")[1].length > 2) {
                if (isNaN(parseFloat(this.value))) return;
                this.value = parseFloat(this.value).toFixed(2);
            }
        }
        return this;
    })

    $("#requestPaymentForm").submit(function (e) {


        
        
            $.ajax({
                async:false,
                url: 'https://100.125.0.119:8443/api/values',
                type: 'GET',
                dataType: 'json',
                success: function (res) {
                    console.log(res);
                    $.ajax({
                        async:false,
                        url: 'https://100.125.0.119:8443/api/Hash',
                        type: 'post',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({
                            'paymentOrder': $('#mp_order').val(),
                            'paymentReference': $('#mp_reference').val(),
                            'paymentAmount': $('#mp_amount').val()
                        }),
                        success: function (response) {



                            console.log(response);
                            

                            
                            $('input[name="mp_signature"]').val(response.hash);

                            var requestData = {
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

                            console.log(requestData);
                            $.ajax({
                                url: 'https://100.125.0.119:8443/api/RequestPayment/',
                                async:false,
                                type: 'post',
                                dataType: 'json',
                                contentType: 'application/json',
                                data: JSON.stringify(requestData),
                                statusCode: {

                                    201: function () {
                                        console.log("it was a 201");
                                    }

                                },
                                success: function (response) {

                                },
                                error: function (request, status, errorThrown) {
                                    console.log("the server says" + request.status);
                                }
                            })


                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log("in error section");
                            console.log("jqXHR: " + jqXHR);
                            console.log("jqXHR.responseText: " + jqXHR.responseText);
                            console.log("textStatus: " + textStatus);
                            console.log("errorThrown: " + errorThrown);
                            data = JSON.stringify(jqXHR.responseText);
                            console.log("parseJSON data: " + data);
                        }
                    })
                }
            })

        

        return true;
        
    })

 

})


