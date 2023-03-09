# ServerlessPaymentGateway

## Project Structure

* Payment Gateway APIs
* Bank Simulator AWS lambda Function with DynamoDB
* Identity Server for authentication

## Assumptions
Data is stored on the Bank Simulator Lambda Function in DynamoDB (unsure of the compliance)

## Steps to Run

1. Make sure all the API and IdentityServer projects are set as startup projects
2. Use swagger for documentation and postman to send requests
3. Get token which is set for subsequent request authorization
4. Invoke other APIs

## Test Data

#### Source: https://www.checkout.com/docs/testing/response-code-testing

### Invalid Cards stored in DynamoDB

| Card Number | Status |   
| :-------------:| :----------:|
| 4024007103573027 | Declined |
| 4544249167673670 | Declined |
| 5279988405398834 | Declined |
| 5328090869100177 | Declined |
| 4095254802642505 | Expired |
| 4897453568485113 | Declined |
| 5420951756276171 | Declined |

<Any 16 digit card> : Paid 


#### Response Codes

| Amount        | Response         
| ------------- |:-------------:| 
| 200 | Success |
| 201 | Payment Success |
| 422 | Payment Failed |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

#### To Dos:
Tokenize the request

Get all transactions within a date range, sorted by success/failure

Time out a transaction after an interval

Add Docker support
