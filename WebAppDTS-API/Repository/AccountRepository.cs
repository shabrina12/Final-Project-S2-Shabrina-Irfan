using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;
using WebAppDTS_API.Handlers;
using Microsoft.EntityFrameworkCore;

namespace WebAppDTS_API.Repository
{
    public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        //private readonly MyContext context;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IProfilingRepository _profilingRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountRepository(
            MyContext context,
            IUniversityRepository universityRepository,
            IEducationRepository educationRepository,
            IEmployeeRepository employeeRepository,
            IAccountRoleRepository accountRoleRepository,
            IProfilingRepository profilingRepository,
            IRoleRepository roleRepository
            ) : base(context)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _accountRoleRepository = accountRoleRepository;
            _profilingRepository = profilingRepository;
            _roleRepository = roleRepository;
            _context = context;
        }
        
        public async Task<int> RegisterAsync(RegisterVM registerVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // UNIVERSITY
                var university = new University
                {
                    Name = registerVM.UniversityName
                };
                if (await _universityRepository.IsNameExistAsync(registerVM.UniversityName))
                {
                    university.Id = _universityRepository.GetByNameAsync(registerVM.UniversityName).Id;
                }
                else
                {
                    await _universityRepository.InsertAsync(university);
                }

                // EDUCATION
                var education = await _educationRepository.InsertAsync(new Education
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.GPA,
                    UniversityId = university.Id,
                });

                // EMPLOYEE
                var employee = await _employeeRepository.InsertAsync(new Employee
                {
                    Nik = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    PhoneNumber = registerVM.PhoneNumber,
                    Email = registerVM.Email,
                    HiringDate = DateTime.Now
                });

                // ACCOUNT
                await InsertAsync(new Account
                {
                    EmployeeNik = employee!.Nik,
                    Password = Hashing.HashPassword(registerVM.Password)
                });

                // PROFILING
                await _profilingRepository.InsertAsync(new Profiling
                {
                    EmployeeNik = employee.Nik,
                    EducationId = education!.Id
                });

                // ACCOUNT ROLE
                await _accountRoleRepository.InsertAsync(new AccountRole
                {
                    AccountNik = registerVM.NIK,
                    RoleId = 1
                });

                await transaction.CommitAsync();
                return 1;
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                return 0;
            }
        }

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getAccounts = await GetAllAsync();

            var getUserData = getEmployees.Join(getAccounts,
                                                e => e.Nik,
                                                a => a.EmployeeNik,
                                                (e, a) => new LoginVM
                                                {
                                                    Email = e.Email,
                                                    Password = a.Password
                                                })
                                          .FirstOrDefault(ud => ud.Email == loginVM.Email);

            return getUserData is not null && Hashing.ValidatePassword(loginVM.Password, getUserData.Password);
        }

        //public async Task<Account?> GetAccountByEmail(string email)
        //{
        //    //using var transaction = _context.Database.BeginTransaction();
        //    //var getNIK = await _employeeRepository.GetFullNameByEmailAsync(email);
        //    //var getEmployee = await _employeeRepository.GetAllAsync();
        //    //var getAccount = await GetAllAsync();

        //    var getNIK = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        //    if (getNIK == null)
        //    {
        //        return null;
        //    }

        //    //var account = getEmployee
        //    //                .Join(getAccount, e => e.Nik, a => a.EmployeeNik, (e, a) => new { e.Email, a.EmployeeNik })
        //    //                .FirstOrDefault(e => e.Email == email).EmployeeNik;
        //    var account = await _context.Set<Account>().FirstOrDefaultAsync(a => a.EmployeeNik == getNIK.Nik);
        //    return account;
        //}
    }
}
