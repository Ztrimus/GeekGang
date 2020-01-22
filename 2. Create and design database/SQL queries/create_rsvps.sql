create table RSVPs (
id int not null identity(1,1) primary key,
meet_id int foreign key references Meetups(id) not null,
user_id int foreign key references Users(id) not null,
status varchar(10) not null
)