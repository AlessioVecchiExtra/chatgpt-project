-- Prima di inserire i dati, vediamo quante domande e risposte ci sono
DECLARE @QuestionCount INT;
DECLARE @AnswerCount INT;

-- Conta il numero di domande e risposte nella tabella QuestionAnswer
SELECT @QuestionCount = COUNT(DISTINCT QuestionId) FROM dbo.QuestionAnswer WHERE Deleted = 0;
SELECT @AnswerCount = COUNT(*) FROM dbo.QuestionAnswer WHERE Deleted = 0;

-- Crea una variabile per tenere traccia di quanti record sono stati inseriti
DECLARE @Counter INT = 0;
DECLARE @MeetingId INT = 1;
DECLARE @QuestionId INT = 1;

-- Ciclo per inserire 1000 voti casuali
WHILE @Counter < 500
BEGIN
	DECLARE @QuestionAnswerId INT;
	DECLARE @QuestionAnswerText nvarchar(max);
    -- Inserisci una riga casuale nella tabella Vote

	SELECT TOP 1 @QuestionId= QuestionId FROM dbo.QuestionAnswer WHERE Deleted = 0 ORDER BY NEWID();
	SELECT TOP 1 @QuestionAnswerId = Id, @QuestionAnswerText = AnswerText
	FROM dbo.QuestionAnswer WHERE QuestionId = @QuestionId AND Deleted = 0 ORDER BY NEWID()

    INSERT INTO dbo.Vote (MeetingId, QuestionId, QuestionAnswerId, QuestionAnswerText, CreatedAt, Deleted)
    SELECT 
        
        @MeetingId as MeetingId,        
        @QuestionId as QuestionId, 
		@QuestionAnswerId as QuestionAnswerId,
		@QuestionAnswerText  as QuestionAnswerText,              
        GETDATE() as CreatedAt,        
        0 as Deleted;

    -- Incrementa il contatore
    SET @Counter = @Counter + 1;
END;
