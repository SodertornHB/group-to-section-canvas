/* 
 IMPORTANT! Before running this script you need to create a new database  
 */
if not exists (
    select distinct 1
    from information_schema.columns
    where table_name = 'Migration'
) begin CREATE TABLE [dbo].[Migration](
    [Id] [int] IDENTITY(1, 1) NOT NULL,
    [ClientVersion] [nvarchar](200) NULL,
    [DatabaseVersion] [nvarchar](200) NULL,
    [CreatedOn] [datetime] NULL,
    CONSTRAINT [PK_Migrations] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
insert [Migration] ([ClientVersion], [DatabaseVersion], [CreatedOn])
values ('1.0.0', '1.0.0', getdate())
end
go if not exists (
        select distinct 1
        from information_schema.columns
        where table_name = 'Log'
    ) begin CREATE TABLE [dbo].[Log](
        [Id] [int] IDENTITY(1, 1) NOT NULL,
        [Origin] [nvarchar](2000) NULL,
        [Message] [nvarchar](2000) NULL,
        [LogLevel] [nvarchar](2000) NULL,
        [CreatedOn] [datetime] NULL,
        [Exception] [nvarchar](4000) NULL,
        [Trace] [nvarchar](4000) NULL,
        CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
    ) ON [PRIMARY]
end
go IF EXISTS (
        SELECT 1
        FROM information_schema.routines
        WHERE routine_name = 'GetLogsByDatetime'
            AND routine_type = 'PROCEDURE'
    ) BEGIN DROP PROCEDURE GetLogsByDatetime;
END
GO -- =====================================================================================================================================================
    -- Description: Retrieves logs within a specified window of minutes around a given datetime and filtered by a log level.
    -- Parameters:  
    --    @datetimeString: An NVARCHAR(20) input parameter, specifies the central datetime in ISO 8601 format (YYYY-MM-DDTHH:MM:SS).
    --    @windowInMinutes: An INT input parameter, specifies the number of minutes before and after the @datetimeString to include in the result set.
    --    @level: An NVARCHAR(256) input parameter, specifies the log level to filter the logs. If it is an empty string, all log levels will be included.
    -- Example usage:
    --    EXEC GetLogsByDatetime '2023-05-29T09:55:30', 10, 'error';
    -- =====================================================================================================================================================
    CREATE PROCEDURE GetLogsByDatetime @datetimeString NVARCHAR(20),
    @windowInMinutes INT,
    @level NVARCHAR(256) AS BEGIN -- Validate and parse the input datetime string
DECLARE @datetime DATETIME;
BEGIN TRY
SET @datetime = CONVERT(DATETIME, @datetimeString, 126);
END TRY BEGIN CATCH RAISERROR(
    'Invalid datetime format. Expected ISO 8601 format: YYYY-MM-DDTHH:MM:SS.',
    16,
    1
);
RETURN;
END CATCH;
DECLARE @startDatetime DATETIME;
SET @startDatetime = DATEADD(MINUTE, - @windowInMinutes, @datetime);
DECLARE @endDatetime DATETIME;
SET @endDatetime = DATEADD(MINUTE, @windowInMinutes, @datetime);
SELECT *
FROM [dbo].[Log]
WHERE CreatedOn BETWEEN @startDatetime AND @endDatetime
    AND (
        @level = ''
        OR LogLevel LIKE @level
    );
END
GO