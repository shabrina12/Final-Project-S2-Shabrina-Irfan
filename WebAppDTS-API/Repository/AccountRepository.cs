using WebAppDTS_API.Contexts;
using WebAppDTS_API.Models;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.ViewModels;
using WebAppDTS_API.Handlers;

namespace WebAppDTS_API.Repository
{
    public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
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
        }

        public async Task RegisterAsync(RegisterVM registerVM)
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
            }
            catch
            {
                await transaction.RollbackAsync();
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

        public async Task<string> GetRoleName(string email)
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getRoleName = await _roleRepository.GetAllAsync();
            var getAccountRoles = await _accountRoleRepository.GetAllAsync();

            var getUserRole = getEmployees
                                .Join(getAccountRoles, e => e.Nik, ar => ar.AccountNik, (e, ar) => new { e.Email, ar.RoleId })
                                .Join(getRoleName, ea => ea.RoleId, r => r.Id, (ea, r) => new { ea.Email, r.Name })
                                .FirstOrDefault(e => e.Email == email)!.Name;

            return getUserRole;
        }
    }
}
