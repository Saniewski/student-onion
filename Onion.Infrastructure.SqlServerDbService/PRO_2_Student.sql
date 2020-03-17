
use s16996;

create table pro.Student
(
	Id int,
	FirstName varchar(200),
	LastName varchar(200)
);

insert into pro.Student
(Id, FirstName, LastName) values
(1, 'Jan', 'Kowalski'),
(2, 'Anna', 'Malewski'),
(3, 'Andrzej', 'Maciejewski'),
(4, 'Wojciech', 'Wojciechowski'),
(5, 'Natalia', 'Iksiñska'),
(6, 'Jaros³aw', 'Jaros³awski'),
(7, 'Jadwiga', 'Jagoda');

create procedure sp_pro_InsertStudent @Id int, @FirstName varchar(200), @LastName varchar(200)
as
	insert into pro.Student
	(Id, FirstName, LastName) values
	(@Id, @FirstName, @LastName);
go

create procedure sp_pro_SelectStudents
as
	select * from pro.Student;
go