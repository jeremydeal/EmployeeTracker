IF EXISTS (SELECT * FROM sys.objects WHERE type = 'TR' AND name = 'TRG_TrackEmployeeUpdates')
BEGIN
	DROP TRIGGER TRG_TrackEmployeeUpdates
END
GO

CREATE TRIGGER TRG_TrackEmployeeUpdates ON Employees
AFTER UPDATE
AS
BEGIN
	DECLARE @id int, @oldManager int, @newManager int, @oldJobTitle int, @newJobTitle int, @oldPermissionLevel int, @newPermissionLevel int
	
	-- set old and new values so we can decide what to track
	SELECT @id = d.ID, @oldManager = d.ManagerID, @oldJobTitle = d.JobTitleID, @oldPermissionLevel = d.PermissionLevel
		FROM deleted AS d;

	SELECT @newManager = i.ManagerID, @newJobTitle = i.JobTitleID, @newPermissionLevel = i.PermissionLevel
		FROM inserted AS i;

	-- insert a row into EmployeeChangeHistories for each of the tracked fields that changed
	IF @oldPermissionLevel != @newPermissionLevel
	BEGIN
		INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
			VALUES (@id, 0, @oldPermissionLevel, @newPermissionLevel, GETDATE());
	END

	IF @oldJobTitle != @newJobTitle
	BEGIN
		DECLARE @oldJobTitleName varchar(100), @newJobTitleName varchar(100)

		SELECT @oldJobTitleName = jt.Name
			FROM JobTitles AS jt
			WHERE jt.ID = @oldJobTitle;

		SELECT @newJobTitleName = jt.Name
			FROM JobTitles AS jt
			WHERE jt.ID = @newJobTitle;

		INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
			VALUES (@id, 1, @oldJobTitleName, @newJobTitleName, GETDATE());
	END

	IF @oldManager != @newManager
	BEGIN
		DECLARE @oldManagerName varchar(100), @newManagerName varchar(100)

		SELECT @oldManagerName = m.FirstName + ' ' + m.LastName
			FROM Employees AS m
			WHERE m.ID = @oldManager;

		SELECT @newManagerName = m.FirstName + ' ' + m.LastName
			FROM Employees AS m
			WHERE m.ID = @newManager;

		INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
			VALUES (@id, 2, @oldManagerName, @newManagerName, GETDATE());
	END

END
GO