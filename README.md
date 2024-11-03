**Script-Migration query is also available:**

```IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Movie] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [ReleaseData] datetime2 NOT NULL,
    [Genre] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [Rating] int NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241004205042_IntitialCreate', N'8.0.8');
GO

COMMIT;
GO

```
