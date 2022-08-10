# XirectssManager
# Project

Manager service to process users and accounts data.
This project has two microservices and Unit tests for each one.
The two microservices are "connected" through Triggers, although both are in the same instance but different Database.


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
  
# Installation and Startup
  * First you need to create the Databases and change the **appsettings.json** file to point to your local bases.
  * Then you need to create the Triggers listed below in each database.
  * To initiate the application you must Run the solution pointing to one Microservice, then you need to Right Click in the other one and select **Debug** and **Start new Instance**
  
# Triggers
# Account
```
USE [AccountDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[DeleteAccountIfDoesntMatchWithUser]
   ON  [dbo].[Account]
   AFTER INSERT AS 
BEGIN
    SET NOCOUNT ON;

	DECLARE @idUser int = (select UserId from inserted)
	DECLARE @idUserExist int = null;

	set @idUserExist = (SELECT Id from [UserDB].dbo.[User] where Id = @idUser)

	IF @idUserExist is null
	Begin
		DELETE from [AccountDB].dbo.Account
		WHERE UserId = @idUser
    End

END
```
------------------------
```
USE [AccountDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[DontActivateAccountIfUserIsNotActive]
   ON  [dbo].[Account]
   AFTER UPDATE AS 
BEGIN
    SET NOCOUNT ON;

	DECLARE @activateAccount bit = (select Active from inserted)
	IF @activateAccount = 1
		BEGIN
		DECLARE @idUser int = (select UserId from inserted)
		DECLARE @idAccount int = (select Id from inserted)
		DECLARE @isUserActive bit = (select Active from [UserDB].dbo.[User] where Id = @idUser)

		IF @isUserActive = 0		
			BEGIN
				UPDATE [AccountDB].dbo.Account
				SET Active = 0
				WHERE Id = @idAccount
			END
		END
END
```

# User
```
USE [UserDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[InsertAccountForNewUser]
   ON  [dbo].[User]
   AFTER INSERT AS 
BEGIN
    SET NOCOUNT ON;
	DECLARE @dateNow Datetime = GETDATE();
	DECLARE @idUser int = (select Id from inserted)

    INSERT INTO [AccountDB].dbo.Account
	VALUES (@dateNow,0,@idUser)

END
```
------------------------
```
USE [UserDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[SetAccountActiveFalseWhenUserNoLongerActive]
   ON  [dbo].[User]
   AFTER UPDATE AS 
BEGIN
    SET NOCOUNT ON;

	DECLARE @isActive bit = (select Active from inserted)
	IF @isActive = 1
	Begin
          Return
    End
	DECLARE @idUser int = (select Id from inserted)
	UPDATE [AccountDB].dbo.Account
	SET Active = 0
	WHERE UserId = @idUser

END
```

# Bug :'(

  * On the **UpdateAccountState** endpoint, the account cannot change to Active status if the user who owns the account is not Active. However, the endpoint responds that the account has been modified when in fact it has not.

# Notes
  * Has Swagger incorporated
