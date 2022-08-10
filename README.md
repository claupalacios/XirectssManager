# XirectssManager
# Project

Manager service to process users and accounts data.
This project has two microservices and Unit tests for each one.

# AccountMS:

  * **GetAccountsByUserId**: Get accounts searching by user id.
  * **AddAccount** Add new accounts to specific user with default **Inactive State** 
  * **UpdateAccountState** Update Active field of a specific Account
  * The service will log into "C:\Logs\AccountMS.log"

# UserMS:

  * **GetAllActiveUsers**: Get list of users with state Active
  * **AddUser**: Add a new user with default state **Active**
  * **UpdateUserState**: Update Active field of a specific User
  * The service will log into "C:\Logs\UserMS.log"
  
# Startup

  * To initiate the application you must Run the solution pointing to one Microservice, then you need to Right Click in the other one and select **Debug** and **Start new Instance**
  

# Bug :'(

  * On the **UpdateAccountState** endpoint, the account cannot change to Active status if the user who owns the account is not Active. However, the endpoint responds that the account has been modified when in fact it has not.

# Notes
  * Has Swagger incorporated
