create database QLSV1;
use QLSV1

CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY,
    FacultyName NVARCHAR(100)
);

CREATE TABLE Student (
    StudentID BIGINT PRIMARY KEY,
    FullName NVARCHAR(100),
    AverageScore DECIMAL(3, 1),
    FacultyID INT,
    FOREIGN KEY (FacultyID) REFERENCES Faculty(FacultyID)
);

INSERT INTO Faculty (FacultyID, FacultyName)
VALUES 
(1, N'Công Nghệ Thông Tin'),
(2, N'Ngôn Ngữ Anh'),
(3, N'Quản trị kinh doanh');

-- Insert data into Student table
INSERT INTO Student (StudentID, FullName, AverageScore, FacultyID)
VALUES
(1611061916, N'Nguyễn Trần Hoàng Lan', 4.5, 1),
(1711060596, N'Đàm Minh Đức', 2.5, 1),
(1711061004, N'Nguyễn Quốc An', 10, 2);


select * from Faculty 


select * from Student