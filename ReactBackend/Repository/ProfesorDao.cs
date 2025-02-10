using ReactBackend.Context;
using ReactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactBackend.Repository
{
    public class ProfesorDao
    {
        #region context
        public RegistroAlumnoContext context = new RegistroAlumnoContext();
        #endregion

        #region GetById
        public Profesor login(string usuario, string pass)
        {
            var prof = context.Profesors.Where
                (p => p.Usuario == usuario && p.Pass == pass).FirstOrDefault();
            return prof;
        }
        #endregion
    }
}
