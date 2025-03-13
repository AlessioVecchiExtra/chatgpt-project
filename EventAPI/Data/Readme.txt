
SCAFFOLDING
===========

EXTRA
Scaffold-DbContext "User Id=sa;Password=Tgsd!ex06; Server=192.168.1.18;Database=Extrait_meeting_survey;Trusted_Connection=False;Encrypt=false;" Microsoft.EntityFrameworkCore.SqlServer -Context AppDbContext -OutputDir ./Models -force -Schema "dbo"



LOCAL
Scaffold-DbContext "Server=.\SQLEXPRESS;Database=Extrait_meeting_survey;Trusted_Connection=False;Encrypt=false;TrustServerCertificate=True;User Id=sa_Eumetra_Budget;Password=eumetra;" Microsoft.EntityFrameworkCore.SqlServer -Context AppDbContext -OutputDir ./Models -force -Schema "dbo"

