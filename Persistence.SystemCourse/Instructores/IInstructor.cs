using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.SystemCourse.Instructores
{
    public interface IInstructor
    {
         Task<IEnumerable<InstructorModel>> ObtainList();
         
         Task<int> New(string name, string lastnames, string grade);
         Task<int> Update(Guid instructorId, string name, string lastnames, string grade);
         Task<int> Delete(Guid id);
        Task<InstructorModel> ObtainForId(Guid Id);


    }
}