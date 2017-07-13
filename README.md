# ATag

ATag is a very simple .NET Standard library to help you specifying tags for your systems' entities.

# How to use?

Install the nuget package:

```
Install-Package ATag.EntityFrameworkCore -Pre
```

Initialize database with this script:

``` SQL
CREATE TABLE [dbo].[Tag] (
    [Id] int NOT NULL IDENTITY,
    [CreatedByUserId] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    [ModifiedByUserId] int,
    [DateModified] datetime2,
    [Name] varchar(30),
    [OwnerId] varchar(30),
    [OwnerType] int NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [dbo].[TaggedEntity] (
    [Id] int NOT NULL IDENTITY,
    [CreatedByUserId] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [EntityKey] varchar(20),
    [EntityType] varchar(30),
    [TagId] int NOT NULL,
    CONSTRAINT [PK_TaggedEntity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaggedEntity_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [dbo].[TagComment] (
    [Id] int NOT NULL,
    [Comment] nvarchar(1000),
    [CreatedByUserId] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [ModifiedByUserId] int,
    [DateModified] datetime2,
    CONSTRAINT [PK_TagComment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TagComment_TaggedEntity_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[TaggedEntity] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Tag_OwnerType_OwnerId] ON [dbo].[Tag] ([OwnerType], [OwnerId]);

GO

CREATE INDEX [IX_TaggedEntity_TagId] ON [dbo].[TaggedEntity] ([TagId]);

GO

CREATE INDEX [IX_TaggedEntity_EntityType_EntityKey_TagId] ON [dbo].[TaggedEntity] ([EntityType], [EntityKey], [TagId]);
```

