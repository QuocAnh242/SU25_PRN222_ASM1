-- DATABASE NAME
CREATE DATABASE SU25_PRN222_SE1706_G6_DNATestServiceManager

USE SU25_PRN222_SE1706_G6_DNATestServiceManager

-- UserAccount Table
CREATE TABLE UserAccount (
    UserAccountID INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    Phone VARCHAR(50) NOT NULL,
    EmployeeCode VARCHAR(50) NOT NULL,
    RoleId INT NOT NULL,
    RequestCode VARCHAR(50),
    CreatedDate DATETIME,
    ApplicationCode VARCHAR(50),
    CreatedBy VARCHAR(50),
    ModifiedDate DATETIME,
    ModifiedBy VARCHAR(50),
    IsActive BIT NOT NULL
);

-- Sample data
INSERT INTO UserAccount (UserName, Password, FullName, Email, Phone, EmployeeCode, RoleId, RequestCode, CreatedDate, ApplicationCode, CreatedBy, ModifiedDate, ModifiedBy, IsActive) VALUES 
('acc', '@a', 'Accountant', 'Accountant@', '0913652742', '000001', 2, NULL, NULL, NULL, NULL, NULL, NULL, 1),
('auditor', '@a', 'Internal Auditor', 'InternalAuditor@', '0972224568', '000002', 3, NULL, NULL, NULL, NULL, NULL, NULL, 1),
('chiefacc', '@a', 'Chief Accountant', 'ChiefAccountant@', '0902927373', '000003', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1);

-- ServicesAnhTHQ Table
CREATE TABLE ServicesAnhTHQ (
    ServiceAnhTHQID INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceType NVARCHAR(50),
    Description NVARCHAR(MAX),
    Category NVARCHAR(50),
    CreatedDate DATETIME,
    CreatedBy VARCHAR(50),
    ModifiedDate DATETIME,
    ModifiedBy VARCHAR(50),
    IsActive BIT
);

-- ServicePriceListAnhTHQ Table
CREATE TABLE ServicePriceListAnhTHQ (
    ServicePriceListAnhTHQID INT IDENTITY(1,1) PRIMARY KEY,
    ServiceAnhTHQID INT NOT NULL,
    BasePrice DECIMAL(15,2),
    AdditionalFee DECIMAL(15,2),
    EffectiveDate DATETIME,
    ExpiryDate DATETIME,
    IsAvailable BIT,
    FOREIGN KEY (ServiceAnhTHQID) REFERENCES ServicesAnhTHQ(ServiceAnhTHQID)
);

-- BookingMinhNDA Table
CREATE TABLE BookingMinhNDA (
    BookingMinhNDAID INT IDENTITY(1,1) PRIMARY KEY,
    BookingType VARCHAR(50),
    Status VARCHAR(50),
    TotalPrice DECIMAL(15,2),
    SampleCount INT,
    RequestDate DATETIME,
    PreferredTime DATETIME,
    IsSelfCollection BIT,
    Note TEXT,
    UserAccountID INT,
    ServiceAnhTHQID INT,
    FOREIGN KEY (UserAccountID) REFERENCES UserAccount(UserAccountID),
    FOREIGN KEY (ServiceAnhTHQID) REFERENCES ServicesAnhTHQ(ServiceAnhTHQID)
);

-- InvoiceMinhNDA Table
CREATE TABLE InvoiceMinhNDA (
    InvoiceMinhNDAID INT IDENTITY(1,1) PRIMARY KEY,
    PaymentStatus NVARCHAR(50),
    PaymentMethod NVARCHAR(50),
    Amount DECIMAL(18, 2),
    Transaction_ID NVARCHAR(100),
    PaymentDate DATETIME,
    BookingMinhNDAID INT,
    FOREIGN KEY (BookingMinhNDAID) REFERENCES BookingMinhNDA(BookingMinhNDAID)
);

