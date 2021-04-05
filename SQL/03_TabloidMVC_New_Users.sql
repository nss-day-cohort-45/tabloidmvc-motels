/* Add Deactivated column to UserProfile table */

ALTER TABLE UserProfile
ADD Deactivated bit;

UPDATE UserProfile
SET Deactivated = 0
WHERE id = 1;

/* Admin accounts to test with */

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Meghan', 'S', 'adminMeghan', 'meghan@example.com', SYSDATETIME(), NULL, 1, 0);

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Jordan', 'C', 'adminJordan', 'jordan@example.com', SYSDATETIME(), NULL, 1, 0);

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Stephanie', 'G', 'adminStephanie', 'jordan@example.com', SYSDATETIME(), NULL, 1, 0);

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Ryan', 'Y', 'adminRyan', 'ryan@example.com', SYSDATETIME(), NULL, 1, 0);


/* Author accounts to test with */
INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Joe', 'S', 'Joes', 'joe@example.com', SYSDATETIME(), NULL, 2, 0);

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Andy', 'C', 'Andyman', 'andy@example.com', SYSDATETIME(), NULL, 2, 0);

INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Michael', 'C', 'McColumbia', 'michael@example.com', SYSDATETIME(), NULL, 2, 0);


/* This is a "Bad user", starts off deactivated */
INSERT INTO [UserProfile] (
	   [FirstName], [LastName], [DisplayName], [Email], [CreateDateTime], [ImageLocation], [UserTypeId], [Deactivated])
VALUES ('Bad', 'User', 'BadUser', 'baduser@example.com', SYSDATETIME(), NULL, 2, 1);

