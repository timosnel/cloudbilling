@echo off
powershell -ExecutionPolicy ByPass -NoProfile -File "%~dp0run.ps1" %*
exit /b %ERRORLEVEL%