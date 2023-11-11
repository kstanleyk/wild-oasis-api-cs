using System.Collections.Generic;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Test.Common.Branch
{
    public class BranchTestData
    {

        public static IEnumerable<object[]> TestData
        {
            get
            {
                //tenant
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = "",
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Tenant is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = null,
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Tenant is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 11),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Tenant must not exceed 10 characters."
                };

                //Code
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = "",
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Code is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = null,
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Code is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 6),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Code must not exceed 5 characters."
                };

                //Description
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = "",
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Description is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = null,
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Description is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 76),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Description must not exceed 75 characters."
                };

                //BranchName
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = "",
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Name is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = null,
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Name is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 76),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Name must not exceed 75 characters."
                };

                //BranchShortName
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = "",
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Short Name is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = null,
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Short Name is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 36),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Branch Short Name must not exceed 35 characters."
                };

                //StationCode
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = "",
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Station Code is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = null,
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Station Code is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 11),
        Address = new string('a', 50),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Station Code must not exceed 10 characters."
                };

                //Address
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = "",
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Address is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
       Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = null,
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Address is required."
                };
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 51),
        Telephone = new string('t', 35),
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Address must not exceed 50 characters."
                };

                //Telephone
                yield return new object[]
                {
    new BranchVm
    {
        Tenant = new string('t', 10),
        Code = new string('c', 5),
        Description = new string('d', 75),
        BranchName = new string('b', 75),
        BranchShortName = new string('b', 35),
        StationCode = new string('s', 10),
        Address = new string('a', 50),
        Telephone = "",
        Region = new string('r', 2),
        Motto = new string('m', 150),
        HeadOffice = new string('h', 2),
        Employer = new string('e', 5),
        BranchType = new string('b', 2),
        BranchTown = new string('b', 35),
    },
    "Telephone is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = null,
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Telephone is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 36),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Telephone must not exceed 35 characters."
                };

                //Region
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = "",
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Region is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = null,
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Region is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 3),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Region must not exceed 2 characters."
                };

                //Motto
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = "",
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Motto is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = null,
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Motto is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 151),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Motto must not exceed 150 characters."
                };

                //HeadOffice
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = "",
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Head Office is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = null,
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Head Office is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 3),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Head Office must not exceed 2 characters."
                };

                //Employer
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = "",
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Employer is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = null,
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Employer is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 6),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 35),
                    },
                    "Employer must not exceed 5 characters."
                };

                //BranchType
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = "",
                        BranchTown = new string('b', 35),
                    },
                    "Branch Type is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = null,
                        BranchTown = new string('b', 35),
                    },
                    "Branch Type is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 3),
                        BranchTown = new string('b', 35),
                    },
                    "Branch Type must not exceed 2 characters."
                };

                //BranchTown
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = "",
                    },
                    "Branch Town is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = null,
                    },
                    "Branch Town is required."
                };
                yield return new object[]
                {
                    new BranchVm
                    {
                        Tenant = new string('t', 10),
                        Code = new string('c', 5),
                        Description = new string('d', 75),
                        BranchName = new string('b', 75),
                        BranchShortName = new string('b', 35),
                        StationCode = new string('s', 10),
                        Address = new string('a', 50),
                        Telephone = new string('t', 35),
                        Region = new string('r', 2),
                        Motto = new string('m', 150),
                        HeadOffice = new string('h', 2),
                        Employer = new string('e', 5),
                        BranchType = new string('b', 2),
                        BranchTown = new string('b', 36),
                    },
                    "Branch Town must not exceed 35 characters."
                };




            }
        }
    }
}
