@echo off
setlocal enabledelayedexpansion

REM Path to your server script
set SERVER_SCRIPT=C:\Unity\SERVER\kampai_server.py

REM Time interval to check for changes (seconds)
set INTERVAL=2

REM Get the last modified timestamp of all .py files in the folder
:GET_TIMESTAMP
set LAST_TIMESTAMP=0
for %%f in (%SERVER_SCRIPT%) do (
    for /f "tokens=1" %%a in ('powershell -command "(Get-Item '%%f').LastWriteTime.Ticks"') do (
        set LAST_TIMESTAMP=%%a
    )
)
goto :START_SERVER

:START_SERVER
echo Starting server...
python "%SERVER_SCRIPT%"
echo Server stopped. Watching for changes...

:WATCH_LOOP
set CHANGED=0
for %%f in (%SERVER_SCRIPT%) do (
    for /f "tokens=1" %%a in ('powershell -command "(Get-Item '%%f').LastWriteTime.Ticks"') do (
        if not "%%a"=="!LAST_TIMESTAMP!" (
            set LAST_TIMESTAMP=%%a
            set CHANGED=1
        )
    )
)
if !CHANGED! == 1 (
    echo Change detected! Restarting server...
    goto :START_SERVER
)
timeout /t %INTERVAL% >nul
goto :WATCH_LOOP