-- SampleNhanTTT Table
CREATE TABLE SampleNhanTTT (
    SampleNhanTTTID INT IDENTITY(1,1) PRIMARY KEY,
    BookingMinhNDAID INT,
    SampleCode VARCHAR(100),
    CollectedBy VARCHAR(100),
    Quantity INT,
    CollectedDate DATETIME,
    ReceivedDate DATETIME,
    IsValid BIT,
    IsReferenceSample BIT,
    Note TEXT,
    Status VARCHAR(50),
    FOREIGN KEY (BookingMinhNDAID) REFERENCES BookingMinhNDA(BookingMinhNDAID)
);

-- SampleTestResultNhanTTT Table
CREATE TABLE SampleTestResultNhanTTT (
    SampleTestResultNhanTTTID INT IDENTITY(1,1) PRIMARY KEY,
    SampleNhanTTTID INT,
    GenLocus VARCHAR(255),
    AlenOne VARCHAR(255),
    AlenTwo VARCHAR(255),
    IsMatched BIT,
    Note TEXT,
    FOREIGN KEY (SampleNhanTTTID) REFERENCES SampleNhanTTT(SampleNhanTTTID)
);

-- TestResultThanhDC Table
CREATE TABLE TestResultThanhDC (
    TestResultThanhDCID INT IDENTITY(1,1) PRIMARY KEY,
    BookingMinhNDAID INT,
    ResultCode VARCHAR(50),
    MatchPercent DECIMAL(10,5),
    ProbabilityPaternity DECIMAL(10,5),
    Conclusion TEXT,
    ResultDate DATETIME,
    EvaluatedBy VARCHAR(100),
    IsDelivered BIT,
    Note TEXT,
    FOREIGN KEY (BookingMinhNDAID) REFERENCES BookingMinhNDA(BookingMinhNDAID)
);

-- RelationshipTestThanhDC Table
CREATE TABLE RelationshipTestThanhDC (
    RelationshipTestThanhDCID INT IDENTITY(1,1) PRIMARY KEY,
    TestResultThanhDCID INT NOT NULL,
    RelationshipExpected NVARCHAR(100),
    RelationshipConclusion NVARCHAR(100),
    MatchProbability DECIMAL(5,2),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TestResultThanhDCID) REFERENCES TestResultThanhDC(TestResultThanhDCID)
);

-- AppointmentTuTTC Table
CREATE TABLE AppointmentScheduleTuTTC (
    AppointmentScheduleTuTTCID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT,
    UserAccountID INT,
    AppointmentDate DATETIME,
    Status VARCHAR(50),
    StaffInCharge VARCHAR(100),
    Location VARCHAR(255),
    DurationMinutes INT,
    ConfirmedBy VARCHAR(100),
    IsConfirmed BIT,
    IsCanceled BIT,
    AdditionalNote TEXT,
    FOREIGN KEY (BookingID) REFERENCES BookingMinhNDA(BookingMinhNDAID),
    FOREIGN KEY (UserAccountID) REFERENCES UserAccount(UserAccountID)
);

-- AppointmentLogTuTTC Table
CREATE TABLE AppointmentLogTuTTC (
    AppointmentLogTuTTCID INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentScheduleTuTTCID INT,
    LogText TEXT,
    CreatedAt DATETIME,
    CreatedBy VARCHAR(100),
    LogType VARCHAR(50),
    FOREIGN KEY (AppointmentScheduleTuTTCID) REFERENCES AppointmentScheduleTuTTC(AppointmentScheduleTuTTCID)
);

-- KitDeliveryNguyenTQ Table
CREATE TABLE KitDeliveryNguyenTQ (
    KitDeliveryNguyenTQID INT IDENTITY(1,1) PRIMARY KEY,
    BookingMinhNDAID INT,
    KitCode VARCHAR(50),
    DeliveryDate DATETIME,
    IsDelivered BIT,
    CourierOut VARCHAR(100),
    TrackingNumberOut VARCHAR(100),
    DeliveryStatusOut VARCHAR(50),
    DeliveryNoteOut NVARCHAR(255),
    ReturnDate DATETIME,
    IsReturned BIT,
    CourierIn VARCHAR(100),
    TrackingNumberIn VARCHAR(100),
    DeliveryStatusIn VARCHAR(50),
    ReturnReason NVARCHAR(255),
    DeliveryNoteIn NVARCHAR(255),
    DamageReported BIT,
    Fee DECIMAL(10,2),
    LastUpdated DATETIME DEFAULT GETDATE(),
    IsExpressDelivery BIT,
    FOREIGN KEY (BookingMinhNDAID) REFERENCES BookingMinhNDA(BookingMinhNDAID)
);

