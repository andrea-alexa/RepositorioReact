﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactBackend.Models;
using ReactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDao _dao = new AlumnoDao();

        #region endPointAlumnoProfesor
        [HttpGet("alumnoProfesor")]
        public List<AlumnoProfesor> GetAlumnoProfesor(string usuario)
        {
            return _dao.alumnoProfesor(usuario);
        }
        #endregion

        #region SelectById
        [HttpGet("alumno")]
        public Alumno selectById(int id)
        {
            var alumno = _dao.GetById(id);
            return alumno;
        }
        #endregion

        #region ActualizarDatos
        [HttpPut("alumno")]
        public bool actualizarAlumno([FromBody] Alumno alumno)
        {
            return _dao.update(alumno.Id, alumno);
        }
        #endregion

        #region AlumnoMatricula
        [HttpPost("alumno")]
        public bool insertarMatricula([FromBody] Alumno alumno, int idAsignaruta)
        {
            return _dao.InsertarMatricula(alumno, idAsignaruta);
        }
        #endregion

        #region deleteAlumno
        [HttpDelete("alumno")]

        public bool eliminarAlumno(int id)
        {
            return _dao.eliminarAlumno(id);
        }
        #endregion
    }
}
