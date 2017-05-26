USE master
GO
-- Create database via attach
CREATE DATABASE [RbacWeb]
    ON ( FILENAME = N'C:\data\RbacWeb.mdf'),
    ( FILENAME = N'C:\data\RbacWeb_log.ldf')
    FOR ATTACH;
GO