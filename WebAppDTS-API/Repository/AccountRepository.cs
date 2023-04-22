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
        //    var getNIK = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        //    if (getNIK == null)
        //    {
        //        return null;
        //    }
        //    var account = await _context.Set<Account>().FirstOrDefaultAsync(a => a.EmployeeNik == getNIK.Nik);
        //    return account;
        //}

        //public async Task<UserVM> GetUserData(string email)
        //{
        //    var result = await _context.Employees.Select(e => new UserVM
        //    {
        //        Email = e.Email,
        //        FullName = String.Concat(e.FirstName, " ", e.LastName),
        //    }).FirstOrDefaultAsync(e => e.Email == email);

        //    return result;
        //}

        //public async Task<List<string>> GetRoles(string email)
        //{
        //    var getNIK = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        //    var getAccountRoles = await _accountRoleRepository.GetAllAsync();
        //    var getRoles = await _roleRepository.GetAllAsync();
        //    var getRole = getAccountRoles.Where(ar => ar.AccountNik == getNIK.Nik) 
        //                                 .Join(getRoles, ar => ar.RoleId, r => r.Id, (ar, r) => r.Name).ToList();

        //    return getRole;
        //}

        //public async Task<int> UpdateToken(string email, string refreshToken, DateTime expiryTime)
        //{
        //    var getNIK = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        //    if (getNIK == null)
        //    {
        //        return 0;
        //    }
           
        //    var account = await _context.Set<Account>().FirstOrDefaultAsync(a => a.EmployeeNik == getNIK.Nik);
        //    account.RefreshToken = refreshToken;
        //    account.RefreshTokenExpiryTime = expiryTime;

        //    return await base.UpdateAsync(account);
        //}

        //public async Task<int> UpdateToken(string email, string refreshToken)
        //{
        //    var getNIK = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        //    if (getNIK == null)
        //    {
        //        return 0;
        //    }
        //    var account = await _context.Set<Account>().FirstOrDefaultAsync(a => a.EmployeeNik == getNIK.Nik);
        //    account.RefreshToken = refreshToken;

        //    return await base.UpdateAsync(account);
        //}
    }
}
