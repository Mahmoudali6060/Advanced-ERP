

--Username: admin6060
--Password: Oppo@2020
--[1] Create Company


INSERT INTO [dbo].[Companies]
           ([ImageUrl]
           ,[Name]
           ,[AddressDetails]
           ,[ContactPerson]
           ,[ContactTelephone]
           ,[IsActive]
           ,[WebsiteLink])
     VALUES
           (
		   	N'معرض البواب_2023_11_10_19_43.jpg',
			N'معرض البواب	',
			N'مول مصر بدر',
		    N'د/خلاد',	
			'010256454545',
			1,
			'https://www.facebook.com/'
		   )
GO



--[2] Create AspNetUsers
INSERT INTO [dbo].[AspNetUsers]
           (Id
           , [UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount])
     VALUES
           ('a3a850a0-e9c6-4120-9c26-cd3379c49e8b',
		   'admin6060',
		   'ADMIN6060',
		   'admin6060@gmail.com',
		   'ADMIN6060@GMAIL.COM',
		   0,
		   'AQAAAAEAACcQAAAAEImIVSQp/ybaKQ7Eu4SUAJ657h+OBZDBrcvCZxXqAfHvzdE+UTp6RCX7BJEOVywWZQ==',
		   'UPYCMIKN2I2EFP2CEJLPQ3EG6RHKVV4C',
		   '4afbf078-637c-4116-aae7-a1ce5186647c',
		   NULL	,
		   0,	
		   0,
		   NULL,
		   1,
		   0
		   )
GO


--[3] Create Role
INSERT INTO [dbo].[RoleGroups]
           ([Name]
           ,[Description]
           ,[Created]
           ,[Modified]
           ,[IsActive]
           ,[CompanyId])
     VALUES
           (
		   	N'Default',
			N'Default for First Login',
			'2020-12-12 00:00:00.0000000',
			'2020-12-12 00:00:00.0000000',
			1,
			(Select top(1) Id from Companies)
		   )
GO


--[4] Create UserProfile
USE [ERP_Beta]
GO

INSERT INTO [dbo].[UserProfiles]
           ([IsActive]
           ,[FirstName]
           ,[LastName]
           ,[Mobile]
           ,[IsFirstLogin]
           ,[IsHide]
           ,[ImageUrl]
           ,[DefaultLanguage]
           ,[AppUserId]
           ,[CompanyId]
           ,[CityId]
           ,[CountryId]
           ,[StateId]
           ,[Created]
           ,[Modified]
           ,[RoleId])
     VALUES
           (
		   	1,
			N'Admin',
			N'Admin Second',
			null,	
			0,
			0,
			NULL,
			'ar',
			(Select top(1) Id from AspNetUsers),
			(Select top(1) Id from Companies),
			NULL,	
			NULL,	
			NULL,
			'0001-01-01 00:00:00.0000000',
			'2023-11-18 20:18:25.0463750',
			(Select top(1) Id from RoleGroups)
		   )
GO




