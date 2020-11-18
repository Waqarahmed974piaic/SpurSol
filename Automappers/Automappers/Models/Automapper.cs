using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automappers.Models
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Employees, EmployeeORD>();
        }
    }
}
