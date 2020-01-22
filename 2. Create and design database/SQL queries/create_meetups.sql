create table Meetups (
id int not null identity(1,1) primary key,
title varchar(100) not null,
host_id int foreign key references Users(id)  not null,
location varchar(20) not null,
date_time smalldatetime not null,
attendees_limit int not null,
attendees_count int not null,
tags varchar(500) not null,
detail varchar(8000) not null
)