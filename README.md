# Noliktavu un to saturošo objektu datu uzskaites sistēma

# Projekta apraksts
Mazām kompānijām bieži ir problēmas ar savu noliktavu pārvaldi. Tās bieži visu inventarizāciju dara uz papīra lapām vai izmantojot citas metodes, kuras ir lēnas, prasa lielu daudzumu cilvēku un tādējādi kompānijai tas izmaksā lielu daudzumu naudas. Tajā pašā laikā lielas kompānijas kā Amazon, Nike, DHL un citas izmanto speciāli veidota lietotnes savās noliktavās. Tādēļ mans piedāvājums ir lietotne, ar kuras palīdzību, jebkura kompānija var viegli pārvaldīt savas noliktavas izmantojot plaši pieejamas ierīces, piemēram darbinieku telefonus, tādējādi izsekojot ienākošos un izejošos produktus. 
Es šo tēmu izvēlējos, jo es esmu dzirdējis, cik lielas problēmas ir mazākām kompānijām un start-up kompānijām, kuras nodarbojas ar preču pārdošanu, pārvaldīt savas noliktavas, kā arī problēmas kompānijām, kuras nodarbojas ar kurjera pakalpojumiem.  

# Izmantotās tehnoloģijas
Projektā tiek izmantots:
- ASP.NET
- HTML/CSS/JS
- Android studio(Kotlin)
- Azure
- C#
- Kotlin

# Izmantotie avoti
[Kotlin documentation](https://kotlinlang.org/docs/home.html) - Kotlin dokumentācija

[W3Schools](https://www.w3schools.com/html/default.asp) - Tika ņemta HTML dokumentācija

[Bootswatch](https://bootswatch.com/) - Bezmaksas Bootstrap dizaini

[Learn ASP.NET Core MVC (.NET 6) - Full Course](https://www.youtube.com/watch?v=hZ1DASYd9rk) - .NET 6 MVC kurss

[ASP.NET core tutorial for beginners](https://www.youtube.com/playlist?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Atskaņošanas saraksts ar video par ASP.NET izstrādi

[Minimal API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0) - Pamācība kā izveidot minimal web API izmantojot .NET

[Retrieving Data from API](https://www.youtube.com/watch?v=Sitt4aliSz4) - Video par datu iegūšanu lietotnē no API

[How to parse JSON using Kotlin](https://johncodeos.com/how-to-parse-json-in-android-using-kotlin/) - Raksts kā apstrādāt JSON izmantojot Kotlin

[BCrypt](https://github.com/BcryptNet/bcrypt.net) - BCrypt.NET dokumentācija

[RegularExpressionAtribute](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.regularexpressionattribute?view=net-6.0) - Dokumentācija par RegularExpressionAtribute klasi

[Data validation](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0) - Dokumentācija par datu validāciju

[Regex for password](https://social.msdn.microsoft.com/Forums/en-US/37cc6433-21f7-46cd-857c-fe60cc6b252e/regex-for-password-with-atlease-one-uppercaseone-lowercaseone-nonalphabet-either-number-or?forum=aspgettingstarted) - Raksts, kā izveidot regular expression priekš paroles validācijas

[Data annotations](https://www.c-sharpcorner.com/article/model-validation-using-data-annotations-in-asp-net-mvc/) - Raksts, kā izveidot modeļa validāciju izmantojot datu anotācijas

[Validate username of user](https://stackoverflow.com/questions/41643385/how-to-validate-username-and-password-of-user-before-log-in-in-asp-net-identity) - Raksts, kā manuāli validēt lietotājvārdu un paroli pirms autorizācijas

[Decimal data annotations](https://stackoverflow.com/questions/19811180/best-data-annotation-for-a-decimal18-2) - Raksts, kā limitēt decimal datu tipa ievadi izmantojot datu anotācijas

[Limit TextBoxFor](https://stackoverflow.com/questions/40676130/how-can-limit-to-2-decimals-in-textboxfor-in-mvc) - Raksts, kā limitēt TextBoxFor ievadi Android

[Limit edittext](https://stackoverflow.com/questions/48753337/android-edittext-two-decimal-places) - Raksts, kā limitēt edittext uz diviem cipariem aiz komata Android

[Calculate sum](https://www.c-sharpcorner.com/article/calculate-the-sum-of-the-datatable-column-in-c-sharp/) - Raksts, kā kalkulēt summu no datu kolonas C#

[Aggregate](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-6.0) - Dokumentācija par saskaitīšanas metodi

[Select](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.select?view=net-6.0) - Dokumentācija par select metodi

[Include](https://docs.microsoft.com/en-us/dotnet/api/system.data.objects.objectquery-1.include?view=netframework-4.8) - Dokumentācija par include metodi

[SelectList](https://www.youtube.com/watch?v=MUTUjxXHzzQ) - Video, kā izveidot nolaižamo izvēlni izmantojot SelectList

[Dropdownlist](https://stackoverflow.com/questions/20242981/asp-net-mvc-dropdown-list-from-selectlist) - Raksts, kā izveidot nolaižamo izvēlni

[Generate APK](https://code.tutsplus.com/tutorials/how-to-generate-apk-and-signed-apk-files-in-android-studio--cms-37927) - Raksts, kā ģenerēt APK failu

[Opera](https://www.opera.com/download/requirements) - Pārlūkprogrammas “Opera” minimālās prasības

[Export to CSV](https://blog.elmah.io/export-data-to-excel-with-asp-net-core/) - Raksts, kā eksportēt datus uz CSV failu

[RedirectToAction](https://stackoverflow.com/questions/10785245/redirect-to-action-in-another-controller) - Raksts, kā izmantot RedirectToAction lai aizvestu uz citu kontrolieri

# Uzstādīšanas instrukcijas
## Mājaslapas instalācija
Mājaslapu var palaist ievadot saiti pārlūkprogrammā. Ja mājaslapu nepieciešams palaist lokāli, tad: 
1. Jāinstalē uz datora Visual Studio 2022 Community 
2. Jāpievieno projekta faili projektam 
3. Jānomaina “DefaultConnection” appsettings.json failā uz lokālā servera nosaukumu 
4. Jāuzraksta “update-database” komanda iekš Package Manager Console 
5. Jānospiež Start poga rīkjoslā 

## Lietotnes instalācija
Lietotne, atšķirībā no mājaslapas, ir obligāti jāinstalē uz ierīces. Šis process var atšķirties starp ierīcēm un Android versijām, bet lielākoties tas izdarāms šādi: 
1. Ja tas nav izdarīts, tad atļaut ierīces iestatījumos pārlūkprogrammai instalēt nezināmas lietotnes 
2. Ielādēt APK failu un atvērt to 
3. Iziet caur ielādes ekrāniem 
4. Ja parādās Play Protect aizsardzība pret nezināmu izstrādātāju, tad jānospiež “Install Anyways” poga 
5. Pēc tā lietotnei būtu jābūt ielādētai un to var palaist no lietotņu saraksta 

# Mājaslapas links
[Links](https://invpalmajaslapa.azurewebsites.net)

# Aplikācijas APK ielāde
[Links](https://github.com/rvt-prog-kval-22/D42-DavisDabols-inventarizacijasPaligs/raw/main/Aplikacija/invpalapp.apk)