-- FeedbackNguyenTQ Table
CREATE TABLE FeedbackNguyenTQ (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    UserAccountID INT NOT NULL,
    KitDeliveryNguyenTQID INT NULL,
    ServiceID INT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(1000),
    FeedbackDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (KitDeliveryNguyenTQID) REFERENCES KitDeliveryNguyenTQ(KitDeliveryNguyenTQID),
    FOREIGN KEY (UserAccountID) REFERENCES UserAccount(UserAccountID)
);

INSERT INTO ServicesAnhTHQ (ServiceName, ServiceType, Description, Category, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, IsActive) VALUES
(N'DNA Paternity Test', N'Genetic', N'Used to determine biological relationship between a father and child.', N'Family', GETDATE(), 'admin', NULL, NULL, 1),
(N'DNA Ancestry Analysis', N'Genetic', N'Provides information about ethnic background and ancestry.', N'Heritage', GETDATE(), 'admin', NULL, NULL, 1),
(N'Forensic DNA Profiling', N'Legal', N'Used in criminal investigations to match DNA evidence.', N'Legal', GETDATE(), 'admin', NULL, NULL, 1),
(N'Prenatal DNA Test', N'Medical', N'Used to determine paternity before birth.', N'Medical', GETDATE(), 'admin', NULL, NULL, 1),
(N'Sibling DNA Test', N'Genetic', N'Used to verify biological sibling relationships.', N'Family', GETDATE(), 'admin', NULL, NULL, 1);

INSERT INTO BookingMinhNDA(BookingType, Status, TotalPrice, SampleCount, RequestDate, PreferredTime, IsSelfCollection, Note, UserAccountID, ServiceAnhTHQID) 
VALUES 
    ('DNA Test', 'Pending', 1200.00, 2, '2025-05-20 10:00:00', '2025-05-25 09:00:00', 1, 'Urgent booking', 1, 1),
    ('Paternity Test', 'Confirmed', 850.00, 1, '2025-05-18 14:00:00', '2025-05-23 10:30:00', 0, 'First-time client', 2, 2),
    ('Ancestry', 'Cancelled', 500.00, 1, '2025-05-15 16:00:00', '2025-05-22 11:00:00', 1, 'Rescheduled', 3, 3),
    ('Immigration DNA', 'Completed', 1500.00, 3, '2025-05-12 09:00:00', '2025-05-20 13:00:00', 0, 'Embassy requested', 1, 1),
    ('Prenatal Test', 'Pending', 2000.00, 1, '2025-05-21 11:30:00', '2025-05-28 08:30:00', 1, 'Sensitive case', 2, 2),
    ('DNA Test', 'Confirmed', 1300.00, 2, '2025-05-19 10:15:00', '2025-05-24 10:00:00', 0, NULL, 3, 3),
    ('Paternity Test', 'Pending', 900.00, 1, '2025-05-22 12:45:00', '2025-05-27 11:45:00', 1, 'Follow-up required', 1, 1),
    ('Immigration DNA', 'Confirmed', 1550.00, 2, '2025-05-17 15:00:00', '2025-05-26 14:00:00', 0, '', 2, 2),
    ('Ancestry', 'Completed', 480.00, 1, '2025-05-10 10:30:00', '2025-05-19 09:30:00', 1, 'Repeat customer', 3, 3),
    ('DNA Test', 'Cancelled', 1100.00, 2, '2025-05-14 08:30:00', '2025-05-21 09:00:00', 0, 'Cancelled by user', 1, 1);
