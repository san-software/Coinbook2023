
using System;
using PayPal.Api;
using System.Collections.Generic;
using PayPal.Sample.Utilities;

namespace Coinbook
{
	// #CreatePayment using credit card Sample
	// This sample code demonstrate how you can process
	// a payment with a credit card.
	// API used: /v1/payments/payment

	namespace PayPal.Sample
	{
		public partial class PaymentWithCreditCard : BaseSamplePage
		{
			protected override void RunSample()
			{
				// ### Api Context
				// Pass in a `APIContext` object to authenticate 
				// the call and to send a unique request id 
				// (that ensures idempotency). The SDK generates
				// a request id if you do not pass one explicitly. 
				// See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
				var apiContext = Configuration.GetAPIContext();

				// A transaction defines the contract of a payment - what is the payment for and who is fulfilling it. 
				var transaction = new Transaction()
				{
					amount = new Amount()
					{
						currency = "USD",
						total = "7",
						details = new Details()
						{
							shipping = "1",
							subtotal = "5",
							tax = "1"
						}
					},
					description = "This is the payment transaction description.",
					item_list = new ItemList()
					{
						items = new List<Item>()
										{
												new Item()
												{
														name = "Item Name",
														currency = "USD",
														price = "1",
														quantity = "5",
														sku = "sku"
												}
										},
						shipping_address = new ShippingAddress
						{
							city = "Johnstown",
							country_code = "US",
							line1 = "52 N Main ST",
							postal_code = "43210",
							state = "OH",
							recipient_name = "Joe Buyer"
						}
					},
					invoice_number = Common.GetRandomInvoiceNumber()
				};

				// A resource representing a Payer that funds a payment.
				var payer = new Payer()
				{
					payment_method = "credit_card",
					funding_instruments = new List<FundingInstrument>()
								{
										new FundingInstrument()
										{
												credit_card = new CreditCard()
												{
														billing_address = new Address()
														{
																city = "Johnstown",
																country_code = "US",
																line1 = "52 N Main ST",
																postal_code = "43210",
																state = "OH"
														},
														cvv2 = "874",
														expire_month = 11,
														expire_year = 2018,
														first_name = "Joe",
														last_name = "Shopper",
														number = "4877274905927862",
														type = "visa"
												}
										}
								},
					payer_info = new PayerInfo
					{
						email = "test@email.com"
					}
				};

				// A Payment resource; create one using the above types and intent as `sale` or `authorize`
				var payment = new Payment()
				{
					intent = "sale",
					payer = payer,
					transactions = new List<Transaction>() { transaction }
				};

				// ^ Ignore workflow code segment
				#region Track Workflow
				this.flow.AddNewRequest("Create credit card payment", payment);
				#endregion

				// Create a payment using a valid APIContext
				var createdPayment = payment.Create(apiContext);

				// ^ Ignore workflow code segment
				#region Track Workflow
				this.flow.RecordResponse(createdPayment);
				#endregion

				// For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
			}

			namespace GenerateCodeNVP
		{
			public class DoDirectPayment
			{
				public DoDirectPayment()
				{
				}
				public string DoDirectPaymentCode(string paymentAction, string amount, string creditCardType, string creditCardNumber, string expdate_month, string cvv2Number, string firstName, string lastName, string address1, string city, string state, string zip, string countryCode, string currencyCode)
				{
					NVPCallerServices caller = new NVPCallerServices();
					IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
					/*
					 WARNING: Do not embed plaintext credentials in your application code.
					 Doing so is insecure and against best practices.
					 Your API credentials must be handled securely. Please consider
					 encrypting them for use in any production environment, and ensure
					 that only authorized individuals may view or modify them.
					 */

					// Set up your API credentials, PayPal end point, API operation and version.
					profile.APIUsername = "sdk-three_api1.sdk.com";
					profile.APIPassword = "QFZCWN5HZM8VBG7Q";
					profile.APISignature = "AVGidzoSQiGWu.lGj3z15HLczXaaAcK6imHawrjefqgclVwBe8imgCHZ";
					profile.Environment = "sandbox";
					caller.APIProfile = profile;

					NVPCodec encoder = new NVPCodec();
					encoder["VERSION"] = "51.0";
					encoder["METHOD"] = "DoDirectPayment";

					// Add request-specific fields to the request.
					encoder["PAYMENTACTION"] = paymentAction;
					encoder["AMT"] = amount;
					encoder["CREDITCARDTYPE"] = creditCardType;
					encoder["ACCT"] = creditCardNumber;
					encoder["EXPDATE"] = expdate_month;
					encoder["CVV2"] = cvv2Number;
					encoder["FIRSTNAME"] = firstName;
					encoder["LASTNAME"] = lastName;
					encoder["STREET"] = address1;
					encoder["CITY"] = city;
					encoder["STATE"] = state;
					encoder["ZIP"] = zip;
					encoder["COUNTRYCODE"] = countryCode;
					encoder["CURRENCYCODE"] = currencyCode;

					// Execute the API operation and obtain the response.
					string pStrrequestforNvp = encoder.Encode();
					string pStresponsenvp = caller.Call(pStrrequestforNvp);

					NVPCodec decoder = new NVPCodec();
					decoder.Decode(pStresponsenvp);
					return decoder["ACK"];

				}
			}
		}
	}
	}