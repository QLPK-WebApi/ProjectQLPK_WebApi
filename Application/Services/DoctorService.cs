using App_QLPK.Application.Common;
using App_QLPK.Application.DTO;
using App_QLPK.Application.Interfaces;
using App_QLPK.Application.Interfaces.Repositories;
using App_QLPK.Application.Interfaces.Services;
using App_QLPK.Application.Mapping;
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Services;


public class DoctorService : IDoctorService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly ISpecialtyRepository _specialtyRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IUnitOfWork _unitOfWork;


    public DoctorService(
        IUserRepository userRepo,
        ISpecialtyRepository specialtyRepo,
        IDoctorRepository doctorRepo,
        IRoleRepository roleRepo,
        IPasswordHasher hasher,
        IUnitOfWork unitOfWork)
    {
        _userRepo = userRepo;
        _specialtyRepo = specialtyRepo;
        _doctorRepo = doctorRepo;
        _roleRepo = roleRepo;
        _hasher = hasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<DoctorDTO> CreateAsync(CreateDoctorDTO dto, CancellationToken ct = default)
    {
        await _unitOfWork.BeginTransactionAsync(ct);

        try
        {   // check username
            var existed = await _doctorRepo.GetByUsernameAsync(dto.Username, ct);

            if (existed != null)
                throw new AppException("Username đã tồn tại.");

            // lấy role Doctor
            var role = await _roleRepo.GetByCodeAsync("Doctor", ct);

            if (role == null)
                throw new AppException("Role Doctor không tồn tại.");
            
            // hash Password
            var hash = _hasher.Hash(dto.Password);

            // Tạo user
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hash,
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                RoleId = role.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };
            await _userRepo.AddAsync(user, ct);


            // Add Specialty
            var specialties = new List<Specialty>();

            foreach (var sp in dto.SpecialtyNames)
            {
                var name = sp.Trim();

                var existedSpec = await _specialtyRepo.GetByNameAsync(name, ct);
                if (existedSpec != null)
                {
                    specialties.Add(existedSpec);
                }
                else
                {
                    var newSpec = new Specialty
                    {
                        Name = name,
                        Description = dto.SpecialtyDescription,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _specialtyRepo.AddAsync(newSpec, ct);

                    specialties.Add(newSpec); // Add vào List<Specialty>
                }
            }


            // Tạo Doctor
            var doctor = new Doctor
            {
                UserId = user.Id,
                LicenseNumber = dto.LicenseNumber,
                Biography = dto.Biography,
                IsActive = true,
                CreatedAt = dto.CreatedAt,
                DoctorSpecialties = specialties.Select(s => new DoctorSpecialty
                {
                    SpecialtyId = s.Id
                }).ToList()
            };

            await _doctorRepo.AddAsync(doctor, ct);



            await _unitOfWork.CommitAsync(ct);


            return doctor.ToDto();
        }
        catch
        {
            await _unitOfWork.RollbackAsync(ct);
            throw ;
        }
        
    }
}