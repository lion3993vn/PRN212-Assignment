create database PRN231_SU23_StudentGroupDB
use PRN231_SU23_StudentGroupDB


create table UserRole
(
Id int identity primary key,
Username nvarchar(250) unique,
Passphrase varchar(250),
UserRole int 
)
GO

insert into UserRole values ('admin','admin@123', 1)
insert into UserRole values ('staff','staff@123', 2)
insert into UserRole values ('member','member@123', 3)
GO


create table StudentGroup
(
Id int identity primary key,
Code nvarchar(10),
GroupName nvarchar(250) 
)
GO

insert into StudentGroup values ('G001','Group001')
insert into StudentGroup values ('G002','Group002')
insert into StudentGroup values ('G003','Group003')
GO

create table Student
(
Id int identity primary key,
Email nvarchar(250),
FullName nvarchar(250),
DateOfBirth datetime,
GroupId int references StudentGroup(Id)
)
GO

insert into Student values ('minh@fpt.edu.vn','Le Van Minh1',21/02/2002,1)
insert into Student values ('minh2@fpt.edu.vn','Le Van Minh2',21/03/2002,1)
insert into Student values ('minh3@fpt.edu.vn','Le Van Minh3',21/04/2002,2)
insert into Student values ('minh4@fpt.edu.vn','Le Van Minh4',21/05/2002,2)
insert into Student values ('minh5@fpt.edu.vn','Le Van Minh5',21/06/2002,3)
GO