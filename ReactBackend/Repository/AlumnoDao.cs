using ReactBackend.Context;
using ReactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactBackend.Repository
{
    public class AlumnoDao
    {
        #region Contex
        public RegistroAlumnoContext contexto = new RegistroAlumnoContext();
        #endregion

        #region Select All
        public List<Alumno> SelectAll()
        {
            var alumno = contexto.Alumnos.ToList<Alumno>();
            return alumno;
        }
        #endregion

        #region Seleccionamos por ID
        public Alumno? GetById(int id)
        {
            var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

        #region Insertar Alumno
        public bool insertarAlumno(Alumno alumno)
        {
            try
            {
                var alum = new Alumno
                {
                    Direccion = alumno.Direccion,
                    Edad = alumno.Edad,
                    Email = alumno.Email,
                    Dni = alumno.Dni
                };

                contexto.Alumnos.Add(alum);
                contexto.SaveChanges();

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region update Alumno
        public bool update(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);

                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }

                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;

                contexto.Alumnos.Update(alumnoUpdate);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public bool deleteAlumno (int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar == null)
                {
                    return false;
                }
                else
                {
                    contexto.Alumnos.Remove(borrar);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region leftjoin
        public List<AlumnoAsignatura> SelectAlumAsig()
        {
            var consulta = from a in contexto.Alumnos
                           join m in contexto.Matriculas on a.Id equals m.AlumnoId
                           join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                           select new AlumnoAsignatura
                           {
                               NombreAlumno = a.Nombre,
                               NombreAsignatura = asig.Nombre
                           };
            return consulta.ToList();
        }
        #endregion

        #region leftJoinAlumnoMatriculaMateria
        public List<AlumnoProfesor> alumnoProfesor(string nombreProfesor)
        {
            var listadoALumno = from a in contexto.Alumnos
                                join m in contexto.Matriculas on a.Id equals m.AlumnoId
                                join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Email = a.Email,
                                    Asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #endregion

        #region SeleccionPorDNI
        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = contexto.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion

        #region AlumnoMatricula
        public bool InsertarMatricula(Alumno alumno, int idAsig)
        {
            try
            {
                var alumnoDNI = DNIAlumno(alumno);

                if (alumno == null)
                {
                    insertarAlumno(alumno);

                    var alumnoInsertado = DNIAlumno(alumno);
                    var unirAlumnoMatricula = matriculaAsignatiraAlumno(alumno, idAsig);
                    
                    if (unirAlumnoMatricula == false)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    matriculaAsignatiraAlumno(alumnoDNI, idAsig);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region matriculaAsignatiraAlumno
        public bool matriculaAsignatiraAlumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();

                matricula.AlumnoId = alumno.Id;
                matricula.AsignaturaId = idAsignatura;

                contexto.Matriculas.Add(matricula);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
