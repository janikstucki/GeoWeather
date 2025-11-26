use master 
go
if db_id('stations_db') is not null
	drop database stations_db;
go
create database stations_db;
go

use stations_db;
go
if object_id('stations') is not null
	drop table stations;
go
create table stations
(
	station_id   int      identity    not null primary key,
	name varchar(100) not null,
	xCoordinate  float        not null,
	yCoordinate  float        not null
);
go
create table stationData
(
	station_id int not null foreign key references stations(station_id),
	temperatur int not null,
	humidity int not null,
	windSpeed int not null,
	windDirection varchar(10) not null
);
go
insert into stations(name, xCoordinate, yCoordinate) values
('Central Station', 34.0522, -118.2437);
go
insert into stationData(station_id, temperatur, humidity, windSpeed, windDirection) values
(1, 75, 60, 10, 'NW');

go
select * from  stations;
go
select * from stationData;