create table Users (
id int not null identity(1,1) primary key,
firstname varchar(20) not null,
lastname varchar(20) not null,
email varchar(50) not null,
password varchar(50) not null,
)