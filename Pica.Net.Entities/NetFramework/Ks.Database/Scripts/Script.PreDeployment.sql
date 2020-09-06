/*
 Plantilla de script anterior a la implementación							
--------------------------------------------------------------------------------------
 Este archivo contiene instrucciones de SQL que se ejecutarán antes del script de compilación	
 Use la sintaxis de SQLCMD para incluir un archivo en el script anterior a la implementación			
 Ejemplo:      :r .\miArchivo.sql								
 Use la sintaxis de SQLCMD para hacer referencia a una variable en el script anterior a la implementación		
 Ejemplo:      :setvar TableName miTabla							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*
No cambie las variables de nombre o ruta de acceso de la base de datos.
Cualquier variable sqlcmd será correctamente sustituida durante 
la compilación y la implementación.
*/

ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [Orders_FG] CONTAINS MEMORY_OPTIMIZED_DATA