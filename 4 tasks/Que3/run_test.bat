@echo off
set PYTHONIOENCODING=utf-8
echo Starting server...
start /b python -u Que3_server.py > server_output_utf8.txt 2>&1
timeout /t 2 /nobreak > nul
echo Running client...
python -u Que3_client.py < input_client.txt > client_output_utf8.txt 2>&1
echo Client finished.
taskkill /F /IM python.exe /T > nul 2>&1
echo Server stopped.
