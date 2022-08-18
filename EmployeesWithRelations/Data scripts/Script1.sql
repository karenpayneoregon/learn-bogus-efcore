--- used to verify data while writing code

SELECT E.EmployeeID, 
       E.TitleOfCourtesy, 
       E.LastName, 
       E.FirstName, 
       E.BirthDate, 
       E.ContactTypeIdentifier, 
       CT.ContactTitle, 
       E.CountryIdentifier, 
       E.ReportsToNavigationEmployeeID, 
       C.[Name] AS Country
FROM Employees AS E
     INNER JOIN ContactType AS CT ON E.ContactTypeIdentifier = CT.ContactTypeIdentifier
     INNER JOIN Countries AS C ON E.CountryIdentifier = C.CountryIdentifier;