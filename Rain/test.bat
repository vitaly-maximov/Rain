@ECHO OFF
for /f "delims=" %%A in ('forfiles /s /m *.txt /c "cmd /c echo @relpath"') do Rain.exe %%A
pause