USE msdb;
GO

EXEC sp_add_job
    @job_name = N'Job_AnularTodosTokens';

-- Passo do job
EXEC sp_add_jobstep
    @job_name = N'Job_AnularTodosTokens',
    @step_name = N'Executar Procedure AnularTodosTokens',
    @subsystem = N'TSQL',
    @command = N'EXEC AnularTodosTokens;',
    @database_name = N'SeuBancoDeDados';

-- Agenda: todo dia à meia-noite
EXEC sp_add_schedule
    @schedule_name = N'Schedule_AnularTokens_Diario',
    @freq_type = 4,            -- diário
    @freq_interval = 1,        -- todos os dias
    @active_start_time = 0000; -- 00:00h

-- Liga o job ao schedule
EXEC sp_attach_schedule
    @job_name = N'Job_AnularTodosTokens',
    @schedule_name = N'Schedule_AnularTokens_Diario';

-- Adiciona o job ao servidor
EXEC sp_add_jobserver
    @job_name = N'Job_AnularTodosTokens';
GO
