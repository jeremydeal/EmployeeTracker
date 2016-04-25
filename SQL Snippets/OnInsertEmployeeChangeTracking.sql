IF EXISTS (SELECT * FROM sys.objects WHERE type = 'TR' AND name = 'TRG_TrackEmployeeInserts')
BEGIN
	DROP TRIGGER TRG_TrackEmployeeInserts
END
GO

CREATE TRIGGER TRG_TrackEmployeeInserts ON Employees
AFTER INSERT
AS
BEGIN
	DECLARE @id int, @newManager int, @newJobTitle int, @newPermissionLevel int, @newManagerName varchar(100), @newJobTitleName varchar(100)
	
	-- grab inserted values that we want to track
	SELECT @id = i.ID, @newManager = i.ManagerID, @newJobTitle = i.JobTitleID, @newPermissionLevel = i.PermissionLevel
		FROM inserted AS i;

	-- convert inserted IDs into the corresponding names	
	SELECT @newJobTitleName = jt.Name
		FROM JobTitles AS jt
		WHERE jt.ID = @newJobTitle;

	SELECT @newManagerName = m.FirstName + ' ' + m.LastName
		FROM Employees AS m
		WHERE m.ID = @newManager;

	-- insert a row into EmployeeChangeHistories for each of the tracked fields
	INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
		VALUES (@id, 0, '', @newPermissionLevel, GETDATE());

	INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
		VALUES (@id, 1, '', @newJobTitleName, GETDATE());

	INSERT EmployeeChangeHistories (EmployeeID, Type, OldValue, NewValue, DateChanged)
		VALUES (@id, 2, '', @newManagerName, GETDATE());

END
GO