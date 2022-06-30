CREATE DATABASE Mediafier;

USE Mediafier;

CREATE TABLE Users(users_Id int primary key identity (1,1) , users_Username varchar(50) NOT NUll , Users_Password NVARCHAR(500) NOT NULL, users_CreatedAt datetime);



CREATE TABLE Folders(folders_Id int primary key identity(1,1), folders_name varchar(50) NOT NULL, folders_CreatedBy int FOREIGN KEY REFERENCES Users(users_Id),folders_CreatedAt datetime , folders_ISDeleted bit default 0);

--ALTER TABLE Folders ADD CONSTRAINT [DF_folders_ISDeleted] DEFAULT (0) FOR folders_ISDeleted 
--GO

CREATE TABLE document(doc_id int primary key identity(1,1), doc_name varchar(100),doc_contentType varchar(100), doc_size int, doc_createdBy int FOREIGN KEY REFERENCES Users(users_Id), doc_createdAt datetime, doc_folderId int FOREIGN KEY REFERENCES Folders(folders_Id), doc_isDeleted bit);

ALTER TABLE document ADD CONSTRAINT [doc_isDeleted] DEFAULT (0) FOR doc_isDeleted 
GO

