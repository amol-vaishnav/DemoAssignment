USE [DemoApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_Register]    Script Date: 3/23/2020 3:19:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Proc_Register](@RegID nvarchar(50)=null,
        @FirstName nvarchar(50),
        @LastName nvarchar(50),
		@Address nvarchar(200),
        @UserName nvarchar(50),
        @Password nvarchar(500),@ProfilePhoto varbinary(max)=null)
as
Begin
DECLARE @AutoID nvarchar(50);

exec ProcCreateAutoID  @tableName='tbl_registration',@ColName='RegID',@Initial='RG',@returnResult=@AutoID output

Insert Into tbl_registration values(@AutoID,@FirstName,@LastName,@Address,@UserName,@Password,@ProfilePhoto);

end;

GO
/****** Object:  StoredProcedure [dbo].[ProcCreateAutoID]    Script Date: 3/23/2020 3:19:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ProcCreateAutoID]
(
   @tableName as varchar(200),@ColName as varchar(200),@Initial as varchar(200),
  
   @returnResult as Varchar(200) OUTPUT
)
-- or whatever length you need
AS
BEGIN
 DECLARE @ret nvarchar(20);

 DECLARE @SQLSTR nvarchar(1000);
   set @SQLSTR ='SELECT ''' + @Initial +  ''' + right(''00000'' + Cast(Substring(ISNULL(MAX(' +@ColName+'),0),Len(MAX('+@ColName+'))-5,8) +1 as Varchar ),6)   FROM '+ @tableName ;
	
   Delete from TempReturn;
   insert into TempReturn execute (@SQLSTR);
  
 SELECT @returnResult = isnull(ReturnVal,@Initial +'000001') FROM TempReturn; 

END

GO
/****** Object:  Table [dbo].[tbl_Registration]    Script Date: 3/23/2020 3:19:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Registration](
	[RegID] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](800) NULL,
	[ProfilePhoto] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempReturn]    Script Date: 3/23/2020 3:19:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempReturn](
	[ReturnVal] [nvarchar](200) NULL
) ON [PRIMARY]

GO
