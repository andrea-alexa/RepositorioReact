using ReactBackend.Models;
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
Console.WriteLine(" ");
#region addAlumno
var nuevoAlumno = new Alumno
{
    Direccion = "Chalatenango",
    Dni = "12345",
    Edad = 30,
    Email = "12344321@email",
    Nombre = "Ricardo JR Milos"
};

var resultado = alumnoDao.insertarAlumno(nuevoAlumno);
Console.WriteLine(resultado);
#endregion
Console.WriteLine(" ");
#region updateAlumno
var nuevoAlumno2 = new Alumno
{
    Direccion = "Chalatenango",
    Dni = "1234",
    Edad = 30,
    Email = "1234@gmail.com",
    Nombre = "Williams"
};

var resultado2 = alumnoDao.update(2, nuevoAlumno2);
Console.WriteLine(resultado2);
#endregion
Console.WriteLine(" ");
#region borrar
var result = alumnoDao.deleteAlumno(1007);
Console.WriteLine("Se elimino " + result);
#endregion