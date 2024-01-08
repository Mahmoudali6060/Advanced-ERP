--->>>>8_1_2024 --- Published:False

--Make Product Name unique
ALTER TABLE Products ALTER COLUMN Name nvarchar(500)

ALTER TABLE Products
ADD CONSTRAINT UQ_product_name UNIQUE(name);

---Make PaymentMethodId Allow null
ALTER TABLE Treasuries ALTER COLUMN PaymentMethodId INT NULL
---Add Privileges

INSERT INTO [dbo].[RolePrivileges]
           ([PrivilegeId]
           ,[RoleGroupId]
           ,[IsActive]
           ,[Created]
           ,[Modified])
     VALUES
           (53
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		   (54
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		    (55
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		     (56
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		     (58
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		     (59
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		     (60
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024'),

		     (61
           ,1
           ,1
           ,'08/01/2024'
           ,'08/01/2024')


GO




