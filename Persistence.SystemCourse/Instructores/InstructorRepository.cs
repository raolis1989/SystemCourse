using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Dapper;
using Persistence.SystemCourse.DapperConection;

namespace Persistence.SystemCourse.Instructores
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public Task<int> Delete(Guid id)
        {
            var storeProcedure = "usp_Instructor_Delete";
            try
            {
                var connection = _factoryConnection.GetConnection();
               var resultado=  connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId= id
                    },
                    commandType: CommandType.StoredProcedure
                    );
                _factoryConnection.CloseConnection();
                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("error delete data", ex);
            }
        }

        public async Task<int> New(string name, string lastnames, string grade)
        {
            var storeProcedure = "usp_Instructor_New";

            try
            {
                var connection = _factoryConnection.GetConnection();

                var resultado = await connection.ExecuteAsync(
                    storeProcedure, new
                    {
                        InstructorId = Guid.NewGuid(),
                        NameP = name,
                        LastName = lastnames,
                        Grade = grade
                    },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Not Insert Data Instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<InstructorModel> ObtainForId(Guid Id)
        {
            var storeProcedure = "usp_Instructor_Obtain_Id";

            try
            {
                var connection = _factoryConnection.GetConnection();

                var resultado = await connection.QueryFirstAsync<InstructorModel>(
                    storeProcedure, new
                    {
                        Id = Id,
                    },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Not Find Data Instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            
            }
        }

        public async Task<IEnumerable<InstructorModel>> ObtainList()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("error consult data", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }

            return instructorList;
        }

        public async Task<int> Update(Guid instructorId, string name, string lastnames, string grade)
        {
            var storeProcedure = "usp_Instructor_Edit";
            try
            {
                var connection = _factoryConnection.GetConnection();

                var resultado = await connection.ExecuteAsync(
                    storeProcedure, new
                    {
                        InstructorId = instructorId,
                        NameP = name,
                        LastName = lastnames,
                        Grade = grade
                    },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("Not Update Data Instructor", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }
    }
}