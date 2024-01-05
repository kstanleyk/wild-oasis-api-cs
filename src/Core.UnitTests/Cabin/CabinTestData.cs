using System.Collections.Generic;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Test.Cabin
{
    public class CabinTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                //tenant
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Tenant is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Tenant is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Tenant must not exceed 10 characters."
                };

                //Code
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Code is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Code is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Code must not exceed 5 characters."
                };

                //Description
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = "",
                    },
                    "Description is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = null,
                    },
                    "Description is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 76),
                    },
                    "Description must not exceed 75 characters."
                };

                //CabinName
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Name is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Name is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Name must not exceed 75 characters."
                };

                //CabinShortName
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Short Name is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Short Name is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Cabin Short Name must not exceed 35 characters."
                };

                //StationCode
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Station Code is required."
                };
                yield return new object[]
                {
                    new CabinVm
                    {
                        Description = new string('d', 75),
                    },
                    "Station Code is required."
                };

            }
        }
    }
}
