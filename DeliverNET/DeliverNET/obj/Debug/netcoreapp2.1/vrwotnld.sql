IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [DOB] datetime2 NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Businesses] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Long] float NOT NULL,
    [Lat] float NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [SignupDate] datetime2 NOT NULL,
    [VerificationDate] datetime2 NOT NULL,
    [IsVerified] bit NOT NULL,
    [Credentials] nvarchar(max) NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Businesses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Deliverer] (
    [IsValidated] bit NOT NULL,
    [Credentials] nvarchar(max) NULL,
    [IsWorking] bit NOT NULL,
    [IsDelivering] bit NOT NULL,
    [OperationalRegion] nvarchar(max) NULL,
    [Long] float NOT NULL,
    [Lat] float NOT NULL,
    [DeliverNetUserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Deliverer] PRIMARY KEY ([DeliverNetUserId]),
    CONSTRAINT [FK_Deliverer_AspNetUsers_DeliverNetUserId] FOREIGN KEY ([DeliverNetUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Ratings] (
    [Id] uniqueidentifier NOT NULL,
    [Tstamp] datetime2 NOT NULL,
    [Rater] nvarchar(max) NULL,
    [RateeId] nvarchar(450) NULL,
    [Comment] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ratings_AspNetUsers_RateeId] FOREIGN KEY ([RateeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BusinessCashiers] (
    [DeliverNetUserId] nvarchar(450) NOT NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_BusinessCashiers] PRIMARY KEY ([DeliverNetUserId]),
    CONSTRAINT [FK_BusinessCashiers_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BusinessCashiers_AspNetUsers_DeliverNetUserId] FOREIGN KEY ([DeliverNetUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BusinessOwners] (
    [DeliverNetUserId] nvarchar(450) NOT NULL,
    [BusinessId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_BusinessOwners] PRIMARY KEY ([DeliverNetUserId]),
    CONSTRAINT [FK_BusinessOwners_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BusinessOwners_AspNetUsers_DeliverNetUserId] FOREIGN KEY ([DeliverNetUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [BusinessId] uniqueidentifier NULL,
    [CashierDeliverNetUserId] nvarchar(450) NULL,
    [DelivererDeliverNetUserId] nvarchar(450) NULL,
    [Tstamp] datetime2 NOT NULL,
    [Address] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [StateProvince] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [Long] float NOT NULL,
    [Lat] float NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [FloorNo] int NOT NULL,
    [DoorName] nvarchar(max) NULL,
    [PaymentTYpeId] int NOT NULL,
    [Tariff] real NOT NULL,
    [Price] real NOT NULL,
    [Comments] nvarchar(max) NULL,
    [AcceptedTime] datetime2 NOT NULL,
    [PickupTime] datetime2 NOT NULL,
    [DeliveredTime] datetime2 NOT NULL,
    [IsAccepted] bit NOT NULL,
    [IsPickedup] bit NOT NULL,
    [IsDelivered] bit NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_BusinessCashiers_CashierDeliverNetUserId] FOREIGN KEY ([CashierDeliverNetUserId]) REFERENCES [BusinessCashiers] ([DeliverNetUserId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Deliverer_DelivererDeliverNetUserId] FOREIGN KEY ([DelivererDeliverNetUserId]) REFERENCES [Deliverer] ([DeliverNetUserId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_BusinessCashiers_BusinessId] ON [BusinessCashiers] ([BusinessId]);

GO

CREATE UNIQUE INDEX [IX_BusinessOwners_BusinessId] ON [BusinessOwners] ([BusinessId]);

GO

CREATE INDEX [IX_Orders_BusinessId] ON [Orders] ([BusinessId]);

GO

CREATE INDEX [IX_Orders_CashierDeliverNetUserId] ON [Orders] ([CashierDeliverNetUserId]);

GO

CREATE INDEX [IX_Orders_DelivererDeliverNetUserId] ON [Orders] ([DelivererDeliverNetUserId]);

GO

CREATE INDEX [IX_Ratings_RateeId] ON [Ratings] ([RateeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181203125111_InitCreate', N'2.1.4-rtm-31024');

GO

