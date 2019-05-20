Lucram pe Visual Studio 2019
Descarcati Microsoft SQL Server Management Studio(mssql sms)
Cand il deschideti, conectativa la server cum e implicit; daca nu merge, apasati win+r si scrieti services.msc.
        Dupa ce apare fereastra, mereti pe sql server, click dreapta, start, asteptati sa se incarce
        Daca nu apare in fereastra un sql server(cu orice scris dupa el), descarcati un sql database server de pe net(nu are importatnta versiunea)

in proiect ar trebui sa fie instalate pachetele, nu stiu cum se face commit pe github, daca nu merg pachetele(apar erori in cod), 
click dreapta(in dreapta la solution explorer), manae nuet package si cautati identity in browse, instalati pe primul care apare, apoi pe cel cun entity in coada, apoi cautati owin si instalati-l pe ala cu ceva host system web(nu le mai stiu exact)

mergeti in web.config(dupa ce s-a instalat mssql sms) si cautati tag-ul 
<connectionStrings>
    <add name="ConString" connectionString="Data Source=PC-ANDRA;Database=master;Initial Catalog=MyCinema;Integrated Security=True"
      providerName="System.Data.SqlClient" />
    <add name="MyCinemaContext" connectionString="Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=MyCinemaContext-20190418041103; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|MyCinemaContext-20190418041103.mdf"
      providerName="System.Data.SqlClient" />
  </connectionStrings>

  incercati unul din alea 2 tag-uri de add, daca il luati pe primul, trebuie sa schimbati PC-ANDRA cu serverul la care v-ati conectat voi in mssql sms.

mie imi ruleaza programul cand am controller-ul deschis si rulez dintr-un fisier .cshtml, la voi daca nu va merge, deschideti toate paginile .cshtml și controllerul si dati rulare dintr-o pagina(nu din controller ca nu merge)

