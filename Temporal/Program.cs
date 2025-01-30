using ReactBackend.Repository;

AlumnoDao alumnoDao = new AlumnoDao();

#region SelectAll
var alumno = alumnoDao.SelectAll();
foreach (var item in alumno)
{
    Console.WriteLine(item.Nombre);
}
#endregion
Console.WriteLine(" ");
#region SlectById
var selectById = alumnoDao.GetById(1000);
Console.WriteLine(selectById?.Nombre);
#endregion